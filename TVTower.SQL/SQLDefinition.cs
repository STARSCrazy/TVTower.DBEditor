using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using CodeKnight.Core;
using MySql.Data.MySqlClient;
using TVTower.Converter;

namespace TVTower.SQL
{
	public interface ISQLDefinition
	{
		string OwnerIdField { get; set; }

		ISQLDefinitionField Add( ISQLDefinitionField field );

		ISQLDefinitionField GetFieldDefinition( string fieldName );
	}

	public class SQLDefinition<T> : ISQLDefinition
	{
		public string Table;
		public string OwnerIdField { get; set; }
		private List<ISQLDefinitionField> Definition = new List<ISQLDefinitionField>();
		public Dictionary<PropertyInfo, ISQLDefinition> SubDefinitions = new Dictionary<PropertyInfo, ISQLDefinition>();
		public Action<MySqlConnection, T> AfterInsert = null;

		public SQLDefinitionField Add<TProperty>( Expression<Func<T, TProperty>> expression, string fieldName = null, string suffix = null, int? listIndex = null )
		{
			var result = new SQLDefinitionField( PInfo<T>.Info( expression, false ), fieldName, suffix, listIndex );
			Definition.Add( result );
			return result;
		}

		public ISQLDefinitionField Add( ISQLDefinitionField field )
		{
			Definition.Add( field );
			return field;
		}

		public void AddSubDefinition<TProperty>( Expression<Func<T, TProperty>> expression, ISQLDefinition subDefinition )
		{
			SubDefinitions.Add( PInfo<T>.Info( expression, false ), subDefinition );
		}

		public ISQLDefinitionField GetFieldDefinition( string fieldName )
		{
			return Definition.FirstOrDefault( x => x.FieldName == fieldName );
		}

		public string GetFieldNames( char seperator, string prefix = null )
		{
			if ( !string.IsNullOrEmpty( prefix ) )
			{
				return Definition.Select( x => prefix + x.FieldName ).ToContentString( seperator );
			}
			else
				return Definition.Select( x => x.FieldName ).ToContentString( seperator );
		}

		public IEnumerator<ISQLDefinitionField> GetEnumerator()
		{
			return Definition.GetEnumerator();
		}
	}

	public interface ISQLDefinitionField
	{
		string FieldName { get; set; }
		bool IsKeyField { get; set; }

		object GetValue( object model, bool withQuotemarks = false );
		void Read( MySqlDataReader reader, object model );
	}

	public class SQLDefinitionFieldFunc : ISQLDefinitionField
	{
		public string FieldName { get; set; }
		public bool IsKeyField { get; set; }
		public Func<object, bool, object> GetValueFunc { get; set; }
		public Action<MySqlDataReader, object> ReadAction { get; set; }

		public SQLDefinitionFieldFunc( string fieldName, Func<object, bool, object> getValueFunc = null, Action<MySqlDataReader, object> readAction = null )
		{
			FieldName = fieldName;
			GetValueFunc = getValueFunc;
			ReadAction = readAction;
		}

		public object GetValue( object model, bool withQuotemarks = false )
		{
			return GetValueFunc.Invoke( model, withQuotemarks );
		}

		public void Read( MySqlDataReader reader, object model )
		{
			ReadAction.Invoke( reader, model );
		}
	}

	public class SQLDefinitionField : ISQLDefinitionField
	{
		public string FieldName { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
		public int? ListIndex { get; set; }
		public bool IsKeyField { get; set; }

		public SQLDefinitionField( PropertyInfo propertyInfo, string fieldName = null, string suffix = null, int? listIndex = null )
		{
			this.IsKeyField = false;
			this.PropertyInfo = propertyInfo;

			this.ListIndex = listIndex;

			if ( fieldName == null )
				fieldName = propertyInfo.Name;
			var builder = new StringBuilder();
			bool lastWasUpper = false;
			bool lastWasNumber = false;

			foreach ( var currChar in fieldName.ToCharArray() )
			{
				if ( char.IsUpper( currChar ) )
				{
					if ( builder.Length == 0 || lastWasUpper )
						builder.Append( currChar.ToString().ToLower() );
					else
						builder.Append( "_" + currChar.ToString().ToLower() );
					lastWasUpper = true;
					lastWasNumber = false;
				}
				else if ( char.IsNumber( currChar ) )
				{
					if ( builder.Length == 0 || lastWasNumber )
						builder.Append( currChar.ToString().ToLower() );
					else
						builder.Append( "_" + currChar.ToString().ToLower() );
					lastWasUpper = false;
					lastWasNumber = true;
				}
				else
				{
					builder.Append( currChar );
					lastWasUpper = false;
					lastWasNumber = false;
				}
			}
			FieldName = builder.ToString();

			if ( !string.IsNullOrEmpty( suffix ) )
				FieldName = FieldName + suffix;
		}

		public object GetValue( object model, bool withQuotemarks = false )
		{
			var value = PropertyInfo.GetValue( model, null );

			if ( value is IList )
			{
				var list = (IList)value;

				if ( this.ListIndex.HasValue )
				{
					if ( list.Count > ListIndex.Value )
						return list[ListIndex.Value];
					else
						return null;
				}
				else
				{
					if ( list != null && list.Count > 0 )
						return list.ToContentString();
					else
						return null;
				}
			}
			else if ( value is IIdEntity )
				return (value as IIdEntity).Id;
			else if ( value is DateTime )
				return value;
			else if ( value is Enum )
				return (int)value;
			else if ( value is int || value is float )
				return value;
			else if ( withQuotemarks )
				return "'" + value + "'";
			else
				return value;
		}

		public virtual void Read( MySqlDataReader reader, object model )
		{
			var value = reader[FieldName];
			object typeValue;

			typeValue = DBValueConverter.ConvertDBValueToType( PropertyInfo.PropertyType, value );

			if ( PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof( List<> ) )
			{
				if ( typeValue != null )
				{
					var listValue = (IList)typeValue;

					var propertyList = (IList)PropertyInfo.GetValue( model, null );

					if ( propertyList != null )
						propertyList.Add( listValue[0] );
					else
					{
						PropertyInfo.SetValue( model, listValue, null );
					}
				}
			}
			else
				PropertyInfo.SetValue( model, typeValue, null );
		}
	}


	public class SQLDefinitionFieldList<T> : SQLDefinitionField
	{
		public SQLDefinitionFieldList( PropertyInfo propertyInfo, string fieldName = null, string suffix = null, int? listIndex = null )
			: base( propertyInfo, fieldName, suffix, listIndex )
		{
		}

		public override void Read( MySqlDataReader reader, object model )
		{
			var value = reader[FieldName];
			if ( value is DBNull )
				value = null;

			var list = new List<T>();
			var item = Enum.Parse( typeof( T ), value.ToString() );
			list.Add( (T)item );

			PropertyInfo.SetValue( model, list, null );
		}
	}
}
