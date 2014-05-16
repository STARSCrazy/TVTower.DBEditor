using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;

namespace TVTower.Converter
{
	public static class MovieOldV2Converter
	{
		public static void Convert( List<MovieOldV2> moviesOldV2, ITVTDatabase database )
		{
            var seriesEpisodes = new List<TVTProgramme>();

			foreach ( var movSrc in moviesOldV2 )
			{
                TVTProgramme programme = new TVTProgramme();		

				if ( movSrc.parentID == 0 ) //Film
					programme.ProgrammeType = TVTProgrammeType.Movie;
				else //Serien-Episode       
                    programme.ProgrammeType = TVTProgrammeType.Episode;

                programme.DataContent = TVTDataContent.Undefined;
				if ( movSrc.custom )
                    programme.DataType = TVTDataType.TVTower;
				else
                    programme.DataType = TVTDataType.Custom;

                programme.GenerateGuid();
                programme.AltId = movSrc.id.ToString();
                programme.TitleDE = movSrc.title;
                programme.TitleEN = movSrc.titleEnglish;
                programme.DescriptionDE = movSrc.description;
                programme.DescriptionEN = movSrc.descriptionEnglish;
                programme.Participants = GetPersonsByNameOrCreate(database, movSrc.actors, TVTDataContent.Original, TVTPersonFunction.Actor);
                programme.Director = GetPersonByNameOrCreate(database, movSrc.director, TVTDataContent.Original, TVTPersonFunction.Director);

                programme.PriceMod = ConvertOldToNewValue(movSrc.price);
                programme.CriticsRate = ConvertOldToNewValue(movSrc.critics);
                programme.BoxOfficeRate = ConvertOldToNewValue(movSrc.outcome);
                programme.ViewersRate = ConvertOldToNewValue(movSrc.speed);

                programme.ApprovedDE = movSrc.approved;

				if ( programme.ProgrammeType == TVTProgrammeType.Movie ) //Film
				{
					programme.Year = movSrc.year;
					programme.Country = movSrc.country;
					programme.Blocks = movSrc.blocks;

					ConvertGenreAndFlags( programme, movSrc );

					if ( programme.Flags.Contains( TVTMovieFlag.Live ) && movSrc.time > 0 )
						programme.LiveHour = movSrc.time;

					if ( movSrc.xrated )
						programme.Flags.Add( TVTMovieFlag.FSK18 );

					database.AddMovie( programme );
				}
                else if (programme.ProgrammeType == TVTProgrammeType.Episode)
				{
                    programme.EpisodeIndex = movSrc.episode;
                    programme.Tag = movSrc.parentID.ToString();
                    seriesEpisodes.Add(programme);
				}
			}

			//Nachträglich die Parents auf IsSeries setzen
			var allMovies = database.GetAllMovies();
			foreach ( var current in seriesEpisodes )
			{
				var series = allMovies.FirstOrDefault( x => x.AltId.CompareTo( current.Tag ) == 0 );
                if (series != null)
                {
                    series.ProgrammeType = TVTProgrammeType.Series;
                    current.SeriesMaster = new CodeKnight.Core.WeakReference<TVTProgramme>(series);
                }
                else
                    current.Incorrect = true;
			}
		}

		public static int ConvertOldToNewValue( int oldValue )
		{
			return oldValue * 100 / 255;
		}

		public static void ConvertGenreAndFlags( TVTProgramme movie, MovieOldV2 movieOld )
		{
			switch ( movieOld.genre )
			{
				case 0:  //action
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Action;
					break;
				case 1:  //thriller
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Thriller;
					break;
				case 2:  //sci-fi
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.SciFi;
					break;
				case 3:  //comedy
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Comedy;
					break;
				case 4:  //horror
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Horror;
					break;
				case 5:  //love
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Romance;
					break;
				case 6:  //erotic
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Erotic;
					break;
				case 7:  //western
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Western;
					break;
				case 8:  //live
					movie.ProgrammeType = TVTProgrammeType.Event;
					movie.Flags.Add( TVTMovieFlag.Live );
					break;
				case 9:  //kidsmovie
					movie.MainGenre = TVTProgrammeGenre.Family;
					movie.TargetGroups.Add( TVTTargetGroup.Children );
					break;
				case 10:  //cartoon
					movie.MainGenre = TVTProgrammeGenre.Animation;
					movie.Flags.Add( TVTMovieFlag.Animation );
					break;
				case 11:  //music
					movie.ProgrammeType = TVTProgrammeType.Event;
					movie.MainGenre = TVTProgrammeGenre.Music;
					break;
				case 12:  //sport
					movie.ProgrammeType = TVTProgrammeType.Event;
                    movie.MainGenre = TVTProgrammeGenre.Sport;
					break;
				case 13:  //culture
					movie.MainGenre = TVTProgrammeGenre.Documentary;
					movie.Flags.Add( TVTMovieFlag.Culture );
					break;
				case 14:  //fantasy
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Fantasy;
					break;
				case 15:  //yellow press
					movie.ProgrammeType = TVTProgrammeType.Reportage;
                    movie.MainGenre = TVTProgrammeGenre.YellowPress;
					break;
				case 16:  //news
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.ProgrammeType = TVTProgrammeType.Reportage;
					break;
				case 17:  //show					
					movie.ProgrammeType = TVTProgrammeType.Show;
                    movie.MainGenre = TVTProgrammeGenre.Undefined_Show;
					break;
				case 18:  //monumental
                    movie.ProgrammeType = TVTProgrammeType.Movie;
					movie.MainGenre = TVTProgrammeGenre.Monumental;
					movie.Flags.Add( TVTMovieFlag.Cult );
					break;
				case 19:  //fillers
                    movie.ProgrammeType = TVTProgrammeType.Misc;
					movie.MainGenre = TVTProgrammeGenre.Undefined;
					movie.Flags.Add( TVTMovieFlag.Trash );
					break;
				case 20:  //paid programing
					movie.ProgrammeType = TVTProgrammeType.Commercial;
					movie.Flags.Add( TVTMovieFlag.Paid );
					break;
				default:
					throw new Exception();
			}
		}

		public static List<TVTPerson> GetPersonsByNameOrCreate( ITVTDatabase database, string names, TVTDataContent defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			var result = new List<TVTPerson>();
			if ( !string.IsNullOrEmpty( names ) )
			{
				var array = names.Split( ',' );
				foreach ( var aValue in array )
				{
					var personName = aValue.Trim();

					var person = GetPersonByNameOrCreate( database, personName, defaultStatus, functionForNew );
                    if (person != null)
					result.Add( person );
				}
			}
			return result;
		}

		public static TVTPerson GetPersonByNameOrCreate( ITVTDatabase database, string name, TVTDataContent defaultType, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			if ( !string.IsNullOrEmpty( name ) )
			{
				var tempName = name.Split( ',' )[0];
				var person = database.GetPersonByName( tempName );

				if ( person == null )
				{
					person = new TVTPerson();
					person.GenerateGuid();
					person.DataContent = defaultType;
					if ( defaultType == TVTDataContent.Fake || defaultType == TVTDataContent.FakeWithRefId )
						PersonConverter.ConvertFakeFullname( person, tempName );
					else
						PersonConverter.ConvertFullname( person, tempName );
					person.Functions.Add( functionForNew );
					database.AddPerson( person );
				}
				return person;
			}
			else
				return null;
		}
	}
}
