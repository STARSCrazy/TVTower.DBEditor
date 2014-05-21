using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TVTower.SQL;
using TVTower.DBEditor;
using TVTower.Converter;

namespace TVTower.UnitTests
{
    [TestClass]
    public class MySQLTest
    {
        [TestMethod]
        public void ConnectTest()
        {
            string myConnectionString = "SERVER=localhost;" +
                                        "DATABASE=tvtower;" +
                                        "UID=TVTowerUser;" +
                                        "PASSWORD=123;";

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_filme";
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString() + ", ";
                Console.WriteLine(row);
            }
            connection.Close();
        }

        [TestMethod]
        public void LoadMoviesTest()
        {
            var database = new TVTBindingListDatabase();
            database.Initialize();

            using (var connection = TVTSQLSession.GetSession())
            {
                var movies = TVTCommands.LoadMoviesOldV2(connection);

                OldV2Converter.Convert(movies, database);

                TVTCommands.LoadFakesForPeople(connection, database.GetAllPeople());

                OldV2Converter.RefreshMovieDescriptions(database);
                //MovieOldV2Converter.FakePersonNames(database);

                database.RefreshPersonProgrammeCount();
                database.RefreshStatus();

                var ads = TVTCommands.LoadAdsOldV2(connection);

                OldV2Converter.Convert(ads, database);
            }

            using (var connection = TVTSQLSession.GetSessionNewDB())
            {
				TVTCommands.InsertPeople( connection, database.GetAllPeople() );
                TVTCommands.InsertProgrammes(connection, database.GetAllMovies(true));
                TVTCommands.InsertEpisodes(connection, database.GetAllEpisodes());
                TVTCommands.InsertAdvertisings(connection, database.GetAllAdvertisings());
            }
            

            var t = 1;
        }


    }
}
