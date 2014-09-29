using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;
using TVTower.SQL;
using TVTower.Database;

namespace TVTower.DBEditorGUI
{
    public class TVTEditorApplication
    {
        private static TVTEditorApplication instance;

        private TVTEditorApplication() { }

        public ITVTDatabase InternalDatabase { get; set; }
        public TVTSQLDatabase MYSQLDatabase { get; set; }

        public static TVTEditorApplication Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new TVTEditorApplication();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            var database = new TVTDatabase(); 
            database.Initialize();
            InternalDatabase = database;
        }

        public void ConnectToMySQLDatabase(string dbconnection)
        {
            InternalDatabase.Clear();

            MYSQLDatabase = new TVTSQLDatabase( dbconnection );
            MYSQLDatabase.FillTVTDatabase( InternalDatabase );
        }
    }
}
