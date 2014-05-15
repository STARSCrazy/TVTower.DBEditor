using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;

namespace TVTower.Converter
{
    public static class MovieOldV2Converter
    {
        public static void Convert(List<MovieOldV2> moviesOldV2, ITVTDatabase database)
        {
            var seriesEpisodes = new List<TVTEpisode>();

            foreach (var movSrc in moviesOldV2)
            {
                TVTProgramme programme = null;
                TVTEpisode episode = null;

                if (movSrc.parentID == 0) //Film
                {
                    programme = new TVTProgramme();
                    programme.ProgrammeType = TVTProgrammeType.Movie;
                    episode = programme;
                }
                else //Serien-Episode       
                {
                    episode = new TVTEpisode();
                }

                episode.DataContent = TVTDataContent.Undefined;
                if (movSrc.custom)
                    episode.DataType = TVTDataType.TVTower;
                else
                    episode.DataType = TVTDataType.Custom;

                episode.GenerateGuid();
                episode.AltId = movSrc.id.ToString();
                episode.TitleDE = movSrc.title;
                episode.TitleEN = movSrc.titleEnglish;
                episode.DescriptionDE = movSrc.description;
                episode.DescriptionEN = movSrc.descriptionEnglish;
                episode.Participants = GetPersonsByNameOrCreate(database, movSrc.actors, TVTDataContent.Original, TVTPersonFunction.Actor);
                episode.Director = GetPersonByNameOrCreate(database, movSrc.director, TVTDataContent.Original, TVTPersonFunction.Director);

                episode.PriceMod = ConvertOldToNewValue(movSrc.price);
                episode.CriticsRate = ConvertOldToNewValue(movSrc.critics);
                episode.BoxOfficeRate = ConvertOldToNewValue(movSrc.outcome);
                episode.ViewersRate = ConvertOldToNewValue(movSrc.speed);             
   
                episode.ApprovedDE = movSrc.approved;

                if (programme != null) //Film
                {
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
                else
                {
                    episode.EpisodeIndex = movSrc.episode;
                    episode.Tag = movSrc.parentID.ToString();
                    seriesEpisodes.Add(episode);
                }
            }

            //Nachträglich die Parents auf IsSeries setzen
            var allMovies = database.GetAllMovies();
            foreach (var current in seriesEpisodes)
            {                
                var series = allMovies.FirstOrDefault(x => x.AltId.CompareTo(current.Tag) == 0);
                series.ProgrammeType = TVTProgrammeType.Series;
            }
        }

        public static int ConvertOldToNewValue(int oldValue)
        {
            return oldValue * 100 / 255;
        }

        public static void ConvertGenreAndFlags(TVTProgramme movie, MovieOldV2 movieOld)
        {
            switch (movieOld.genre)
			{
				case 0:  //action
					movie.MainGenre = TVTGenre.Action;
					break;
				case 1:  //thriller
					movie.MainGenre = TVTGenre.Thriller;
					break;
				case 2:  //sci-fi
					movie.MainGenre = TVTGenre.SciFi;
					break;
				case 3:  //comedy
					movie.MainGenre = TVTGenre.Comedy;
					break;
				case 4:  //horror
					movie.MainGenre = TVTGenre.Horror;
					break;
				case 5:  //love
					movie.MainGenre = TVTGenre.Romance;
					break;
				case 6:  //erotic
					movie.MainGenre = TVTGenre.Erotic;
					break;
				case 7:  //western
					movie.MainGenre = TVTGenre.Western;
					break;
				case 8:  //live
                    movie.ProgrammeType = TVTProgrammeType.Event;
					movie.Flags.Add( TVTMovieFlag.Live );
					break;
				case 9:  //kidsmovie
					movie.MainGenre = TVTGenre.Family;
					movie.TargetGroups.Add( TVTTargetGroup.Children );
					break;
				case 10:  //cartoon
					movie.MainGenre = TVTGenre.Animation;
					movie.Flags.Add( TVTMovieFlag.Animation );
					break;
				case 11:  //music
                    movie.ProgrammeType = TVTProgrammeType.Event;
                    movie.EventGenre = TVTEventGenre.Music;
					break;
				case 12:  //sport
                    movie.ProgrammeType = TVTProgrammeType.Event;
                    movie.EventGenre = TVTEventGenre.Sport;
					break;
				case 13:  //culture
					movie.MainGenre = TVTGenre.Documentary;
					movie.Flags.Add( TVTMovieFlag.Culture );
					break;
				case 14:  //fantasy
					movie.MainGenre = TVTGenre.Fantasy;
					break;
				case 15:  //yellow press
                    movie.ProgrammeType = TVTProgrammeType.Reportage;
                    movie.ReportageGenre = TVTReportageGenre.Undefined;
					break;
				case 16:  //news
                    movie.ProgrammeType = TVTProgrammeType.Reportage;					
					break;
				case 17:  //show					
                    movie.ProgrammeType = TVTProgrammeType.Show;
					movie.ShowGenre = TVTShowGenre.Undefined;                    
					break;
				case 18:  //monumental
					movie.MainGenre = TVTGenre.Monumental;
					movie.Flags.Add( TVTMovieFlag.Cult );
					break;
				case 19:  //fillers
					movie.MainGenre = TVTGenre.Undefined;
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

        public static List<TVTPerson> GetPersonsByNameOrCreate(ITVTDatabase database, string names, TVTDataContent defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown)
        {
            var result = new List<TVTPerson>();
            if (!string.IsNullOrEmpty(names))
            {
                var array = names.Split(',');
                foreach (var aValue in array)
                {
                    var personName = aValue.Trim();

                    var person = GetPersonByNameOrCreate(database, personName, defaultStatus, functionForNew);
                    result.Add(person);
                }
            }
            return result;
        }

        public static TVTPerson GetPersonByNameOrCreate(ITVTDatabase database, string name, TVTDataContent defaultType, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var tempName = name.Split(',')[0];
                var person = database.GetPersonByName(tempName);

                if (person == null)
                {
                    person = new TVTPerson();
                    person.GenerateGuid();
                    person.DataContent = defaultType;
                    if (defaultType == TVTDataContent.Fake || defaultType == TVTDataContent.FakeWithRefId)
                        person.ConvertFakeFullname(tempName);
                    else
                        person.ConvertFullname(tempName);
                    person.Functions.Add(functionForNew);
                    database.AddPerson(person);
                }
                return person;
            }
            else
                return null;
        }
    }
}
