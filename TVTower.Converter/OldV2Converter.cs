﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TVTower.Entities;

namespace TVTower.Converter
{
	public static class OldV2Converter
	{
		public static void RefreshMovieDescriptions( ITVTDatabase database )
		{
			var programme = new List<ITVTProgrammeCore>();
			programme.AddRange( database.GetAllProgrammes( true ) );
			programme.AddRange( database.GetAllEpisodes() );

			foreach ( var person in database.GetAllPeople() )
			{                
				var foundProgrammes = programme.Where( x => x.Staff.FirstOrDefault( y => y.Person == person ) != null );
				foreach ( var foundProgramme in foundProgrammes )
				{
					var index = foundProgramme.Staff.FirstOrDefault( y => y.Person == person ).Index;

                    if ( !string.IsNullOrEmpty( foundProgramme.DescriptionDE ) )
                    {
                        if ( person.FullName != " " && foundProgramme.DescriptionDE.Contains( person.FullName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FullName, string.Format( "[{0}|Full]", index ) );

                        if ( !string.IsNullOrEmpty( person.FirstName ) && foundProgramme.DescriptionDE.Contains( person.FirstName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FirstName, string.Format( "[{0}|First]", index ) );

                        if ( !string.IsNullOrEmpty( person.LastName ) && foundProgramme.DescriptionDE.Contains( person.LastName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.LastName, string.Format( "[{0}|Last]", index ) );

                        if ( !string.IsNullOrEmpty( person.NickName ) && foundProgramme.DescriptionDE.Contains( person.NickName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.NickName, string.Format( "[{0}|Nick]", index ) );
                    }


                    if ( !string.IsNullOrEmpty( foundProgramme.DescriptionEN ) )
                    {
                        if ( person.FullName != " " && foundProgramme.DescriptionEN.Contains( person.FullName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FullName, string.Format( "[{0}|Full]", index ) );

                        if ( !string.IsNullOrEmpty( person.FirstName ) && foundProgramme.DescriptionEN.Contains( person.FirstName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FirstName, string.Format( "[{0}|First]", index ) );

                        if ( !string.IsNullOrEmpty( person.LastName ) && foundProgramme.DescriptionEN.Contains( person.LastName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.LastName, string.Format( "[{0}|Last]", index ) );

                        if ( !string.IsNullOrEmpty( person.NickName ) && foundProgramme.DescriptionEN.Contains( person.NickName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.NickName, string.Format( "[{0}|Nick]", index ) );
                    }



                    if ( !string.IsNullOrEmpty( foundProgramme.DescriptionDE ) )
                    {
                        if ( person.FakeFullName != " " && foundProgramme.DescriptionDE.Contains( person.FakeFullName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FakeFullName, string.Format( "[{0}|Full]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeFirstName ) && foundProgramme.DescriptionDE.Contains( person.FakeFirstName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FakeFirstName, string.Format( "[{0}|First]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeLastName ) && foundProgramme.DescriptionDE.Contains( person.FakeLastName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FakeLastName, string.Format( "[{0}|Last]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeNickName ) && foundProgramme.DescriptionDE.Contains( person.FakeNickName ) )
                            foundProgramme.DescriptionDE = foundProgramme.DescriptionDE.Replace( person.FakeNickName, string.Format( "[{0}|Nick]", index ) );
                    }

                    if ( !string.IsNullOrEmpty( foundProgramme.DescriptionEN ) )
                    {
                        if ( person.FakeFullName != " " && foundProgramme.DescriptionEN.Contains( person.FakeFullName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FakeFullName, string.Format( "[{0}|Full]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeFirstName ) && foundProgramme.DescriptionEN.Contains( person.FakeFirstName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FakeFirstName, string.Format( "[{0}|First]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeLastName ) && foundProgramme.DescriptionEN.Contains( person.FakeLastName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FakeLastName, string.Format( "[{0}|Last]", index ) );

                        if ( !string.IsNullOrEmpty( person.FakeNickName ) && foundProgramme.DescriptionEN.Contains( person.FakeNickName ) )
                            foundProgramme.DescriptionEN = foundProgramme.DescriptionEN.Replace( person.FakeNickName, string.Format( "[{0}|Nick]", index ) );
                    }
				}
			}
		}

		public static void FakePersonNames( ITVTDatabase database )
		{
			//var syl = new Dictionary<string, int>();

			//foreach (var person in database.GetAllPeople())
			//{
			//    var name = person.LastName;
			//    for (var i = 0; i <= name.Length - 3; i++)
			//    {
			//        var currSylKey = name.Substring(i, 3);
			//        if (!currSylKey.Contains(' '))
			//        {
			//            currSylKey = currSylKey.ToLower();

			//            if (syl.ContainsKey(currSylKey))
			//                syl[currSylKey] = syl[currSylKey] + 1;
			//            else
			//                syl.Add(currSylKey, 1);
			//        }
			//    }
			//}

			//var myList = syl.ToList();

			//myList.Sort((firstPair, nextPair) =>
			//{
			//    return firstPair.Value.CompareTo(nextPair.Value)*-1;
			//}
			//);

			//var i2 = 0;
			//foreach (var entry in myList)
			//{
			//    Console.WriteLine(entry.Key + " = " + entry.Value);
			//    i2++;
			//    if (i2 >= 100)
			//        break;
			//}

			int count = 0;
			var faker = new NameFaker();
			faker.InitializeData();

			foreach ( var person in database.GetAllPeople() )
			{
				if ( person.DataType != TVTDataType.Fictitious )
				{
					if ( string.IsNullOrEmpty( person.FakeLastName ) || person.LastName == person.FakeLastName )
					{
						var tempFakeName = faker.Fake( person.LastName );
						if ( tempFakeName != person.LastName )
							person.FakeLastName = tempFakeName;
						else
						{
							person.FakeLastName = tempFakeName.Substring( 0, 1 ) + ".";
							Console.WriteLine( person.LastName );
							count++;
						}
					}
				}
			}
			Console.WriteLine( "Result: " + count );
		}

		private static void ConvertCommonMinimal( CommonOldV2 common, ITVTNamesBasic names, ITVTDatabase database )
		{
			names.GenerateGuid();
			names.OldId = common.id.ToString();
			names.TitleDE = HttpUtility.HtmlDecode( common.title ); ;
			names.TitleEN = common.titleEnglish;
			names.DescriptionDE = common.description;
			names.DescriptionEN = common.descriptionEnglish;
		}

		private static void ConvertCommon( CommonOldV2 common, ITVTNames names, ITVTDatabase database, bool flipFake = false )
		{
			ConvertCommonMinimal( common, names, database );
			if ( flipFake )
			{
				names.FakeTitleDE = common.title != null ? common.title.Trim() : null;
				names.FakeTitleEN = common.titleEnglish != null ? common.titleEnglish.Trim() : null;
				names.TitleDE = common.titleFake != null ? common.titleFake.Trim() : null;
				names.TitleEN = common.titleEnglishFake != null ? common.titleEnglishFake.Trim() : null;
			}
			else
			{
				names.FakeTitleDE = common.titleFake != null ? common.titleFake.Trim() : null;
				names.FakeTitleEN = common.titleEnglishFake != null ? common.titleEnglishFake.Trim() : null;
			}
		}

		private static void ConvertEpisode( MovieOldV2 movieOldV2, ITVTProgrammeCore episode, ITVTDatabase database )
		{
			if ( movieOldV2.custom )
			{
				episode.DataType = TVTDataType.Fictitious;
			}
			else
			{
				episode.DataType = TVTDataType.Undefined;
			}

			ConvertCommon( movieOldV2, episode, database );
			episode.CreatorId = movieOldV2.creatorID;
			episode.EditorId = movieOldV2.editorID;
			episode.LastModified = new DateTime( 2004, 1, 1 );

			var director = GetPersonByNameOrCreate( database, movieOldV2.director, episode.DataType, movieOldV2.useFakeNames, TVTPersonFunction.Director );
			if ( director != null )
				episode.Staff.Add( new TVTStaff( director, TVTPersonFunction.Director ) );

			var actors = GetPersonsByNameOrCreate( database, movieOldV2.actors, episode.DataType, movieOldV2.useFakeNames, TVTPersonFunction.Actor );
			actors.ForEach( x => episode.Staff.Add( new TVTStaff( x, TVTPersonFunction.Actor ) ) );
		}

		public static void Convert( List<MovieOldV2> moviesOldV2, ITVTDatabase database, TVTDataRoot dataRoot )
		{
			foreach ( var movSrc in moviesOldV2 )
			{
				if ( movSrc.parentID == 0 ) //Film
				{
					var programme = new TVTProgramme();
					programme.DataRoot = dataRoot;
					programme.ProductType = TVTProductType.Programme;
					programme.ProgrammeType = TVTProgrammeType.Movie;
					programme.Approved = movSrc.approved;

					ConvertEpisode( movSrc, programme, database );

					programme.CriticsRate = ConvertOldToNewValue( movSrc.critics );
					programme.ViewersRate = ConvertOldToNewValue( movSrc.speed );
					programme.PriceMod = ((float)movSrc.price) / 150;
					if ( programme.PriceMod == 0.0 )
						programme.PriceMod = 1;
					else if ( programme.PriceMod > 10 )
						programme.PriceMod = 10;

					programme.BoxOfficeRate = ConvertOldToNewValue( movSrc.outcome );

					programme.Year = movSrc.year;
					programme.Country = movSrc.country;
					programme.Blocks = movSrc.blocks;

					ConvertGenreAndFlags( programme, movSrc );

					if ( programme.Flags.Contains( TVTProgrammeFlag.Live ) && movSrc.time > 0 )
						programme.LiveHour = movSrc.time;

					if ( movSrc.xrated )
						programme.Flags.Add( TVTProgrammeFlag.XRated );

					database.AddProgramme( programme );
				}
				else //Serien-Episode
				{
					var episode = new TVTProgramme();
					episode.DataRoot = dataRoot;
					episode.ProductType = TVTProductType.Episode;
					episode.ProgrammeType = TVTProgrammeType.Movie;
					episode.Approved = movSrc.approved;
					ConvertEpisode( movSrc, episode, database );

					episode.Tag = movSrc.parentID.ToString();

					episode.CriticsRate = ConvertOldToNewValue( movSrc.critics );
					if ( episode.CriticsRate == 0 )
						episode.CriticsRate = -1;
					episode.ViewersRate = ConvertOldToNewValue( movSrc.speed );
					if ( episode.ViewersRate == 0 )
						episode.ViewersRate = -1;
					episode.EpisodeIndex = movSrc.episode;

					episode.DistributionChannel = TVTDistributionChannel.Inherit;
					episode.MainGenre = TVTProgrammeGenre.Inherit;
					episode.SubGenre = TVTProgrammeGenre.Inherit;
					episode.Flags.Add( TVTProgrammeFlag.Inhert );

					episode.LiveHour = -1;

					episode.TargetGroups.Add( TVTTargetGroup.Inhert );
					episode.ProPressureGroups.Add( TVTPressureGroup.Inhert );
					episode.ContraPressureGroups.Add( TVTPressureGroup.Inhert );

					database.AddProgramme( episode );
				}
			}

			//Nachträglich die Parents auf IsSeries setzen
			var allMovies = database.GetAllProgrammes( true );
			foreach ( var current in database.GetAllEpisodes() )
			{
				var series = allMovies.FirstOrDefault( x => x.OldId.CompareTo( current.Tag ) == 0 );
				if ( series != null )
				{
					series.ProductType = TVTProductType.Series;
					current.SeriesMaster = new CodeKnight.Core.WeakReference<TVTProgramme>( series );
				}
				else
					current.DataStatus = TVTDataStatus.Incorrect;
			}
		}

		public static void Convert( List<AdvertisingOldV2> adsOldV2, ITVTDatabase database, TVTDataRoot dataRoot )
		{
			foreach ( var adSrc in adsOldV2 )
			{
				var ad = new TVTAdvertising();
				ad.DataRoot = dataRoot;

				ConvertCommonMinimal( adSrc, ad, database );
				ad.DescriptionDE = adSrc.description;
				ad.DescriptionEN = adSrc.descriptionEnglish;

				ad.CreatorId = adSrc.creatorID;
				ad.EditorId = adSrc.editorID;
				ad.LastModified = new DateTime( 2004, 1, 1 );

				ad.FixPrice = (adSrc.fixedProfit > 0);
				ad.MinAudience = ConvertOldToNewValueFloat( adSrc.minAudience );
				ad.MinImage = ConvertOldToNewValue( adSrc.minImage );
				ad.Repetitions = adSrc.repetitions;
				ad.Duration = adSrc.duration;
				ad.Profit = ConvertProfitPenalty( adSrc.profit, adSrc.fixedProfit > 0, adSrc.fixedProfit );
				ad.Penalty = ConvertProfitPenalty( adSrc.penalty, adSrc.fixedPenalty > 0, adSrc.fixedPenalty );

				ad.TargetGroup = ConvertTargetGroup( adSrc.targetgroup );

				ad.Approved = adSrc.approved;

				database.AddAdvertising( ad );
			}
		}

		public static void Convert( List<NewsOldV2> newsOldV2, ITVTDatabase database, TVTDataRoot dataRoot )
		{
			foreach ( var newsSrc in newsOldV2 )
			{
				var news = new TVTNews();
				news.DataRoot = dataRoot;

				ConvertCommonMinimal( newsSrc, news, database );

				if ( newsSrc.parentID == 0 )
				{
					news.NewsType = TVTNewsType.InitialNews;
					news.NewsThreadId = newsSrc.id.ToString();
					news.Tag = -1;
				}
				else
				{
					news.NewsType = TVTNewsType.FollowingNews;
					news.NewsThreadId = newsSrc.parentID.ToString();
					news.Tag = newsSrc.episode;
				}

				//PossibleFollower - erst später ermitteln;
				news.Genre = (TVTNewsGenre)newsSrc.genre;

				news.AvailableAfterDays = -1;
				news.YearRangeFrom = -1;
				news.YearRangeTo = -1;
				news.MinHoursAfterPredecessor = -1;
				news.TimeRangeFrom = -1;
				news.TimeRangeTo = -1;

				news.Price = ConvertOldToNewValue( newsSrc.price );
				news.Quality = ConvertOldToNewValue( newsSrc.topicality );

				news.Resource1Type = null;
				news.Resource2Type = null;
				news.Resource3Type = null;
				news.Resource4Type = null;

				database.AddNews( news );
			}

			var allNews = database.GetAllNews();

			List<string> threadsDone = new List<string>();

			//Episoden-ID fixen
			foreach ( var news in allNews )
			{
				if ( !threadsDone.Contains( news.NewsThreadId ) )
				{
					var newsThread = allNews.Where( x => x.NewsThreadId == news.NewsThreadId );

					var start = newsThread.FirstOrDefault( x => (int)x.Tag == -1 );
					start.Tag = 0;

					var rest = newsThread.ToList();
					rest.Remove( start );

					var episode = 1;
					foreach ( var restNews in rest.OrderBy( x => x.Tag ).ToList() )
					{
						restNews.Tag = episode;
						episode++;
					}

					threadsDone.Add( news.NewsThreadId );
				}
			}

			foreach ( var news in allNews )
			{
				var episode = (int)news.Tag;

				TVTNews follower = allNews.FirstOrDefault( x => x.NewsThreadId == news.NewsThreadId && (int)x.Tag == episode + 1 );
				if ( follower != null )
				{
					follower.NewsType = TVTNewsType.FollowingNews;
					news.Effects.Add( new TVTNewsEffect( TVTNewsEffectType.TriggerNews, follower.Id.ToString() ) );
				}
				else
				{
					if ( news.NewsThreadId == news.OldId )
						news.NewsType = TVTNewsType.InitialNews;
					else
						news.NewsType = TVTNewsType.FollowingNews;
				}
			}

			foreach ( var news in allNews )
			{
				//if ( news.NewsType == TVTNewsType.SingleNews )
				//    news.NewsThreadId = null;
				//else
				//{
				TVTNews thread_owner = allNews.FirstOrDefault( x => x.OldId == news.NewsThreadId );
				news.NewsThreadId = thread_owner.Id.ToString();
				//}
			}
		}

		public static int ConvertOldToNewValue( int oldValue )
		{
			return oldValue * 100 / 255;
		}

		public static float ConvertOldToNewValueFloat( int oldValue )
		{
			float value = ((float)oldValue) * 100 / 255;
			return (float)Math.Round( (Decimal)value, 1, MidpointRounding.AwayFromZero );
		}

		public static int ConvertProfitPenalty( int value, bool isFixValue, int fixValue )
		{
			if ( isFixValue )
				return fixValue;// / 1000;
			else
				return value * 30;
		}

		public static TVTTargetGroup ConvertTargetGroup( int oldTargetGroup )
		{
            if ( oldTargetGroup >= 0 && oldTargetGroup <= 9 )
            {
                switch ( oldTargetGroup )
                {
                    case 0:
                    default:
                        return TVTTargetGroup.All;
                    case 1:
                        return TVTTargetGroup.Children;
                    case 2:
                        return TVTTargetGroup.Teenagers;
                    case 3:
                        return TVTTargetGroup.HouseWifes;
                    case 4:
                        return TVTTargetGroup.Employees;
                    case 5:
                        return TVTTargetGroup.Unemployed;
                    case 6:
                        return TVTTargetGroup.Manager;
                    case 7:
                        return TVTTargetGroup.Pensioners;
                    case 8:
                        return TVTTargetGroup.Women;
                    case 9:
                        return TVTTargetGroup.Men;
                }                
            }
            else
                return TVTTargetGroup.All;
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
					movie.Flags.Add( TVTProgrammeFlag.Live );
					break;
				case 9:  //kidsmovie
					movie.MainGenre = TVTProgrammeGenre.Family;
					movie.TargetGroups.Add( TVTTargetGroup.Children );
					break;
				case 10:  //cartoon
					movie.MainGenre = TVTProgrammeGenre.Family;
					movie.Flags.Add( TVTProgrammeFlag.Animation );
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
					movie.Flags.Add( TVTProgrammeFlag.Culture );
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
					movie.Flags.Add( TVTProgrammeFlag.Cult );
					break;
				case 19:  //fillers
					movie.ProgrammeType = TVTProgrammeType.Misc;
					movie.MainGenre = TVTProgrammeGenre.Undefined;
					movie.Flags.Add( TVTProgrammeFlag.Trash );
					break;
				case 20:  //paid programing
					movie.ProgrammeType = TVTProgrammeType.Commercial;
					movie.Flags.Add( TVTProgrammeFlag.Paid );
					break;
				default:
					throw new Exception();
			}
		}

		public static List<TVTPerson> GetPersonsByNameOrCreate( ITVTDatabase database, string names, TVTDataType defaultType, bool isFakeName, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			var result = new List<TVTPerson>();
			if ( !string.IsNullOrEmpty( names ) )
			{
				var array = names.Split( ',' );
				foreach ( var aValue in array )
				{
					var personName = aValue.Trim();

					var person = GetPersonByNameOrCreate( database, personName, defaultType, isFakeName, functionForNew );
					if ( person != null )
						result.Add( person );
				}
			}
			return result;
		}

		public static TVTPerson GetPersonByNameOrCreate( ITVTDatabase database, string name, TVTDataType defaultType, bool isFakeName, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			if ( !string.IsNullOrEmpty( name ) )
			{
				var tempName = name.Split( ',' )[0];
				var person = database.GetPersonByName( tempName );

				if ( person == null )
				{
					person = new TVTPerson();
					person.GenerateGuid();
					person.DataType = defaultType;
					if ( isFakeName )
						PersonConverter.ConvertFakeFullname( person, tempName );
					else
						PersonConverter.ConvertFullname( person, tempName );

					person.Functions.Add( functionForNew );
					person.PriceMod = 1;
					database.AddPerson( person );
				}
				return person;
			}
			else
				return null;
		}
	}
}
