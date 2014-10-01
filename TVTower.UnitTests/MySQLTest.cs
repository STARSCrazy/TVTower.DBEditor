using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using TVTower.Converter;
using TVTower.Database;
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
				var tt = news.FirstOrDefault( x => x.title.StartsWith( "Programmierer" ) );
				OldV2Converter.Convert( news, database, TVTDataRoot.V2InStorage );

				database.RefreshPersonProgrammeCount();
				database.RefreshReferences();
				database.RefreshStatus();
			}

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				TVTCommandsV3.Insert<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), database.GetAllPeople() );
				TVTCommandsV3.Insert<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), database.GetAllProgrammes( true, true ) );
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

			var merging = new DatabaseMerging( sqlDB );

			{
				foreach ( var currMovie in databaseV2.GetAllProgrammes( true, true ) )
				{
					var found = merging.FindProgrammeMatch( currMovie );
					if ( found != null )
					{
						merging.MergeProgrammeData( found, currMovie, true );
						found.DataRoot = TVTDataRoot.V2InUse;
						found.IsChanged = true;
						foreach ( var member in found.Staff )
						{
							if ( member.Person != null )
							{
								member.Person.DataRoot = TVTDataRoot.V2InUse;
								member.Person.IsChanged = true;
							}
						}

					}
					else
					{
						var newProgramme = new TVTProgramme();
						newProgramme.GenerateGuid();
						merging.MergeProgrammeData( newProgramme, currMovie );
						newProgramme.DataRoot = TVTDataRoot.V2InUse;
						newProgramme.IsNew = true;
						newProgramme.IsChanged = true;
						foreach ( var member in newProgramme.Staff )
						{
							member.Person.DataRoot = TVTDataRoot.V2InUse;
							member.Person.IsChanged = true;
						}
						sqlDB.AddProgramme( newProgramme );
					}
				}
			}

			{
				foreach ( var currNews in databaseV2.GetAllNews() )
				{
					var found = merging.FindNewsMatchWithV2( currNews );
					if ( found != null )
					{
						merging.MergeNewsData( found, currNews, true );
						found.DataRoot = TVTDataRoot.V2InUse;
						found.IsChanged = true;
					}
					else
					{
						var newNews = new TVTNews();
						newNews.GenerateGuid();
						merging.CopyPropertyValues<TVTNews>( newNews, currNews );
						newNews.DataRoot = TVTDataRoot.V2InUse;
						newNews.IsNew = true;
						newNews.IsChanged = true;
						sqlDB.AddNews( newNews );
					}
				}
			}

			{
				foreach ( var currAd in databaseV2.GetAllAdvertisings() )
				{
					var found = merging.FindAdMatchWithV2( currAd );
					if ( found != null )
					{
						merging.CopyPropertyValues<TVTAdvertising>( found, currAd );
						found.DataRoot = TVTDataRoot.V2InUse;
						found.IsChanged = true;
					}
					else
					{
						var newAd = new TVTAdvertising();
						newAd.GenerateGuid();
						merging.CopyPropertyValues<TVTAdvertising>( newAd, currAd );
						newAd.DataRoot = TVTDataRoot.V2InUse;
						newAd.IsNew = true;
						newAd.IsChanged = true;
						sqlDB.AddAdvertising( newAd );
					}
				}
			}

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				string myConnectionString = "SERVER=localhost;" +
											"DATABASE=tvtower_new;" +
											"UID=TVTowerUser;" +
											"PASSWORD=123;";

				var mvSQLDatabase = new TVTSQLDatabase( myConnectionString );
				mvSQLDatabase.WriteChangesToDatabase( sqlDB );
			}
		}

		[TestMethod]
		[DeploymentItem( "TestData\\ads-changes.xml" )]
		public void ReadXMLV2AdChangesTest()
		{
			System.Diagnostics.Trace.WriteLine( Directory.GetCurrentDirectory() );
			Assert.IsTrue( File.Exists( "ads-changes.xml" ) );

			var databaseV3 = new TVTDatabase();
			databaseV3.Initialize();

			var persister = new XmlPersisterV3();
			persister.LoadXML( "ads-changes.xml", databaseV3 );

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

			var merging = new DatabaseMerging( sqlDB );

			{
				foreach ( var currAd in databaseV3.GetAllAdvertisings() )
				{
					var found = merging.FindAdMatchWithV2( currAd );
					if ( found != null )
					{
						merging.CopyPropertyValues<TVTAdvertising>( found, currAd );
						found.DataRoot = TVTDataRoot.V2InUse;
						found.IsChanged = true;
					}
					else
					{
						var newAd = new TVTAdvertising();
						newAd.GenerateGuid();
						merging.CopyPropertyValues<TVTAdvertising>( newAd, currAd );
						newAd.DataRoot = TVTDataRoot.V2InUse;
						newAd.IsNew = true;
						newAd.IsChanged = true;
						sqlDB.AddAdvertising( newAd );
					}
				}
			}

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				string myConnectionString = "SERVER=localhost;" +
											"DATABASE=tvtower_new;" +
											"UID=TVTowerUser;" +
											"PASSWORD=123;";

				var mvSQLDatabase = new TVTSQLDatabase( myConnectionString );
				mvSQLDatabase.WriteChangesToDatabase( sqlDB );
			}
		}



		[TestMethod]
		public void CreateXMLTest()
		{
			var database = new TVTBindingListDatabaseOld();
			database.Initialize();

			var dataRoot = TVTDataRoot.V2InUse;

			using ( var connection = TVTSQLSession.GetSessionNewDB() )
			{
				var programmes = TVTCommandsV3.Read<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), "master_id, episode_index, fake_title_de, title_de" );
				//database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );
				//var yames = programmes.Where( x => x.FakeTitleDE != null && x.FakeTitleDE.StartsWith( "Yams Pond" ) ).ToList();
				//var yames2 = yames.Where( x => x.DataRoot == dataRoot ).ToList();
				database.AddProgrammes( programmes.Where( x => x.DataRoot == dataRoot ) );

				var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), "fake_title_de, title_de" );
				database.AddAdvertisings( ads.Where( x => x.DataRoot == dataRoot ) );

				var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), "fake_last_name, fake_first_name, last_name" );
				database.AddPeople( people.Where( x => x.DataRoot == dataRoot ) );

				var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), "news_thread_id, news_type, title_de" );
				database.AddNews( news.Where( x => x.DataRoot == dataRoot ) );

				database.RefreshReferences();
			}

			var persister = new XmlPersisterV3();
			persister.SaveXML( database, "TVTDatabaseV3.xml", DatabaseVersion.V3, DataStructure.FakeData, false );
		}

	}
}
