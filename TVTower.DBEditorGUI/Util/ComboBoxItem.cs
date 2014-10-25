using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.DBEditorGUI.Util
{
    public class ComboBoxItem
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public T ToValue<T>()
        {
            return (T)Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
