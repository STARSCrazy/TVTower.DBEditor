using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TVTower.DBEditorGUI
{
    public interface IFormControl
    {
        DockStyle Dock { get; set; }
        string Name { get; set; }

        Control ToControl();
    }
}
