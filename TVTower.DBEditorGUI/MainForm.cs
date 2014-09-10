using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTower.DBEditorGUI.EntityForms;

namespace DBEditorGUI
{
    public partial class MainForm : Form
    {
        AdvertisingForm advertisingForm = new AdvertisingForm();

        public MainForm()
        {
            InitializeComponent();

            this.mainSplitContainer.Panel2.Controls.Add( advertisingForm );
        }

        private void connectToDatabaseMenuItem_Click( object sender, EventArgs e )
        {
            
        }
    }
}
