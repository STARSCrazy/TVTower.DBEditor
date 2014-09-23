using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TVTower.DBEditorGUI.Controls
{
    public class TVTColumnHeader : ColumnHeader
    {
        public TVTColumnHeader() : base()
        {
        }

        public TVTColumnHeader(int imageIndex) : base(imageIndex)
        {
        }

        public TVTColumnHeader( string imageKey )
            : base( imageKey )
        {
        }
    }
}
