using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeKnight.Core;
using MySql.Data.MySqlClient;
using TVTower.Entities;

namespace TVTower.SQL
{

	public class TVTCommandsV3
	{
		private static string GetListToSeperatedString<T>( List<T> list )
		{
			if ( list != null )
				return list.ToContentString();
			else
				return null;
		}



		public static void AddNamesBasicSQLDefinition<T>( SQLDefinition<T> definition )
			where T : ITVTNamesBasic
		{
			definition.Add( x => x.Id );
			definition.Add( x => x.TitleDE );
			definition.Add( x => x.TitleEN );
			definition.Add( x => x.DescriptionDE );
			definition.Add( x => x.DescriptionEN );
		}

		public static void AddNamesSQLDefinition<T>( SQLDefinition<T> definition )
			where T : ITVTNames
		{
			AddNamesBasicSQLDefinition( definition );
			definition.Add( x => x.FakeTitleDE );
			definition.Add( x => x.FakeTitleEN );
		}

		public static SQLDefinition<TVTProgramme> GetProgrammeSQLDefinition()
		{
			var definition = new SQLDefinition<TVTProgramme>();
			definition.Table = "tvt_programmes";

			AddNamesSQLDefinition( definition );

			definition.Add( x => x.FakeDescriptionDE );
			definition.Add( x => x.FakeDescriptionEN );

			definition.Add( x => x.ProgrammeType );
			definition.Add( x => x.Country );
			definition.Add( x => x.Year );

			definition.Add( x => x.MainGenre );
			definition.Add( x => x.SubGenre );

			definition.Add( x => x.Blocks );
			definition.Add( x => x.LiveHour );
			definition.Add( x => x.DistributionChannel );

			definition.Add( x => x.Flags );
			definition.Add( x => x.TargetGroups );
			definition.Add( x => x.ProPressureGroups );
			definition.Add( x => x.ContraPressureGroups );

			definition.Add( x => x.ImdbId );
			definition.Add( x => x.TmdbId );
			definition.Add( x => x.RottenTomatoesId );
			definition.Add( x => x.ImageUrl );

			//definition.Add( x => x.Director, null, "_id" );
			//definition.Add( x => x.Participants, "participant1_id", null, 0 );
			//definition.Add( x => x.Participants, "participant2_id", null, 1 );
			//definition.Add( x => x.Participants, "participant3_id", null, 2 );

			definition.Add( x => x.BettyBonus );
			definition.Add( x => x.PriceMod );
			definition.Add( x => x.CriticsRate );
			definition.Add( x => x.ViewersRate );
			definition.Add( x => x.BoxOfficeRate );

			//Zusatzinfos
			AdditionalFields( definition );

			definition.AddSubDefinition( x => x.Staff, GetStaffSQLDefinition() );

			return definition;
		}

		public static SQLDefinition<TVTEpisode> GetEpisodeSQLDefinition()
		{
			var definition = new SQLDefinition<TVTEpisode>();
			definition.Table = "tvt_episodes";

			AddNamesSQLDefinition( definition );

			definition.Add( x => x.FakeDescriptionDE );
			definition.Add( x => x.FakeDescriptionEN );

			//definition.Add( x => x.Director, null, "_id" );
			//definition.Add( x => x.Participants, "participant1_id", null, 0 );
			//definition.Add( x => x.Participants, "participant2_id", null, 1 );
			//definition.Add( x => x.Participants, "participant3_id", null, 2 );

			definition.Add( x => x.CriticsRate );
			definition.Add( x => x.ViewersRate );

			//definition.Add(x => x.SeriesMaster, "series_id");
			definition.Add( x => x.SeriesId, "series_id" );
			definition.Add( x => x.EpisodeIndex );

			//Zusatzinfos
			AdditionalFields( definition );

			definition.AddSubDefinition( x => x.Staff, GetStaffSQLDefinition() );

			return definition;
		}

