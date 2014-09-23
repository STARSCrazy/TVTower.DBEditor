using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
				database.AddProgrammes( programmes.Where( x => (int)x.DataStatus >= (int)TVTDataStatus.OnlyDE ) );

				var ads = TVTCommandsV3.Read<TVTAdvertising>( connection, TVTCommandsV3.GetAdvertisingSQLDefinition(), "fake_title_de, title_de" );
				database.AddAdvertisings( ads );

				var people = TVTCommandsV3.Read<TVTPerson>( connection, TVTCommandsV3.GetPersonSQLDefinition(), "fake_last_name, fake_first_name, last_name" );
				database.AddPeople( people );

				var news = TVTCommandsV3.Read<TVTNews>( connection, TVTCommandsV3.GetNewsSQLDefinition(), "title_de" );
				database.AddNews( news );

				database.RefreshReferences();
			}
		}
	}
}
