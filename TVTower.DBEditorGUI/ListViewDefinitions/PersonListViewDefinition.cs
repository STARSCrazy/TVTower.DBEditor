using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;
using TVTower.DBEditorGUI.Controls;

namespace TVTower.DBEditorGUI.ListViewDefinitions
{
    public class PersonListViewDefinition : BaseListViewDefinition<TVTPerson>
    {        
        public override List<TVTColumnHeader> GetColumnDefinition()
        {
            if ( columnDefinition == null )
            {
                columnDefinition = new List<TVTGenericColumnHeader<TVTPerson>>();
                AddDefinition( "FirstName", x => x.FirstName, 150 );
                AddDefinition( "LastName", x => x.LastName, 150 );
            }
            return columnDefinition.Cast<TVTColumnHeader>().ToList();
        }
    }
}
