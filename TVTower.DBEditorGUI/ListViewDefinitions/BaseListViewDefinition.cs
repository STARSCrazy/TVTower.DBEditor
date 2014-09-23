using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeKnight.Core;
using TVTower.DBEditorGUI.Controls;

namespace TVTower.DBEditorGUI.ListViewDefinitions
{
    public class BaseListViewDefinition<T> : IListViewDefinition where T : IIdEntity
    {
        protected List<TVTGenericColumnHeader<T>> columnDefinition;

        protected void AddDefinition(string name, Func<T, object> getValueFunc, int width)
        {
            var columnHeader = new TVTGenericColumnHeader<T>();
            columnHeader.Name = name + "Column";
            columnHeader.Text = name;
            columnHeader.Width = width;
            columnHeader.GetValueFunc = getValueFunc;
            columnDefinition.Add(columnHeader);
        }

        public virtual List<TVTColumnHeader> GetColumnDefinition()
        {
            return columnDefinition.Cast<TVTColumnHeader>().ToList();
        }
    }
}
