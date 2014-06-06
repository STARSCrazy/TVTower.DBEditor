using System;
using System.Collections.Generic;
using System.Linq;
using CherryTomato.Entities;
using TVTower.Entities;
using WatTmdb.V3;
using TVTower.Converter;

namespace TVTower.Import
{
	public class MovieImporter
	{
		public ITVTDatabase Database { get; set; }
		//public SortedBindingList<TVTMovieExtended> MovieSource { get; set; }
		//public SortedBindingList<TVTPerson> PersonSource { get; set; }

		public List<TVTProgramme> Result { get; set; }


		public Dictionary<int, TmdbMovie> ImdbMovies { get; set; }
		public Dictionary<int, TomatoMovie> RottenTomatoeMovies { get; set; }

		public MovieImporter()
		{
			ImdbMovies = new Dictionary<int, TmdbMovie>();
			RottenTomatoeMovies = new Dictionary<int, TomatoMovie>();
		}

		public TmdbMovie GetMovieResult( TVTProgramme movie )
		{
			if ( movie.TmdbId.HasValue )
				return ImdbMovies[movie.TmdbId.Value];
			else
				return null;
		}

		// Volatile is used as hint to the compiler that this data
		// member will be accessed by multiple threads.
		private volatile bool _shouldStop;

		// Declare the delegate (if using non-generic pattern).
		public delegate void FinishEventHandler( object sender, EventArgs e );

		// Declare the event.
		public event FinishEventHandler FinishEvent;

		public void Start()
		{
			var fillMovies = new FillMovieExtended();

			List<MovieResult> movieExport = new List<MovieResult>();

			ServiceApi.InitializeApis();

			//for ( var year = 1954; year <= 1955; year++ )
			for ( var year = 1954; year <= 1959; year++ )
			//for ( var year = 1920; year <= 2013; year++ ) //Kein Film vor 1920 gefunden
			{
				if ( _shouldStop )
					break;

				Console.WriteLine( "Import year '{0}'", year );

				var maxpage = 1;
				var movieCollection = new List<MovieResult>();

				for ( var page = 1; page <= maxpage; page++ )
				{
					Console.WriteLine( "Import movies '{0}'", page );

					RequestChecker.TmDbReadyOrWait();
					var movieList = ServiceApi.TmdbApi.DiscoverMovie( page, "DE", true, null, year, 10, 2, "DE", "A" );

					movieCollection.AddRange( movieList.results );

					maxpage = movieList.total_pages;
				}
				var sortedList = movieCollection.OrderByDescending( x => x.popularity );

				if ( year < 1950 )
					movieExport.AddRange( sortedList.Take( 3 ) );
				else if ( year < 1965 )
					movieExport.AddRange( sortedList.Take( 5 ) );
				else if ( year < 1980 )
					movieExport.AddRange( sortedList.Take( 8 ) );
				else if ( year < 1990 )
					movieExport.AddRange( sortedList.Take( 12 ) );
				else
					movieExport.AddRange( sortedList.Take( 25 ) );
			}



			var resultList = GenerateTVTMovies( movieExport );
			foreach ( var entry in resultList )
			{
				Console.WriteLine( "Add result '{0}'", entry.TitleDE );
				MovieStatistics.Add( entry.Year, entry.MovieAdditional.Budget, entry.MovieAdditional.Revenue );
			}

			foreach ( var entry in resultList )
			{
				Console.WriteLine( "Calc rating '{0}'", entry.TitleDE );
				if ( !_shouldStop )
				{
					fillMovies.calcRating( this, entry );
				}
				Database.AddProgramme( entry );
			}

			Result = resultList;

			if ( FinishEvent != null )
				FinishEvent( this, new EventArgs() );
		}

		public void RequestStop()
		{
			_shouldStop = true;
		}

		private List<TVTProgramme> GenerateTVTMovies( List<MovieResult> TmdbList )
		{
			var result = new List<TVTProgramme>();
			var filler = new FillMovieExtended();

			foreach ( var entry in TmdbList )
			{
				var movie = new TVTProgramme();
				Console.WriteLine( "Load details '{0}'", entry.title );
				filler.LoadDetailsFromTmDB( this, movie, entry );
				result.Add( movie );
			}
			return result;
		}

