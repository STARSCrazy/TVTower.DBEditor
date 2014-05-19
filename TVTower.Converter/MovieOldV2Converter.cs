using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;
using TVTower.Entities.Entities;

namespace TVTower.Converter
{
	public static class MovieOldV2Converter
	{
        public static void RefreshMovieDescriptions(ITVTDatabase database)
        {
            var programme = new List<ITVTProgrammeCore>();
            programme.AddRange(database.GetAllMovies(true));
            programme.AddRange(database.GetAllEpisodes());

            foreach (var person in database.GetAllPeople())
            {
                var foundProgrammes = programme.Where(x => x.Participants.Contains(person));
                foreach (var foundProgramme in foundProgrammes)
                {
                    var index = foundProgramme.Participants.IndexOf(person);

                    if (person.FullName != " " && foundProgramme.DescriptionDE.Contains(person.FullName))
                        foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace(person.FullName, string.Format("[{0}|Full]", index));

                    if (!string.IsNullOrEmpty(person.FirstName) && foundProgramme.DescriptionDE.Contains(person.FirstName))
                        foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace(person.FirstName, string.Format("[{0}|First]", index));

                    if (!string.IsNullOrEmpty(person.LastName) && foundProgramme.DescriptionDE.Contains(person.LastName))
                        foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace(person.FirstName, string.Format("[{0}|Last]", index));

                    if (!string.IsNullOrEmpty(person.NickName) && foundProgramme.DescriptionDE.Contains(person.NickName))
                        foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace(person.FirstName, string.Format("[{0}|Nick]", index));


                    if (person.FullName != " " && foundProgramme.DescriptionEN.Contains(person.FullName))
                        foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace(person.FullName, string.Format("[{0}|Full]", index));

                    if (!string.IsNullOrEmpty(person.FirstName) && foundProgramme.DescriptionEN.Contains(person.FirstName))
                        foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace(person.FirstName, string.Format("[{0}|First]", index));

                    if (!string.IsNullOrEmpty(person.LastName) && foundProgramme.DescriptionEN.Contains(person.LastName))
                        foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace(person.FirstName, string.Format("[{0}|Last]", index));

                    if (!string.IsNullOrEmpty(person.NickName) && foundProgramme.DescriptionEN.Contains(person.NickName))
                        foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace(person.FirstName, string.Format("[{0}|Nick]", index));
                }
            }
        }

        private static void ConvertEpisode(MovieOldV2 movieOldV2, ITVTProgrammeCore episode, ITVTDatabase database)
        {
            if (movieOldV2.custom)
                episode.DataType = TVTDataType.TVTower;
            else
                episode.DataType = TVTDataType.Custom;

            episode.GenerateGuid();
            episode.AltId = movieOldV2.id.ToString();
            episode.TitleDE = movieOldV2.title;
            episode.TitleEN = movieOldV2.titleEnglish;
            episode.FakeTitleDE = movieOldV2.titleFake;
            episode.FakeTitleEN = movieOldV2.titleEnglishFake;            
            episode.DescriptionDE = movieOldV2.description;
            episode.DescriptionEN = movieOldV2.descriptionEnglish;
            episode.Participants = GetPersonsByNameOrCreate(database, movieOldV2.actors, TVTDataContent.Original, TVTPersonFunction.Actor);
            episode.Director = GetPersonByNameOrCreate(database, movieOldV2.director, TVTDataContent.Original, TVTPersonFunction.Director);
            
            //episode.AdditionalInfo = movieOldV2.approved ? "Approved" : null;            
        }

		public static void Convert( List<MovieOldV2> moviesOldV2, ITVTDatabase database )
		{
			foreach ( var movSrc in moviesOldV2 )
			{
                if (movSrc.parentID == 0) //Film
                {
                    var programme = new TVTProgramme();
                    programme.ProgrammeType = TVTProgrammeType.Movie;
                    programme.Approved = movSrc.approved;

                    ConvertEpisode(movSrc, programme, database);

                    programme.PriceMod = ConvertOldToNewValue(movSrc.price);
                    programme.BoxOfficeRate = ConvertOldToNewValue(movSrc.outcome);

                    programme.Year = movSrc.year;
                    programme.Country = movSrc.country;
                    programme.Blocks = movSrc.blocks;

                    ConvertGenreAndFlags(programme, movSrc);

                    if (programme.Flags.Contains(TVTMovieFlag.Live) && movSrc.time > 0)
                        programme.LiveHour = movSrc.time;

                    if (movSrc.xrated)
                        programme.Flags.Add(TVTMovieFlag.FSK18);

                    database.AddMovie(programme);
                }
                else //Serien-Episode
                {
                    var episode = new TVTEpisode();
                    episode.Approved = movSrc.approved;
                    ConvertEpisode(movSrc, episode, database);

                    episode.Tag = movSrc.parentID.ToString();

                    episode.CriticsRate = ConvertOldToNewValue(movSrc.critics);
                    episode.ViewersRate = ConvertOldToNewValue(movSrc.speed);
                    episode.EpisodeIndex = movSrc.episode;

                    database.AddEpisode(episode);
                }
			}

			//Nachträglich die Parents auf IsSeries setzen
			var allMovies = database.GetAllMovies();
            foreach (var current in database.GetAllEpisodes())
			{
				var series = allMovies.FirstOrDefault( x => x.AltId.CompareTo( current.Tag ) == 0 );
                if (series != null)
                {
                    series.ProgrammeType = TVTProgrammeType.Series;
                    current.SeriesMaster = new CodeKnight.Core.WeakReference<TVTProgramme>(series);
                }
                else
                    current.DataStatus = TVTDataStatus.Incorrect;
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
