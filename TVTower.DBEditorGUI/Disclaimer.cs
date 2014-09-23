using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTower.DBEditorGUI.Properties;

namespace TVTower.DBEditorGUI
{
    public partial class Disclaimer : Form
    {
        public Disclaimer()
        {
            InitializeComponent();

            rtbDisclaimer.AppendText( Settings.Default.Disclaimer );
        }

        private void btnAccept_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
