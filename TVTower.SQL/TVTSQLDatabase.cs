using System.Linq;
using MySql.Data.MySqlClient;
using TVTower.Entities;

namespace TVTower.SQL
{
	public class TVTSQLDatabase
	{
		private string connectionString;

		public TVTSQLDatabase( string connectionString )
		{
			this.connectionString = connectionString;
		}

		private MySqlConnection GetConnection()
		{
			var connection = new MySqlConnection( connectionString );
			connection.Open();
			return connection;
		}

		public void FillTVTDatabase( ITVTDatabase database )
		{
			using ( var connection = GetConnection() )
			{
				var programmes = TVTCommandsV3.Read<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), "master_id, episode_index, fake_title_de, title_de" );
				//database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );
				database.AddProgrammes( programmes );

				var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), "fake_title_de, title_de" );
				database.AddAdvertisings( ads );

				var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), "fake_last_name, fake_first_name, last_name" );
				database.AddPeople( people );

				var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), "title_de" );
				database.AddNews( news );

				database.RefreshReferences();
			}
		}

		public void WriteChangesToDatabase( ITVTDatabase database )
		{
			using ( var connection = GetConnection() )
			{
				{
					var newProgrammes = database.GetAllProgrammes( true, true ).Where( x => x.IsNew );
					TVTCommandsV3.Insert<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), newProgrammes );
					newProgrammes.ToList().ForEach( x => { x.IsNew = false; x.IsChanged = false; } );

					var changedProgrammes = database.GetAllProgrammes( true, true ).Where( x => x.IsChanged );
					TVTCommandsV3.Update<TVTProgramme>( connection, TVTCommandsV3.GetProgrammeSQLDefinition(), changedProgrammes );
					changedProgrammes.ToList().ForEach( x => { x.IsChanged = false; } );
				}

				{
					var newNews = database.GetAllNews().Where( x => x.IsNew );
					TVTCommandsV3.Insert<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), newNews );
					newNews.ToList().ForEach( x => { x.IsNew = false; x.IsChanged = false; } );

					var changedNews = database.GetAllNews().Where( x => x.IsChanged );
					TVTCommandsV3.Update<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), changedNews );
					changedNews.ToList().ForEach( x => { x.IsChanged = false; } );
				}

                {
                    var newAds = database.GetAllAdvertisings().Where( x => x.IsNew );
                    TVTCommandsV3.Insert<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), newAds );
                    newAds.ToList().ForEach( x => { x.IsNew = false; x.IsChanged = false; } );

                    var changedAds = database.GetAllAdvertisings().Where( x => x.IsChanged );
                    TVTCommandsV3.Update<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), changedAds );
                    changedAds.ToList().ForEach( x => { x.IsChanged = false; } );
                }

                {
                    var newPeople = database.GetAllPeople().Where( x => x.IsNew );
                    TVTCommandsV3.Insert<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), newPeople );
                    newPeople.ToList().ForEach( x => { x.IsNew = false; x.IsChanged = false; } );

                    var changedPeople = database.GetAllPeople().Where( x => x.IsChanged );
                    TVTCommandsV3.Update<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), changedPeople );
                    changedPeople.ToList().ForEach( x => { x.IsChanged = false; } );
                }

				//database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );
				//database.AddProgrammes( programmes );

				//var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), "fake_title_de, title_de" );
				//database.AddAdvertisings( ads );

				//var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), "fake_last_name, fake_first_name, last_name" );
				//database.AddPeople( people );

				//var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), "title_de" );
				//database.AddNews( news );

				//database.RefreshReferences();
			}
		}
	}
}