		public static SQLDefinition<TVTAdvertising> GetAdvertisingSQLDefinition()
		{
			var definition = new SQLDefinition<TVTAdvertising>();
			definition.Table = "tvt_advertisings";

			AddNamesSQLDefinition( definition );

			definition.Add( x => x.FakeDescriptionDE );
			definition.Add( x => x.FakeDescriptionEN );

			definition.Add( x => x.Infomercial );
			definition.Add( x => x.Quality );

			definition.Add( x => x.FixPrice );
			definition.Add( x => x.MinAudience );
			definition.Add( x => x.MinImage );
			definition.Add( x => x.Repetitions );
			definition.Add( x => x.Duration );
			definition.Add( x => x.Profit );
			definition.Add( x => x.Penalty );
			definition.Add( x => x.TargetGroup );

			definition.Add( x => x.AllowedGenres );
			definition.Add( x => x.ProhibitedGenres );
			definition.Add( x => x.AllowedProgrammeTypes );
			definition.Add( x => x.ProhibitedProgrammeTypes );

			definition.Add( x => x.ProPressureGroups );
			definition.Add( x => x.ContraPressureGroups );

			//Zusatzinfos
			AdditionalFields( definition );

			return definition;
		}

		public static SQLDefinition<TVTPerson> GetPersonSQLDefinition()
		{
			var definition = new SQLDefinition<TVTPerson>();
			definition.Table = "tvt_people";

			//AddEntityBaseSQLDefinition(definition);

			definition.Add( x => x.Id );

			definition.Add( x => x.FirstName );
			definition.Add( x => x.LastName );
			definition.Add( x => x.NickName );

			definition.Add( x => x.FakeFirstName );
			definition.Add( x => x.FakeLastName );
			definition.Add( x => x.FakeNickName );

			definition.Add( x => x.TmdbId );
			definition.Add( x => x.ImdbId );
			definition.Add( x => x.ImageUrl );

			definition.Add( new SQLDefinitionFieldList<TVTPersonFunction>( PInfo<TVTPerson>.Info( x => x.Functions, false ) ) );
			//definition.Add(x => x.Functions);
			definition.Add( x => x.Gender );

			definition.Add( x => x.Birthday );
			definition.Add( x => x.Deathday );
			definition.Add( x => x.Country );

			definition.Add( x => x.Prominence );
			definition.Add( x => x.Skill );
			definition.Add( x => x.Fame );
			definition.Add( x => x.Scandalizing );
			definition.Add( x => x.PriceMod );

			definition.Add( x => x.Power );
			definition.Add( x => x.Humor );
			definition.Add( x => x.Charisma );
			definition.Add( x => x.Appearance );

			definition.Add( x => x.TopGenre1 );
			definition.Add( x => x.TopGenre2 );

			definition.Add( x => x.ProgrammeCount );

			//Zusatzinfos
			AdditionalFields( definition );

			return definition;
		}

		public static SQLDefinition<TVTNewsEffect> GetNewsEffectSQLDefinition()
		{
			var definition = new SQLDefinition<TVTNewsEffect>();
			definition.Table = "tvt_news_effects";
			definition.OwnerIdField = "news_id";

			definition.Add( x => x.Id );
			definition.Add( x => x.Type );
			definition.Add( x => x.EffectParameters, "value1", null, 0 );
			definition.Add( x => x.EffectParameters, "value2", null, 1 );
			definition.Add( x => x.EffectParameters, "value3", null, 2 );

			return definition;
		}

		public static SQLDefinition<TVTStaff> GetStaffSQLDefinition()
		{
			var definition = new SQLDefinition<TVTStaff>();
			definition.Table = "tvt_staff";
			definition.OwnerIdField = "owner_id";

			definition.Add( x => x.Id );
			definition.Add( x => x.Function );
			definition.Add( x => x.Person, "person_id" );
			definition.Add( x => x.Index, "sortindex" );

			return definition;
		}

