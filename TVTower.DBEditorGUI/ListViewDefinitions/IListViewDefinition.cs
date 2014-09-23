using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.DBEditorGUI.Controls;

namespace TVTower.DBEditorGUI.ListViewDefinitions
{
    public interface IListViewDefinition
    {
        List<TVTColumnHeader> GetColumnDefinition();
    }
}
