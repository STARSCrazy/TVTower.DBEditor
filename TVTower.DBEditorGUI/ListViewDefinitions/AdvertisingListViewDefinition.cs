using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;
using TVTower.DBEditorGUI.Controls;

namespace TVTower.DBEditorGUI.ListViewDefinitions
{
    public class AdvertisingListViewDefinition : BaseListViewDefinition<TVTAdvertising>
    {        
        public override List<TVTColumnHeader> GetColumnDefinition()
        {
            if ( columnDefinition == null )
            {
                columnDefinition = new List<TVTGenericColumnHeader<TVTAdvertising>>();
                AddDefinition( "TitleDE", x => x.TitleDE, 150 );
                AddDefinition( "TitleEN", x => x.TitleEN, 150 );
            }
            return columnDefinition.Cast<TVTColumnHeader>().ToList();
        }
    }
}
