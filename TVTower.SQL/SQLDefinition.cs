using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using CodeKnight.Core;
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;

namespace TVTower.SQL
{
    public class SQLDefinition<T>
    {
        private List<SQLDefinitionField> Definition = new List<SQLDefinitionField>();

        public void Add<TProperty>(Expression<Func<T, TProperty>> expression, string fieldName = null, string suffix = null, int? listIndex = null)
        {
            Definition.Add(new SQLDefinitionField(PInfo<T>.Info(expression, false), fieldName, suffix, listIndex));
        }

        public void Add<TProperty>(SQLDefinitionField field)
        {
            Definition.Add(field);
        }

        public string GetFieldNames(string seperator, string prefix = null)
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

            foreach (var currChar in fieldName.ToCharArray())
            {
                if (char.IsUpper(currChar))
                {
                    if (builder.Length == 0 || lastWasUpper)
                        builder.Append(currChar.ToString().ToLower());
                    else
                        builder.Append("_" + currChar.ToString().ToLower());
                    lastWasUpper = true;
                }
                else
                {
                    builder.Append(currChar);
                    lastWasUpper = false;
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

        public void Read(MySqlDataReader reader, object model)
        {
            var value = reader[FieldName];
            if (value is DBNull)
                value = null;

            if (PropertyInfo.PropertyType == typeof(string))
            {                
                PropertyInfo.SetValue(model, value, null);
            }
            else if (PropertyInfo.PropertyType == typeof(int))
            {
                var valueInt = int.Parse(value.ToString());
                PropertyInfo.SetValue(model, valueInt, null);
            }
            else if (PropertyInfo.PropertyType == typeof(bool))
            {
                bool valueBool = false;
                int intValue = 0;

                if (int.TryParse(value.ToString(), out intValue))
                {
                    if (intValue == 0)
                        valueBool = false;
                    else if (intValue == 1)
                        valueBool = true;
                    PropertyInfo.SetValue(model, valueBool, null);
                }
                else
                {
                    valueBool = bool.Parse(value.ToString());
                    PropertyInfo.SetValue(model, valueBool, null);
                }
            }
            else if (PropertyInfo.PropertyType == typeof(Guid))
            {
                var guid = Guid.Parse(value.ToString());
                PropertyInfo.SetValue(model, guid, null);
            }
        }
    }
}
