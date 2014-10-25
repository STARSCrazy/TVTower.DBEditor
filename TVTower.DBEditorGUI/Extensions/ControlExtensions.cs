using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.DBEditorGUI.Util;
using System.Collections;
using CodeKnight.Core;

namespace System.Windows.Forms
{
    public static class ControlExtensions
    {
        public static void AddItem( this ComboBox comboBox, string name, object value )
        {
            comboBox.Items.Add( new ComboBoxItem( name, value ) );
        }

        public static void AddItem( this CheckedListBox clBox, string name, object value )
        {
            clBox.Items.Add( new CheckedListBoxItem( name, value ) );
        }

        //public static void SetItem( this ComboBox comboBox, object value )
        //{
        //    var temp = comboBox.Items.OfType<ComboBoxItem>().ToList();
        //    var item = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault( x => ((object)x.Value) == value );
        //    comboBox.SelectedItem = item;
        //}

        public static void SetItem<T>( this ComboBox comboBox, T value )
        {
            var item = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault( x => x.ToValue<T>().Equals(value) );

            comboBox.SelectedItem = item;
        }

        public static void CheckItems<T>( this CheckedListBox clBox, List<T> value )
        {
            if ( value != null )
            {
                var items = clBox.Items.OfType<CheckedListBoxItem>().Where( x => value.Contains( (T)x.Value ) ).ToList();

                for ( var i = 0; i < clBox.Items.Count; i++ )
                {
                    clBox.SetItemChecked( i, items.Contains( clBox.Items[i] ) );
                }
            }
            else
            {
                for ( var i = 0; i < clBox.Items.Count; i++ )
                {
                    clBox.SetItemChecked( i, false );
                }
            }
        }

        public static T GetSelectedItemsAs<T>( this ComboBox comboBox )
        {
            return(T)( (ComboBoxItem)comboBox.SelectedItem ).Value;
        }

        public static List<T> GetCheckedItemsAs<T>(this CheckedListBox clBox)
        {
            return clBox.CheckedItems.OfType<CheckedListBoxItem>().Select( x => x.Value ).OfType<T>().ToList();
        }

        //public static void SetItemEnum<T>( this ComboBox comboBox, T value )
        //    where T : Enum
        //{
        //    ComboBoxItem item = null;
        //    if ( typeof( T ).IsEnum )
        //    {
                

        //        int valueInt = Int16.Parse( value.ToString() );
        //        var t = Convert.ChangeType( value, value.GetTypeCode() );
        //        item = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault( x => ( (int)x.Value ) == valueInt );
        //    }
        //    else
        //        item = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault( x => x.ToValue<T>().Equals( value ) );

        //    comboBox.SelectedItem = item;
        //}
    }
}
