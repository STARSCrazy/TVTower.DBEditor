using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TVTower.Xml
{
	public static class Extensions
	{
		public static void AddElement( this XmlNode node, string name, string innerText )
		{
			var myNode = node.OwnerDocument.CreateElement( name );
			myNode.InnerText = innerText;
			node.AppendChild( myNode );
		}

		public static void AddAttribute( this XmlNode node, string name, string innerText )
		{
			var myAttribute = node.OwnerDocument.CreateAttribute( name );
			myAttribute.InnerText = innerText;
			node.Attributes.Append( myAttribute );
		}

		public static string GetAttribute( this XmlNode node, string name )
		{
			var attr = node.Attributes[name];
			if ( attr != null )
				return attr.Value;
			else
				return null;
		}

		public static bool HasAttribute( this XmlNode node, string name )
		{
			var attr = node.Attributes[name];
			return (attr != null);
		}

		public static int GetAttributeInteger( this XmlNode node, string name )
		{
			var attr = node.Attributes[name];
			if ( attr != null )
				return int.Parse( attr.Value );
			else
				return 0;
		}

		public static string GetElementValue( this XmlNode node )
		{
			if ( node.ChildNodes.Count == 0 )
				return "";
			else if ( node.ChildNodes.Count == 1 )
				return node.ChildNodes[0].Value;
			else
				throw new NotSupportedException();
		}

		public static string ToContentString<T>( this IEnumerable<T> source, string trimmer )
		{
			var result = new StringBuilder();

			if ( source == null || source.Count() == 0 )
				return string.Empty;

			foreach ( var value in source )
			{
				if ( result.Length == 0 )
					result.Append( value.ToString() );
				else
					result.Append( trimmer ).Append( value.ToString() );
			}

			return result.ToString();
		}
	}
}
