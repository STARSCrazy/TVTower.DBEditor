using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;
using WatTmdb.V3;
using TVTower.Import;

namespace TVTower.Import
{
	public class FillMovieExtended
	{
		public void LoadDetailsFromTmDB( MovieImporter importer, TVTMovie movie, MovieResult tmdbMovie )
		{
			movie.TmdbId = tmdbMovie.id;
			movie.Name.OriginalTitleDE = tmdbMovie.title;
			movie.Name.OriginalTitleEN = tmdbMovie.original_title;

			var tmdbMovieDetails = importer.GetTmdbDetails( movie );
			movie.ImdbId = tmdbMovieDetails.imdb_id;

			var tomatoeDetails = importer.GetTomatoeDetails( movie );
			movie.RottenTomatoesId = tomatoeDetails.RottenTomatoesId;




			RequestChecker.TmDbReadyOrWait();
			var tmdbMovieCast = ServiceApi.TmdbApi.GetMovieCast( tmdbMovie.id );





			movie.Name.OriginalTitleDE = tmdbMovie.title;
			movie.Name.OriginalTitleEN = tmdbMovieDetails.original_title;
			movie.Name.DescriptionMovieDB = tmdbMovieDetails.tagline;
			movie.TmdbId = tmdbMovie.id;
			movie.ImdbId = tmdbMovieDetails.imdb_id;
			movie.ImageUrl = @"https://d3gtl9l2a4fn1j.cloudfront.net/t/p/original" + tmdbMovieDetails.backdrop_path;
			movie.MovieAdditional.Budget = tmdbMovieDetails.budget;
			movie.MovieAdditional.Revenue = tmdbMovieDetails.revenue;


			var castCounter = 1;

			movie.Actors = new List<TVTPerson>();
			foreach ( var cast in tmdbMovieCast.cast )
			{
				if ( castCounter > 3 )
					break;

				var person = importer.RegisterActor( cast );
				if ( person != null )
					movie.Actors.Add( person );

				castCounter++;
			}

			foreach ( var crew in tmdbMovieCast.crew )
			{
				if ( crew.department != "Directing" )
					break;

				var person = importer.RegisterDirector( crew );
				movie.Director = person;

				break;
			}

			foreach ( var currCountry in tmdbMovieDetails.production_countries )
			{
				if ( movie.Country == null )
					movie.Country = currCountry.iso_3166_1;
				else
					movie.Country = currCountry.iso_3166_1 + ", " + currCountry.iso_3166_1;
			}

			movie.Year = DateTime.Parse( tmdbMovieDetails.release_date ).Year;

			var genreChecker = new GenreChecker();
			movie.MainGenre = genreChecker.GetGenre( tmdbMovieDetails.genres );
			//TODO: Subgenre

			var runtime = tmdbMovieDetails.runtime;

			if ( runtime == 0 )
			{
				if ( tomatoeDetails != null && tomatoeDetails.Runtime.HasValue )
					runtime = tomatoeDetails.Runtime.Value;
				else
					runtime = 120; //Dann 2 Blocks
			}

			if ( runtime < 85 )
				movie.Blocks = 1;
			else if ( runtime < 145 )
				movie.Blocks = 2;
			else if ( runtime < 205 )
				movie.Blocks = 3;
			else if ( runtime < 265 )
				movie.Blocks = 4;
			else
				movie.Blocks = 5;

			//priceRate = 10;
			//criticRate = 11;
			//speedRate = 12;
			//boxOfficeRate = 13;
		}

		public void calcRating( MovieImporter importer, TVTMovie movie )
		{
			var tmdbMovieDetails = importer.GetTmdbDetails( movie );
			var tomatoeDetails = importer.GetTomatoeDetails( movie );


			if ( tomatoeDetails == null )
			{
				//TODO was machen
				return;
			}

			var reviewerRate = tomatoeDetails.Ratings.First( x => x.Type == "critics_score" );
			var audienceRate1 = tomatoeDetails.Ratings.First( x => x.Type == "audience_score" );
			var audienceRate2 = tmdbMovieDetails.vote_average * 10;
			var audiencePopularity = tmdbMovieDetails.popularity * 20;

			//CriticRate = Kritikerwertung
			movie.CriticsRate = Convert.ToInt32( reviewerRate.Score * 2.55 );

			//SpeedRate = Nutzerwertung (+ Popularität)
			movie.ViewersRate = Convert.ToInt32( (audienceRate1.Score * 0.45 + audienceRate2 * 0.45 + audiencePopularity * 0.1) * 2.55 );

			//Kinokasse = Einnahmen usw. und Popularität (wenn Populär dann haben es auch meist viele gesehen)
			int boxOfficeFallback = 0;
			if ( tmdbMovieDetails.revenue > 0 && tmdbMovieDetails.budget > 0 )
			{
				var benefit = ((tmdbMovieDetails.revenue / tmdbMovieDetails.budget) - 1) * 20;
				if ( benefit > 100 )
					benefit = 100;
				else if ( benefit < 0 )
					benefit = 0;

				boxOfficeFallback = Convert.ToInt32( (benefit * 0.8 + audiencePopularity * 0.2) * 2.55 );
			}
			else
				boxOfficeFallback = 75 + Convert.ToInt32( (audienceRate1.Score * 0.1 + audiencePopularity * 0.9) * 1.5 );


			var topRevenue = MovieStatistics.TopRevenue( movie.Year );
			if ( topRevenue.HasValue && tmdbMovieDetails.revenue > 0 )
			{
				var boxOfficeTemp = tmdbMovieDetails.revenue / (topRevenue.Value * 0.9);
				movie.BoxOfficeRate = Convert.ToInt32( (boxOfficeTemp * 0.8 + 1 * 0.2) * 255 );
				movie.BoxOfficeRate = Convert.ToInt32( movie.BoxOfficeRate * 0.8 + boxOfficeFallback * 0.2 );
			}
			else
				movie.BoxOfficeRate = boxOfficeFallback;
		}

		public int Version { get; set; }
	}
}
