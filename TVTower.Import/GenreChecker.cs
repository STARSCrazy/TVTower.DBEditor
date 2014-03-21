using System;
using System.Collections.Generic;
using TVTower.Entities;
using WatTmdb.V3;

namespace TVTower.Import
{
	public class GenreChecker
	{
		private static Dictionary<int, int> Counter = new Dictionary<int, int>();
		private static Dictionary<int, string> Names = new Dictionary<int, string>();

		int[] matchedGenres = new int[20];

		public GenreChecker()
		{
			for ( var i = 0; i < matchedGenres.Length; i++ )
			{
				matchedGenres[i] = 0;
			}
		}

		public const int GENRE_ADVENTURE = 0;
		public const int GENRE_ACTION = 1;
		public const int GENRE_DRAMA = 2;
		public const int GENRE_EROTIC = 3;
		public const int GENRE_FANTASY = 4;
		public const int GENRE_HORROR = 5;
		public const int GENRE_COMEDY = 6;
		public const int GENRE_WAR = 7;
		public const int GENRE_CRIME = 8;
		public const int GENRE_LOVE = 9;
		public const int GENRE_MYSTERY = 10;
		public const int GENRE_SCIFI = 11;
		public const int GENRE_THRILLER = 12;
		public const int GENRE_WESTERN = 13;
		public const int GENRE_DOCUMENTARY = 14;
		public const int GENRE_REPORTAGE = 15;
		public const int GENRE_MISC_EVENTS = 16;
		public const int GENRE_SHOW = 17;
		public const int IGNORE_GENRE = -1000;

		private List<TVTMovieFlag> GetFlags( int movieDbGenreId )
		{
			var result = new List<TVTMovieFlag>();

			switch ( movieDbGenreId )
			{
				case 16: //Animation = 86					
					result.Add( TVTMovieFlag.Animation );
					break;
				case 9805: //Sport = 2				
				case 10757: //Sport Film = 9
					result.Add( TVTMovieFlag.Sport );
					break;
				case 10755: //Short = 6								
					result.Add( TVTMovieFlag.Culture );
					break;
				case 10753: //Film noir = Eigentlich Drama oder Krimi = 9
				case 10754: //Neo-noir = 2
					result.Add( TVTMovieFlag.Culture );
					break;
				case 10402: //Musik = 24
				case 22: //Musical = 11
					result.Add( TVTMovieFlag.Music );
					break;
			}

			return result;
		}

		private List<TVTTargetGroup> GetTargetGroups( int movieDbGenreId )
		{
			var result = new List<TVTTargetGroup>();

			switch ( movieDbGenreId )
			{
				case 10751: //Familie = 117
					result.Add( TVTTargetGroup.Children );
					result.Add( TVTTargetGroup.Employees );
					result.Add( TVTTargetGroup.HouseWifes );
					result.Add( TVTTargetGroup.Pensioners );
					break;
				case 10762: //Kids = 2
					result.Add( TVTTargetGroup.Children );
					break;
				case 10595: //Holiday = 14
					result.Add( TVTTargetGroup.HouseWifes );
					result.Add( TVTTargetGroup.Pensioners );
					break;
				case 36: //Historie = 40
					result.Add( TVTTargetGroup.Pensioners );
					break;
			}

			return result;
		}

		private int GetTVTGenreId( int movieDbGenreId )
		{
			switch ( movieDbGenreId )
			{
				//Abenteuer = 0
				case 12:  //Abenteuer = 252
				case 1115: //Road-Movie = 4
					return GENRE_ADVENTURE;
				//Action = 1
				case 28: return GENRE_ACTION; //Action = 347
				//Drama = 2
				case 18: //Drama = 347
				case 105: //Katastrophenfilm = 4
					return GENRE_DRAMA;
				//Erotik = 3
				case 2916: return GENRE_EROTIC; //Erotik = 4
				//Fantasy = 4
				case 14: return GENRE_FANTASY; //Fantasy = 118
				//Horror = 5
				case 27: return GENRE_HORROR; //Horror = 73
				//Komödie = 6
				case 35: return GENRE_COMEDY; //Komödie = 207				
				//Krieg = 7
				case 10752: return GENRE_WAR; //Kriegsfilm = 43
				//Krimi = 8
				case 80: //Krimi = 133
				case 10753: //Film noir = Eigentlich Drama oder Krimi = 9
				case 10754: //Neo-noir = 2
					return GENRE_CRIME;
				//Liebe = 9
				case 10749: return GENRE_LOVE; //Lovestory = 99			
				//Mystery = 10
				case 9648: return GENRE_MYSTERY; //Mystery = 79
				//Sci-Fi = 11
				case 878: return GENRE_SCIFI; //Sci-Fi = 170
				//Thriller = 12
				case 53: //Thriller = 266
				case 10748: //Suspense = Spannung/Schwebe = 22
					return GENRE_THRILLER;
				//Western = 13
				case 37: return GENRE_WESTERN; //Western = 33
				//Dokumentation = 14
				case 99: return GENRE_DOCUMENTARY; //Dokumentarfilm = 3

				//Sonstige
				case 16: //Animation = 86					
				case 10751: //Familie = 117
				case 10762: //Kids = 2
				case 10595: //Holiday = 14
				case 36: //Historie = 40
				case 10769: //Foreign = 77		
				case 9805: //Sport = 2				
				case 10755: //Short = 6								
				case 10757: //Sport Film = 9
				case 82: //Eastern = 7
				case 10770: //TV-Film = 3				
				case 10756: //Indie = 9										
				case 10402: //Musik = 24
				case 22: //Musical = 11			
					return IGNORE_GENRE;

				default:
					//throw new Exception();
					return IGNORE_GENRE; //Später wieder Exception
			}

			throw new Exception();
		}

		private int GetMoreImportendGenre( int genre1, int genre2 )
		{
			if ( genre1 == 10 || genre2 == 10 )
				return 10;


			if ( genre1 >= 0 )
				return genre1;
			else
				return genre2;
		}

		public TVTGenre GetGenre( List<MovieGenre> genres )
		{
			return TVTGenre.Undefined;
		}

		public int GetGenreId( List<MovieGenre> genres )
		{
			int result = -1000;
			int maximum = 0;
			int maxIndex = -1;

			foreach ( var genre in genres )
			{
				var currentValue = GetTVTGenreId( genre.id );
				int index = currentValue;
				if ( index == -1000 )
					continue;

				if ( currentValue < 0 && currentValue > -1000 )
					index = currentValue * -1;

				matchedGenres[index] = matchedGenres[index] + 1;
				if ( matchedGenres[index] > maximum )
				{
					maximum = matchedGenres[index];
					maxIndex = index;
				}
				else if ( matchedGenres[index] == maximum )
				{
					maxIndex = -1;
				}

				result = GetMoreImportendGenre( result, currentValue );
			}

			if ( maxIndex >= 0 )
				return matchedGenres[maxIndex];
			else
				return result;



		}

	}
}
