using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.DBEditorGUI.Util
{
    public class CheckedListBoxItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool Checked { get; set; }

        public CheckedListBoxItem( string name, object value )
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
