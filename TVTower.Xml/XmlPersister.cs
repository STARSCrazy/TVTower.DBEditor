using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml
{
	public class XmlPersister
	{
		public const int CURRENT_VERSION = 1;

		public void SaveXML( ITVTowerDatabase<TVTMovieExtended> database, string filename )
		{
			XmlDocument doc = new XmlDocument();

			var tvgdb = doc.CreateElement( "tvgdb" );
			doc.AppendChild( tvgdb );

			var allmovies = doc.CreateElement( "allmovies" );
			tvgdb.AppendChild( allmovies );


			foreach ( var movie in database.GetAllMovies() )
			{
				SetMovieDetailNode( doc, allmovies, movie );
			}

			var allpeople = doc.CreateElement( "allpeople" );
			tvgdb.AppendChild( allpeople );

			foreach ( var person in database.GetAllPeople() )
			{
				SetPersonDetailNode( doc, allpeople, person );
			}

			doc.Save( filename );
		}

		public XmlNode SetPersonDetailNode( XmlDocument doc, XmlElement element, TVTPerson person )
		{
			XmlNode personNode;

			personNode = doc.CreateElement( "person" );
			element.AppendChild( personNode );

			personNode.AddElement( "name", person.Name );
			personNode.AddElement( "original_name", person.OriginalName );
			personNode.AddElement( "tmdb_id", person.TmdbId.ToString() );
			personNode.AddElement( "imdb_id", person.ImdbId );
			personNode.AddElement( "image_url", person.ImageUrl );
			personNode.AddElement( "function", person.Function.ToString() );
			personNode.AddElement( "gender", person.Gender.ToString() );
			personNode.AddElement( "birthday", person.Birthday );
			personNode.AddElement( "deathday", person.Deathday );
			personNode.AddElement( "place_of_birth", person.PlaceOfBirth );
			personNode.AddElement( "country", person.Country );
			personNode.AddElement( "info", person.Info );
			personNode.AddElement( "movieRegistrations", person.MovieRegistrations.ToString() );

			personNode.AddElement( "otherInfo", person.OtherInfo );

			return personNode;
		}

		public XmlNode SetMovieDetailNode( XmlDocument doc, XmlElement element, TVTMovieExtended movie )
		{
			XmlNode movieNode, dataNode;

			//movie.LoadDetails();
			//movie.calcRating();

			movieNode = doc.CreateElement( "movie" );
			element.AppendChild( movieNode );

			movieNode.AddElement( "title_de", movie.TitleDE );
			movieNode.AddElement( "title_en", movie.TitleEN );
			movieNode.AddElement( "original_title_de", movie.OriginalTitleDE );
			movieNode.AddElement( "original_title_en", movie.OriginalTitleEN );
			movieNode.AddElement( "description_de", movie.DescriptionDE );
			movieNode.AddElement( "description_en", movie.DescriptionEN );
			movieNode.AddElement( "description_tmdb", movie.DescriptionMovieDB );
			movieNode.AddElement( "tmdb_id", movie.TmdbId.ToString() );
			movieNode.AddElement( "imdb_id", movie.ImdbId );
			movieNode.AddElement( "image_url", movie.ImageUrl );
			movieNode.AddElement( "version", movie.DataVersion.ToString() );

			dataNode = doc.CreateElement( "data" );
			dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
			dataNode.AddAttribute( "actorIds", movie.Actors.Select( x => x.Id ).ToContentString( ";" ) );
			dataNode.AddAttribute( "director", movie.Director.Name );
			dataNode.AddAttribute( "directorId", movie.Director.Id.ToString() );
			dataNode.AddAttribute( "country", movie.Country );
			dataNode.AddAttribute( "year", movie.Year.ToString() );
			dataNode.AddAttribute( "mainGenre", movie.MainGenre.ToString() );
			dataNode.AddAttribute( "subGenre", movie.SubGenre.ToString() );
			dataNode.AddAttribute( "genreOldVersion", movie.GenreOldVersion.ToString() );
			dataNode.AddAttribute( "blocks", movie.Blocks.ToString() );
			dataNode.AddAttribute( "time", movie.LiveHour.ToString() );
			dataNode.AddAttribute( "flags", movie.Flags.ToContentString( " " ) );

			dataNode.AddAttribute( "price", movie.PriceRate.ToString() );
			dataNode.AddAttribute( "critics", movie.CriticsRate.ToString() );
			dataNode.AddAttribute( "speed", movie.ViewersRate.ToString() );
			dataNode.AddAttribute( "outcome", movie.BoxOfficeRate.ToString() );

			movieNode.AppendChild( dataNode );

			return movieNode;
		}

		public ITVTowerDatabase<TVTMovieExtended> LoadXML( string filename, ITVTowerDatabase<TVTMovieExtended> database )
		{
			var result = database;

			var doc = new XmlDocument();
			doc.Load( filename );

			var allMovies = doc.GetElementsByTagName( "allmovies" );

			foreach ( XmlNode xmlMovie in allMovies )
			{
				foreach ( XmlNode childNode in xmlMovie.ChildNodes )
				{
					var movie = new TVTMovieExtended();

					switch ( childNode.Name )
					{
						case "movie":
							foreach ( XmlLinkedNode movieChild in childNode.ChildNodes )
							{
								switch ( movieChild.Name )
								{
									case "title":
										movie.TitleDE = movieChild.GetElementValue();
										break;
									case "title_de":
										movie.TitleDE = movieChild.GetElementValue();
										break;
									case "title_en":
										movie.TitleEN = movieChild.GetElementValue();
										break;

									case "original_title":
										movie.OriginalTitleDE = movieChild.GetElementValue();
										break;
									case "original_title_de":
										movie.OriginalTitleDE = movieChild.GetElementValue();
										break;
									case "original_title_en":
										movie.OriginalTitleEN = movieChild.GetElementValue();
										break;

									case "description":
										movie.DescriptionDE = movieChild.GetElementValue();
										break;
									case "description_de":
										movie.DescriptionDE = movieChild.GetElementValue();
										break;
									case "description_en":
										movie.DescriptionEN = movieChild.GetElementValue();
										break;
									case "description_tmdb":
										movie.DescriptionMovieDB = movieChild.GetElementValue();
										break;


									case "tmdb_id":
										movie.TmdbId = int.Parse( movieChild.GetElementValue() );
										break;
									case "imdb_id":
										movie.ImdbId = movieChild.GetElementValue();
										break;
									case "image_url":
										movie.ImageUrl = movieChild.GetElementValue();
										break;
									case "data":
										//movie.Actors = movieChild.GetAttribute( "actors" );
										movie.Actors = ToPersonList( movieChild.GetAttribute( "actorIds" ), result );
										//movie.Director = movieChild.GetAttribute( "director" );
										movie.Director = result.GetPersonByStringId( movieChild.GetAttribute( "directorId" ) );
										movie.Country = movieChild.GetAttribute( "country" );
										movie.Year = movieChild.GetAttributeInteger( "year" );
										movie.MainGenreRaw = movieChild.GetAttributeInteger( "genre" );
										movie.SubGenreRaw = movieChild.GetAttributeInteger( "subgenre" );
										movie.Blocks = movieChild.GetAttributeInteger( "blocks" );
										movie.LiveHour = movieChild.GetAttributeInteger( "time" );
										movie.PriceRate = movieChild.GetAttributeInteger( "price" );
										movie.CriticsRate = movieChild.GetAttributeInteger( "critics" );
										movie.ViewersRate = movieChild.GetAttributeInteger( "speed" );
										movie.BoxOfficeRate = movieChild.GetAttributeInteger( "outcome" );
										movie.Flags = ToFlagList( movieChild.GetAttribute( "flags" ) );
										break;
									case "version":
										movie.DataVersion = int.Parse( movieChild.GetElementValue() );
										break;
								}
							}

							break;
					}

					ConvertOldMovieData( movie );
					result.AddMovie( movie );
				}
			}
			return result;
		}

		private List<TVTPerson> ToPersonList( string value, ITVTowerDatabase<TVTMovieExtended> database )
		{
			var result = new List<TVTPerson>();
			if ( !string.IsNullOrEmpty( value ) )
			{
				var array = value.Split( ';' );
				foreach ( var aValue in array )
				{
					var person = database.GetPersonById( Guid.Parse( aValue ) );
					if ( person != null )
						result.Add( person );
				}
			}
			return result;
		}

		private List<TVTMovieFlags> ToFlagList( string value )
		{
			var result = new List<TVTMovieFlags>();
			if ( !string.IsNullOrEmpty( value ) )
			{
				var array = value.Split( ' ' );
				foreach ( var aValue in array )
				{
					TVTMovieFlags outFlag;
					if ( Enum.TryParse<TVTMovieFlags>( aValue, out outFlag ) )
					{
						result.Add( outFlag );
					}
				}
			}
			return result;
		}

		private void ConvertOldMovieData( TVTMovieExtended movie )
		{
			if ( movie.DataVersion == 0 )
			{
				GenreConverter( movie );
			}
			else
			{
				movie.MainGenre = (TVTGenre)movie.MainGenreRaw;
				movie.SubGenre = (TVTGenre)movie.SubGenreRaw;
			}
			movie.DataVersion = CURRENT_VERSION;
		}

		private void GenreConverter( TVTMovieExtended movie )
		{
			switch ( movie.GenreOldVersion )
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
					movie.MainGenre = TVTGenre.Reportage;
					movie.Flags.Add( TVTMovieFlags.Live );
					break;
				case 9:  //kidsmovie
					movie.MainGenre = TVTGenre.Family;
					movie.Flags.Add( TVTMovieFlags.TG_Children );
					break;
				case 10:  //cartoon
					movie.MainGenre = TVTGenre.Animation;
					movie.Flags.Add( TVTMovieFlags.Animation );
					break;
				case 11:  //music
					movie.MainGenre = TVTGenre.Music;
					movie.Flags.Add( TVTMovieFlags.Music );
					break;
				case 12:  //sport
					movie.MainGenre = TVTGenre.Sport;
					movie.Flags.Add( TVTMovieFlags.Sport );
					break;
				case 13:  //culture
					movie.MainGenre = TVTGenre.Documentary;
					movie.Flags.Add( TVTMovieFlags.Culture );
					break;
				case 14:  //fantasy
					movie.MainGenre = TVTGenre.Fantasy;
					break;
				case 15:  //yellow press
					movie.MainGenre = TVTGenre.Reportage;
					movie.Flags.Add( TVTMovieFlags.YellowPress );
					break;
				case 16:  //news
					movie.MainGenre = TVTGenre.Reportage;
					break;
				case 17:  //show
					movie.MainGenre = TVTGenre.Show;
					movie.ShowGenre = TVTShowGenre.None;
					break;
				case 18:  //monumental
					movie.MainGenre = TVTGenre.Monumental;
					movie.Flags.Add( TVTMovieFlags.Cult );
					break;
				case 19:  //fillers
					movie.MainGenre = TVTGenre.Undefined;
					movie.Flags.Add( TVTMovieFlags.Trash );
					break;
				case 20:  //paid programing
					movie.MainGenre = TVTGenre.Commercial;
					movie.Flags.Add( TVTMovieFlags.Paid );
					break;
				default:
					throw new Exception();
			}
		}
	}
}