		public TVTPerson RegisterActor( Cast cast )
		{
			var person = Database.GetPersonByTmdbId( cast.id );

			if ( person == null )
			{
				person = new TVTPerson();
                PersonConverter.ConvertFullname(person, cast.name);
				person.TmdbId = cast.id;
				person.Functions.Add(TVTPersonFunction.Actor);
                person.AdditionalInfo = cast.character;

				RegisterPerson( person, cast.id );

				Database.AddPerson(person );
			}

			return person;
		}

		public TVTPerson RegisterDirector( Crew crew )
		{
			var person = Database.GetPersonByTmdbId( crew.id );

			if ( person == null )
			{
				person = new TVTPerson();
                PersonConverter.ConvertFullname(person, crew.name);
				person.TmdbId = crew.id;
				person.Functions.Add(TVTPersonFunction.Director);
                person.AdditionalInfo = crew.job + "/" + crew.department;

				RegisterPerson( person, crew.id );

				Database.AddPerson( person );
			}

			return person;
		}

		private void RegisterPerson( TVTPerson person, int id )
		{
			RequestChecker.TmDbReadyOrWait();
			var personInfo = ServiceApi.TmdbApi.GetPersonInfo( id );
			person.ImdbId = personInfo.imdb_id;
			person.ImageUrl = @"https://d3gtl9l2a4fn1j.cloudfront.net/t/p/original" + personInfo.profile_path;
			person.Birthday = personInfo.birthday;
			person.Deathday = personInfo.deathday;
			//person.PlaceOfBirth = personInfo.place_of_birth;
			person.Country = GetCountryCode( GetCountry( personInfo.place_of_birth ) );
			//person.MovieRegistrations++;
		}

		private string GetCountry( string placeOfBirth )
		{
			if ( placeOfBirth.Contains( "USA" ) || placeOfBirth.Contains( "U.S." ) )
				return "USA";
			else if ( placeOfBirth.Contains( ", " ) )
				return placeOfBirth.Split( ',' ).Last().Trim();
			else if ( placeOfBirth.Contains( "- " ) )
				return placeOfBirth.Split( '-' ).Last().Trim();
			else if ( placeOfBirth.Contains( " " ) )
				return placeOfBirth.Split( ' ' ).Last().Trim();
			else
				return placeOfBirth.Trim();
		}

		private string GetCountryCode( string country )
		{
			switch ( country.ToLower() )
			{
				case "u.s.":
				case "usa":
				case "new york":
				case "united states":
					return "US";
				case "china":
					return "CN";
				case "japan":
					return "JP";
				case "england":
				case "uk":
					return "GB";
				case "ottoman empire":
					return "TR";
				case "puerto rico":
					return "PR";
				case "canada":
				case "british columbia":
					return "CA";
				case "italy":
					return "IT";
				case "austria-hungary":
					return "AT";
				case "germany":
					return "DE";
				case "palestine":
					return "PS";
				case "russia":
					return "RU";
				case "sweden":
					return "SE";
			}
			return country;
		}

		public TmdbMovie GetTmdbDetails( TVTProgramme movie )
		{
			if ( !movie.TmdbId.HasValue )
				throw new Exception();

			if ( ImdbMovies.ContainsKey( movie.TmdbId.Value ) )
				return ImdbMovies[movie.TmdbId.Value];
			else
			{
				RequestChecker.TmDbReadyOrWait();
				var tmdbMovieDetails = ServiceApi.TmdbApi.GetMovieInfo( movie.TmdbId.Value, "de" );
				ImdbMovies.Add( movie.TmdbId.Value, tmdbMovieDetails );
				return tmdbMovieDetails;
			}
		}

		public TomatoMovie GetTomatoeDetails( TVTProgramme movie )
		{
			if ( movie.RottenTomatoesId.HasValue && RottenTomatoeMovies.ContainsKey( movie.RottenTomatoesId.Value ) )
				return RottenTomatoeMovies[movie.RottenTomatoesId.Value];
			else
			{
				var imdbClean = movie.ImdbId;
				if ( imdbClean.StartsWith( "tt" ) )
					imdbClean = imdbClean.Substring( 2 );
				var tomatoeDetails = ServiceApi.TomatoApi.FindMovieByImdbId( imdbClean );
				RottenTomatoeMovies.Add( tomatoeDetails.RottenTomatoesId, tomatoeDetails );
				return tomatoeDetails;
			}
		}


	}
}
