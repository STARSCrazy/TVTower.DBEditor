using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace TVTower.SQL
{
    public static class TVTSQLSession
    {
        public static MySqlConnection GetSession()
        {
            string myConnectionString = "SERVER=localhost;" +
                                        "DATABASE=tvtower;" +
                                        "UID=TVTowerUser;" +
                                        "PASSWORD=123;";
            var connection = new MySqlConnection(myConnectionString);
            connection.Open();
            return connection;
        }

        public static MySqlConnection GetSessionNewDB()
        {
            string myConnectionString = "SERVER=localhost;" +
                                        "DATABASE=tvtower_new;" +
                                        "UID=TVTowerUser;" +
                                        "PASSWORD=123;";
            var connection = new MySqlConnection(myConnectionString);
            connection.Open();
            return connection;
        }
    }
}
