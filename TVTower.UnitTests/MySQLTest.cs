using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TVTower.Converter;
using TVTower.DBEditor;
using TVTower.Entities;
using TVTower.SQL;
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
			var database = new TVTBindingListDatabase();
			database.Initialize();

			using ( var connection = TVTSQLSession.GetSession() )
			{
				var movies = TVTCommandsV2.LoadMoviesOldV2( connection );

				OldV2Converter.Convert( movies, database );

				TVTCommandsV2.LoadFakesForPeople( connection, database.GetAllPeople() );

				OldV2Converter.RefreshMovieDescriptions( database );
				OldV2Converter.FakePersonNames( database );

				var ads = TVTCommandsV2.LoadAdsOldV2( connection );
				OldV2Converter.Convert( ads, database );

				var news = TVTCommandsV2.LoadNewsOldV2( connection );
				OldV2Converter.Convert( news, database );

				database.RefreshPersonProgrammeCount();
				database.RefreshReferences();
				database.RefreshStatus();
			}

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				TVTCommandsV3.Insert<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), database.GetAllPeople() );
				TVTCommandsV3.Insert<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), database.GetAllProgrammes( true ) );
				TVTCommandsV3.Insert<TVTEpisode>( connection, TVTCommandsV3.GetEpisodeSQLDefinition(), database.GetAllEpisodes() );
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
		public void CreateXMLTest()
		{
			var database = new TVTBindingListDatabase();
			database.Initialize();

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				var programmes = TVTCommandsV3.Read<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition() );
				database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );

				var episodes = TVTCommandsV3.Read<TVTEpisode>( connection, TVTCommandsV3.GetEpisodeSQLDefinition() );
				database.AddEpisodes( episodes );

				var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition() );
				database.AddAdvertisings( ads );

				var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition() );
				database.AddPeople( people );

				var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition() );
				database.AddNews( news );

				database.RefreshReferences();
			}

			var persister = new XmlPersister();
			persister.SaveXML( database, "ExportTVTDatabaseV3Original.xml", DatabaseVersion.V3, DataStructure.FakeData );
		}

	}
}