		public static SQLDefinition<TVTNews> GetNewsSQLDefinition()
		{
			var definition = new SQLDefinition<TVTNews>();
			definition.Table = "tvt_news";

			AddNamesBasicSQLDefinition( definition );

			definition.Add( x => x.NewsType );
			definition.Add( x => x.NewsThreadId );
			definition.Add( x => x.Genre );

			definition.Add( x => x.Price );
			definition.Add( x => x.Topicality );

			definition.Add( x => x.FixYear );
			definition.Add( x => x.AvailableAfterDays );
			definition.Add( x => x.YearRangeFrom );
			definition.Add( x => x.YearRangeTo );
			definition.Add( x => x.MinHoursAfterPredecessor );
			definition.Add( x => x.TimeRangeFrom );
			definition.Add( x => x.TimeRangeTo );

			definition.Add( x => x.Resource1Type );
			definition.Add( x => x.Resource2Type );
			definition.Add( x => x.Resource3Type );
			definition.Add( x => x.Resource4Type );

			//definition.Add( x => x.Effects );

			definition.Add( x => x.ProPressureGroups );
			definition.Add( x => x.ContraPressureGroups );

			//Zusatzinfos
			AdditionalFields( definition );

			definition.AddSubDefinition( x => x.Effects, GetNewsEffectSQLDefinition() );


			//definition.AfterInsert = new Action<MySqlConnection, TVTNews>( ( x, y ) =>
			//{

			//});





			//var definition = GetNewsSQLDefinition();

			//var sqlCommandText = "INSERT INTO tvt_news (" + definition.GetFieldNames( ',' ) + ") VALUES (" + definition.GetFieldNames( ',', "?" ) + ")";

			//foreach ( var aNews in news )
			//{
			//    var command = connection.CreateCommand();
			//    command.CommandText = sqlCommandText;
			//    var enumerator = definition.GetEnumerator();
			//    while ( enumerator.MoveNext() )
			//    {
			//        var field = enumerator.Current;
			//        command.Parameters.AddWithValue( "?" + field.FieldName, field.GetValue( aNews ) );
			//    }
			//    command.ExecuteNonQuery();

			//    if ( definition.AfterInsert != null )
			//        definition.AfterInsert( connection, aNews );
			//}



			return definition;
		}

		//private static List<T> ReadGeneric<T>( MySqlConnection connection, string commandText, SQLDefinition<T> definition )
		//{
		//    var result = new List<T>();

		//    var command = connection.CreateCommand();
		//    command.CommandText = commandText;
		//    var Reader = command.ExecuteReader();
		//    try
		//    {
		//        while ( Reader.Read() )
		//        {
		//            var entity = Activator.CreateInstance<T>();

		//            var enumerator = definition.GetEnumerator();
		//            while ( enumerator.MoveNext() )
		//            {
		//                var field = enumerator.Current;
		//                field.Read( Reader, entity );
		//            }

		//            result.Add( entity );
		//        }
		//    }
		//    finally
		//    {
		//        if ( Reader != null && !Reader.IsClosed )
		//            Reader.Close();
		//    }

		//    return result;
		//}

		public static List<T> Read<T>( MySqlConnection connection, SQLDefinition<T> definition, string orderBy = null ) where T : IIdEntity
		{
			var result = new List<T>();

			var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM " + definition.Table;
			if ( !string.IsNullOrEmpty( orderBy ) )
				command.CommandText = command.CommandText + " ORDER BY " + orderBy;

			var Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var entity = Activator.CreateInstance<T>();

					var enumerator = definition.GetEnumerator();
					while ( enumerator.MoveNext() )
					{
						var field = enumerator.Current;
						field.Read( Reader, entity );
					}

					result.Add( entity );
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}


			if ( definition.SubDefinitions.Count > 0 )
			{
				foreach ( var subDef in definition.SubDefinitions )
				{
					var readSubMethod = typeof( TVTCommandsV3 ).GetMethod( "ReadSubDefinition" );
					var readMethodGeneric = readSubMethod.MakeGenericMethod( subDef.Key.PropertyType.GetGenericArguments()[0], typeof( T ) );
					readMethodGeneric.Invoke( null, new object[] { connection, subDef.Value, subDef.Key, result } );
				}
			}

