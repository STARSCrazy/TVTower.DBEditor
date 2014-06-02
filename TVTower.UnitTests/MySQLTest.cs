using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TVTower.SQL;
using TVTower.DBEditor;
using TVTower.Converter;
using TVTower.Entities;
using TVTower.Xml;

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
        public void ConvertOldToNewDataTest()
        {
            var database = new TVTBindingListDatabase();
            database.Initialize();

            using (var connection = TVTSQLSession.GetSession())
            {
                var movies = TVTCommandsV2.LoadMoviesOldV2(connection);

                OldV2Converter.Convert(movies, database);

                TVTCommandsV2.LoadFakesForPeople(connection, database.GetAllPeople());

                OldV2Converter.RefreshMovieDescriptions(database);
                OldV2Converter.FakePersonNames(database);

                var ads = TVTCommandsV2.LoadAdsOldV2(connection);
                OldV2Converter.Convert(ads, database);

                var news = TVTCommandsV2.LoadNewsOldV2(connection);
                OldV2Converter.Convert(news, database);

                database.RefreshPersonProgrammeCount();
                database.RefreshStatus();
            }

            using (var connection = TVTSQLSession.GetSessionNewDB())
            {
                TVTCommandsV3.InsertPeople2(connection, database.GetAllPeople());
                TVTCommandsV3.InsertProgrammes2(connection, database.GetAllMovies(true));
                TVTCommandsV3.InsertEpisodes(connection, database.GetAllEpisodes());
                TVTCommandsV3.InsertAdvertisings2(connection, database.GetAllAdvertisings());
                //TVTCommandsV3.InsertAdvertisings(connection, database.GetAllAdvertisings());
                TVTCommandsV3.InsertNews(connection, database.GetAllNews());
            }
        }

        [TestMethod]
        public void SQLDefinitiontest()
        {
            var definition = new SQLDefinition<TVTProgramme>();
            definition.Add(x => x.FakeTitleDE );
        }

        [TestMethod]
        public void CreateXMLTest()
        {
            var database = new TVTBindingListDatabase();
            database.Initialize();

            using (var connection = TVTSQLSession.GetSessionNewDB())
            {
				var ads = TVTCommandsV3.ReadAdvertisings( connection );
                database.AddAdvertisings(ads);

                var people = TVTCommandsV3.ReadPeople(connection);
                database.AddPeople(people);
                int z = 1;
            }

            var persister = new XmlPersister();
            persister.SaveXML(database, "ExportTVTDatabaseV3Original.xml", DatabaseVersion.V3, DataStructure.FakeData);
        }

    }
}
