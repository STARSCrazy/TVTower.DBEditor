using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TVTower.Converter;
using TVTower.DBEditor;
using TVTower.Entities;
using TVTower.SQL;
using TVTower.Xml;
using TVTower.DBEditorGUI;

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

            MySqlConnection connection = new MySqlConnection( myConnectionString );
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_filme";
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();
            while ( Reader.Read() )
            {
                string row = "";
                for ( int i = 0; i < Reader.FieldCount; i++ )
                    row += Reader.GetValue( i ).ToString() + ", ";
                Console.WriteLine( row );
            }
            connection.Close();
        }

        [TestMethod]
        public void ConvertOldToNewDataTest()
        {
            var database = new TVTBindingListDatabaseOld();
            database.Initialize();

            using ( var connection = TVTSQLSession.GetSession() )
            {
                var movies = TVTCommandsV2.LoadMoviesOldV2( connection );

                OldV2Converter.Convert( movies, database, TVTDataRoot.V2InStorage );

                TVTCommandsV2.LoadFakesForPeople( connection, database.GetAllPeople() );

                OldV2Converter.RefreshMovieDescriptions( database );
                OldV2Converter.FakePersonNames( database );

                var ads = TVTCommandsV2.LoadAdsOldV2( connection );
                OldV2Converter.Convert( ads, database, TVTDataRoot.V2InStorage );

                var news = TVTCommandsV2.LoadNewsOldV2( connection );
                OldV2Converter.Convert( news, database, TVTDataRoot.V2InStorage );

                database.RefreshPersonProgrammeCount();
                database.RefreshReferences();
                database.RefreshStatus();
            }

            using ( var connection = TVTSQLSession.GetSessionNewDB() )
            {
                TVTCommandsV3.Insert<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), database.GetAllPeople() );
                TVTCommandsV3.Insert<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), database.GetAllProgrammes( true ) );
                //TVTCommandsV3.Insert<TVTEpisode>( connection, TVTCommandsV3.GetEpisodeSQLDefinition(), database.GetAllEpisodes() );
                TVTCommandsV3.Insert<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), database.GetAllAdvertisings() );
                TVTCommandsV3.Insert<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), database.GetAllNews() );
            }
        }

        [TestMethod]
        public void SQLDefinitiontest()
        {
            var definition = new SQLDefinition<TVTProgramme>();
            definition.Add( x => x.FakeTitleDE );
        }

        [TestMethod]
        [DeploymentItem( "TestData\\database.xml" )]
        public void ReadXMLV2Test()
        {
            System.Diagnostics.Trace.WriteLine( Directory.GetCurrentDirectory() );
            Assert.IsTrue( File.Exists( "database.xml" ) );

            var databaseV2 = new TVTBindingListDatabaseOld();
            databaseV2.Initialize();

            var persister = new XmlPersisterV2();
            persister.LoadXML( "database.xml", databaseV2, TVTDataRoot.V2InUse );

            var sqlDB = new TVTDatabase();
            sqlDB.Initialize();
            sqlDB.Clear();
            using ( var connection = TVTSQLSession.GetSessionNewDB() )
            {
                string myConnectionString = "SERVER=localhost;" +
                                            "DATABASE=tvtower_new;" +
                                            "UID=TVTowerUser;" +
                                            "PASSWORD=123;";

                var mvSQLDatabase = new TVTSQLDatabase( myConnectionString );
                mvSQLDatabase.FillTVTDatabase( sqlDB );
            }

            var allProgrammes = sqlDB.GetAllProgrammes( true, true );
            foreach ( var currMovie in databaseV2.GetAllProgrammes( true, true ) )
            {
                var found = allProgrammes.FirstOrDefault( x => x.FakeTitleDE == currMovie.FakeTitleDE || x.TitleDE == currMovie.FakeTitleDE );
                if ( found != null )
                {
                    found.DataRoot = TVTDataRoot.V2InUse;
                    found.Changed = true;
                    var i = 3;
                }
                else
                {
                    var director = currMovie.Staff != null ? currMovie.Staff.FirstOrDefault( y => y.Function == TVTPersonFunction.Director ) : null;
                    var direcLastName = director != null ? director.Person.FakeLastName : "unpassend";

                    var found2 = allProgrammes.FirstOrDefault( new Func<TVTProgramme, bool>(x => {
                        if (x.Year != currMovie.Year)
                            return false;

                        if ( x.MainGenre != currMovie.MainGenre )
                            return false;

                        var directorTemp = x.Staff != null ? x.Staff.FirstOrDefault( z => z.Function == TVTPersonFunction.Director ) : null;
                        if (directorTemp != null)
                        {
                            if (directorTemp.Person.FakeLastName != direcLastName)
                            return false;
                        }
                        else
                            return false;

                        return true;
                    }));

                    if ( found2 != null )
                    {
                        found2.DataRoot = TVTDataRoot.V2InUse;
                        found2.Changed = true;
                        var i = 3;
                    }
                    else
                        System.Diagnostics.Trace.WriteLine( "Not found: " + currMovie.FakeTitleDE );
                }

            }
        }

        [TestMethod]
        public void CreateXMLTest()
        {
            var database = new TVTBindingListDatabaseOld();
            database.Initialize();

            using ( var connection = TVTSQLSession.GetSessionNewDB() )
            {
                var programmes = TVTCommandsV3.Read<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), "master_id, episode_index, fake_title_de, title_de" );
                database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );

                //var episodes = TVTCommandsV3.Read<TVTEpisode>( connection, TVTCommandsV3.GetEpisodeSQLDefinition(), "fake_title_de, title_de" );
                //database.AddEpisodes( episodes );

                var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), "fake_title_de, title_de" );
                database.AddAdvertisings( ads );

                var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), "fake_last_name, fake_first_name, last_name" );
                database.AddPeople( people );

                var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), "title_de" );
                database.AddNews( news );

                database.RefreshReferences();
            }

            var persister = new XmlPersisterV3();
            persister.SaveXML( database, "TVTDatabaseV3.xml", DatabaseVersion.V3, DataStructure.FakeData );
        }

    }
}