			return result;
		}

		public static void ReadSubDefinition<T, TOwner>( MySqlConnection connection, SQLDefinition<T> subDefinition, PropertyInfo ownerPropInfo, List<TOwner> possibleOwner )
			where T : IIdEntity
			where TOwner : IIdEntity
		{
			Action<MySqlDataReader, object> ownerReadAction = ( r, x ) =>
			{
				var ownerIdTemp = r[subDefinition.OwnerIdField];
				var ownerId = Guid.Parse( ownerIdTemp.ToString() );

				var owner = possibleOwner.FirstOrDefault( z => z.Id == ownerId );
				if ( owner != null )
				{
					var list = (List<T>)ownerPropInfo.GetValue( owner, null );
					list.Add( (T)x );
				}
			};

			var fieldDef = subDefinition.GetFieldDefinition( subDefinition.OwnerIdField );
			if ( fieldDef == null )
				subDefinition.Add( new SQLDefinitionFieldFunc( subDefinition.OwnerIdField, null, ownerReadAction ) );
			else
				(fieldDef as SQLDefinitionFieldFunc).ReadAction = ownerReadAction;

			Read<T>( connection, subDefinition );

			//var readMethod = typeof( TVTCommandsV3 ).GetMethod( "Read" );
			//var readMethodGeneric = readMethod.MakeGenericMethod( subDef.Key.PropertyType.GetGenericArguments()[0] );
			//readMethodGeneric.Invoke( null, new object[] { connection, subDefinition } );
		}

		public static void Insert<T>( MySqlConnection connection, SQLDefinition<T> definition, IEnumerable<T> elements ) where T : IIdEntity
		{
			var sqlCommandText = "INSERT INTO " + definition.Table + " (" + definition.GetFieldNames( ',' ) + ") VALUES (" + definition.GetFieldNames( ',', "?" ) + ")";

			foreach ( var element in elements )
			{
				var command = connection.CreateCommand();
				command.CommandText = sqlCommandText;
				var enumerator = definition.GetEnumerator();
				while ( enumerator.MoveNext() )
				{
					var field = enumerator.Current;
					command.Parameters.AddWithValue( "?" + field.FieldName, field.GetValue( element ) );
				}
				command.ExecuteNonQuery();

				if ( definition.SubDefinitions.Count > 0 )
				{
					foreach ( var subDef in definition.SubDefinitions )
					{
						var subDefinition = subDef.Value;

						var fieldDef = subDefinition.GetFieldDefinition( subDefinition.OwnerIdField );
						if ( fieldDef == null )
							subDefinition.Add( new SQLDefinitionFieldFunc( subDefinition.OwnerIdField, x => { return element.Id.ToString(); } ) );
						else
							(fieldDef as SQLDefinitionFieldFunc).GetValueFunc = x => { return element.Id.ToString(); };

						var propertyValue = subDef.Key.GetValue( element, null );
						var insertMethod = typeof( TVTCommandsV3 ).GetMethod( "Insert" );
						var insertMethodGeneric = insertMethod.MakeGenericMethod( subDef.Key.PropertyType.GetGenericArguments()[0] );
						insertMethodGeneric.Invoke( null, new object[] { connection, subDefinition, propertyValue } );
					}
				}
			}
		}

		private static void AdditionalFields<T>( SQLDefinition<T> definition )
			where T : TVTEntity
		{
			definition.Add( x => x.DataType );
			definition.Add( x => x.DataStatus );
			definition.Add( x => x.Approved );
			definition.Add( x => x.AdditionalInfo );

			definition.Add( x => x.CreatorId );
			definition.Add( x => x.EditorId );
			definition.Add( x => x.LastModified );
		}
	}
}
