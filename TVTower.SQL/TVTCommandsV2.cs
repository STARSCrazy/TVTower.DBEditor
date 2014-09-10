using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using TVTower.Converter;
using TVTower.Entities;

namespace TVTower.SQL
{
	public class TVTCommandsV2
	{
		public static List<MovieOldV2> LoadMoviesOldV2( MySqlConnection connection )
		{
			var result = new List<MovieOldV2>();

			var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvt_filme";
			var Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var reader = new SQLReaderOldV2( Reader );
					var movie = new MovieOldV2();

					movie.id = reader.GetInt( "id" );
					movie.title = reader.GetString( "title" );
					movie.titleEnglish = reader.GetString( "titleEnglish" );
					movie.description = reader.GetString( "description" );
					movie.descriptionEnglish = reader.GetString( "descriptionEnglish" );
					movie.actors = reader.GetString( "actors" );
					movie.director = reader.GetString( "director" );
					movie.price = reader.GetInt( "price" );
					movie.year = reader.GetInt( "year" );
					movie.country = reader.GetString( "country" );
					movie.genre = reader.GetInt( "genre" );
					movie.blocks = reader.GetInt( "blocks" );
					movie.critics = reader.GetInt( "critics" );
					movie.outcome = reader.GetInt( "outcome" );
					movie.speed = reader.GetInt( "speed" );
					movie.xrated = reader.GetBool( "xrated" );
					movie.parentID = reader.GetInt( "parentID" );
					movie.episode = reader.GetInt( "episode" );
					movie.approved = reader.GetBool( "approved" );
					movie.time = reader.GetInt( "time" );
					movie.creatorID = reader.GetString( "creatorID" );
					movie.editorID = reader.GetString( "editorID" );
					movie.deleted = reader.GetBool( "deleted" );
					movie.custom = reader.GetBool( "custom" );

					result.Add( movie );
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}

			//var fakes = new Dictionary<string, string>();

			//Fakes
			command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvt_convert WHERE type = 'title'";
			Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var oldValue = Reader.GetString( "old" ).Trim();
					var newValue = Reader.GetString( "new" ).Trim();

					var foundMovies = result.Where( x => x.title == oldValue );
					foreach ( var movie in foundMovies )
					{
						movie.titleFake = newValue;
					}

					foundMovies = result.Where( x => x.titleEnglish == oldValue );
					foreach ( var movie in foundMovies )
					{
						movie.titleEnglishFake = newValue;
					}

					foundMovies = result.Where( x => string.IsNullOrEmpty( x.titleFake ) && x.title.Contains( oldValue ) );
					foreach ( var movie in foundMovies )
					{
						movie.titleFake = movie.title.Replace( oldValue, newValue );
					}

					foundMovies = result.Where( x => string.IsNullOrEmpty( x.titleEnglishFake ) && x.titleEnglish.Contains( oldValue ) );
					foreach ( var movie in foundMovies )
					{
						movie.titleEnglishFake = movie.titleEnglish.Replace( oldValue, newValue );
					}
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}

			return result;
		}

		public static List<AdvertisingOldV2> LoadAdsOldV2( MySqlConnection connection )
		{
			var result = new List<AdvertisingOldV2>();

			var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvtower.tvt_werbevertraege";
			var Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var reader = new SQLReaderOldV2( Reader );
					var ad = new AdvertisingOldV2();

					ad.id = reader.GetInt( "id" );
					ad.title = reader.GetString( "title" );
					ad.titleEnglish = reader.GetString( "titleEnglish" );
					ad.description = reader.GetString( "description" );
					ad.descriptionEnglish = reader.GetString( "descriptionEnglish" );

					ad.minAudience = reader.GetInt( "minAudience" );
					ad.minImage = reader.GetInt( "minImage" );
					ad.repetitions = reader.GetInt( "repetitions" );
					ad.fixedPrice = reader.GetInt( "fixedPrice" );
					ad.fixedProfit = reader.GetInt( "fixedProfit" );
					ad.fixedPenalty = reader.GetInt( "fixedPenalty" );
					ad.profit = reader.GetInt( "profit" );
					ad.penalty = reader.GetInt( "penalty" );
					ad.targetgroup = reader.GetInt( "targetgroup" );
					ad.duration = reader.GetInt( "duration" );
					ad.approved = reader.GetBool( "approved" );
					ad.creatorID = reader.GetString( "creatorID" );
					ad.editorID = reader.GetString( "editorID" );
					ad.custom = reader.GetBool( "custom" );
					ad.deleted = reader.GetBool( "deleted" );

					result.Add( ad );
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}

			return result;
		}

		public static List<NewsOldV2> LoadNewsOldV2( MySqlConnection connection )
		{
			var result = new List<NewsOldV2>();

			var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvtower.tvt_nachrichten";
			var Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var reader = new SQLReaderOldV2( Reader );
					var news = new NewsOldV2();

					news.id = reader.GetInt( "id" );
					//news.title = reader.GetString("title");
					//news.titleEnglish = reader.GetString("titleEnglish");
					//news.description = reader.GetString("description");
					//news.descriptionEnglish = reader.GetString("descriptionEnglish");

					news.genre = reader.GetInt( "genre" );
					news.price = reader.GetInt( "price" );
					news.topicality = reader.GetInt( "topicality" );
					news.parentID = reader.GetInt( "parentID" );
					news.approved = reader.GetBool( "approved" );
					news.creatorID = reader.GetInt( "creatorID" );
					news.editorID = reader.GetInt( "editorID" );
					news.episode = reader.GetInt( "episode" );

					result.Add( news );
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}

			command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvtower.tvt_nachrichten_lang";
			Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var reader = new SQLReaderOldV2( Reader );

					var id = reader.GetInt( "news_id" );

					var news = result.FirstOrDefault( x => x.id == id );

					news.title = reader.GetString( "title" );
					news.description = reader.GetString( "text" );
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}



			return result;
		}



		public static void LoadFakesForPeople( MySqlConnection connection, IEnumerable<TVTPerson> people )
		{
			var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tvt_convert WHERE type = 'actor'";
			var Reader = command.ExecuteReader();
			try
			{
				while ( Reader.Read() )
				{
					var oldValue = Reader.GetString( "old" );
					var newValue = Reader.GetString( "new" );

					var foundPeople = people.Where( x => x.FullName == oldValue );
					foreach ( var person in foundPeople )
					{
						PersonConverter.ConvertFakeFullname( person, newValue );
					}

					foundPeople = people.Where( x => x.FakeFullName == " " && x.FullName.Contains( oldValue ) );
					foreach ( var person in foundPeople )
					{
						PersonConverter.ConvertFakeFullname( person, newValue );
					}
				}
			}
			finally
			{
				if ( Reader != null && !Reader.IsClosed )
					Reader.Close();
			}
		}
	}
}
