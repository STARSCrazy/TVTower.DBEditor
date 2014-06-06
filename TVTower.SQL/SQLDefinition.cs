using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using CodeKnight.Core;
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;
using TVTower.Converter;

namespace TVTower.SQL
{
    public class SQLDefinition<T>
    {
        private List<SQLDefinitionField> Definition = new List<SQLDefinitionField>();

        public void Add<TProperty>(Expression<Func<T, TProperty>> expression, string fieldName = null, string suffix = null, int? listIndex = null)
        {
            Definition.Add(new SQLDefinitionField(PInfo<T>.Info(expression, false), fieldName, suffix, listIndex));
        }

        public void Add(SQLDefinitionField field)
        {
            Definition.Add(field);
        }

        public string GetFieldNames(char seperator, string prefix = null)
        {
            if (!string.IsNullOrEmpty(prefix))
            {
                return Definition.Select(x => prefix + x.FieldName).ToContentString(seperator);
            }
            else
                return Definition.Select(x => x.FieldName).ToContentString(seperator);
        }

        public IEnumerator<SQLDefinitionField> GetEnumerator()
        {
            return Definition.GetEnumerator();
        }
    }

    public class SQLDefinitionField
    {
        public string FieldName { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public int? ListIndex { get; set; }

        public SQLDefinitionField(PropertyInfo propertyInfo, string fieldName = null, string suffix = null, int? listIndex = null)
        {
            this.PropertyInfo = propertyInfo;

            this.ListIndex = listIndex;

            if (fieldName == null)
                fieldName = propertyInfo.Name;
            var builder = new StringBuilder();
            bool lastWasUpper = false;
            bool lastWasNumber = false;

            foreach (var currChar in fieldName.ToCharArray())
            {
                if (char.IsUpper(currChar))
                {
                    if (builder.Length == 0 || lastWasUpper)
                        builder.Append(currChar.ToString().ToLower());
                    else
                        builder.Append("_" + currChar.ToString().ToLower());
                    lastWasUpper = true;
                    lastWasNumber = false;
                }
                else if (char.IsNumber(currChar))
                {
                    if (builder.Length == 0 || lastWasNumber)
                        builder.Append(currChar.ToString().ToLower());
                    else
                        builder.Append("_" + currChar.ToString().ToLower());
                    lastWasUpper = false;
                    lastWasNumber = true;
                }
                else
                {
                    builder.Append(currChar);
                    lastWasUpper = false;
                    lastWasNumber = false;
                }
            }
            FieldName = builder.ToString();

            if (!string.IsNullOrEmpty(suffix))
                FieldName = FieldName + suffix;
        }

        public object GetValue(object model)
        {
            var value = PropertyInfo.GetValue(model, null);

            if (value is IList)
            {
                var list = (IList)value;

                if (this.ListIndex.HasValue)
                {
                    if (list.Count > ListIndex.Value)
                        return list[ListIndex.Value];
                    else
                        return null;
                }
                else
                {
                    if (list != null && list.Count > 0)
                        return list.ToContentString();
                    else
                        return null;
                }
            }
            else
                return PropertyInfo.GetValue(model, null);
        }

        public virtual void Read(MySqlDataReader reader, object model)
        {
            var value = reader[FieldName];
            object typeValue;

            typeValue = DBValueConverter.ConvertDBValueToType(PropertyInfo.PropertyType, value);

            if (PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                if (typeValue != null)
                {
                    var listValue = (IList)typeValue;

                    var propertyList = (IList)PropertyInfo.GetValue(model, null);

                    if (propertyList != null)
                        propertyList.Add(listValue[0]);
                    else
                    {
                        PropertyInfo.SetValue(model, listValue, null);
                    }
                }
            }
            else                
                PropertyInfo.SetValue(model, typeValue, null);
        }
    }


    public class SQLDefinitionFieldList<T> : SQLDefinitionField
    {
        public SQLDefinitionFieldList(PropertyInfo propertyInfo, string fieldName = null, string suffix = null, int? listIndex = null)
            : base(propertyInfo, fieldName, suffix, listIndex)
        {
        }

        public override void Read(MySqlDataReader reader, object model)
        {
            var value = reader[FieldName];
            if (value is DBNull)
                value = null;

            var list = new List<T>();
            var item = Enum.Parse(typeof(T), value.ToString());
            list.Add((T)item);

            PropertyInfo.SetValue(model, list, null);
        }
    }
}
