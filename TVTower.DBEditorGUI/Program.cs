using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TVTower.Entities;
using System.IO;
using TVTower.DBEditorGUI;

namespace DBEditorGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            var disclaimer = new Disclaimer();

            var result = disclaimer.ShowDialog();
            if ( result == DialogResult.Yes )
            {
                TVTEditorApplication.Instance.Initialize();
                Application.Run( new MainForm() );
            }
        }
    }
}
