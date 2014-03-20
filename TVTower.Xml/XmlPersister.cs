using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml
{
	public enum DataStructure
	{
		Full,
		FakeData,
		FakeDataOldFormat,
		OriginalData,
		OriginalDataOldFormat
	}

	public class XmlPersister
	{
		public const int CURRENT_VERSION = 3;

		public void SaveXML( ITVTowerDatabase<TVTMovieExtended> database, string filename, DataStructure dataStructure )
		{
			XmlDocument doc = new XmlDocument();

			var declaration = doc.CreateXmlDeclaration( "1.0", "utf-8", null );
			doc.AppendChild( declaration );

			var tvgdb = doc.CreateElement( "tvgdb" );
			doc.AppendChild( tvgdb );

			var version = doc.CreateElement( "version" );
			version.AddAttribute( "version", CURRENT_VERSION.ToString() );
			version.AddAttribute( "comment", "Export from TVTowerDBEditor" );
			version.AddAttribute( "exportDate", DateTime.Now.ToString() );
			version.AddAttribute( "dataStructure", dataStructure.ToString() );
			tvgdb.AppendChild( version );

			var allmovies = doc.CreateElement( "allmovies" );
			//allmovies.AddElement( "version", CURRENT_VERSION.ToString() );
			tvgdb.AppendChild( allmovies );

			foreach ( var movie in database.GetAllMovies() )
			{
				SetMovieDetailNode( doc, allmovies, movie, dataStructure );
			}

			var allpeople = doc.CreateElement( "allpeople" );
			//allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
			tvgdb.AppendChild( allpeople );

			foreach ( var person in database.GetAllPeople() )
			{
				SetPersonDetailNode( doc, allpeople, person, dataStructure );
			}

			//doc.Save( filename );

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "	";

			using ( XmlWriter writer = XmlWriter.Create( filename, settings ) )
			{
				doc.Save( writer );
			}
		}

		public XmlNode SetPersonDetailNode( XmlDocument doc, XmlElement element, TVTPerson person, DataStructure dataStructure )
		{
			XmlNode personNode;

			personNode = doc.CreateElement( "person" );
			element.AppendChild( personNode );

			switch ( dataStructure )
			{
				case DataStructure.Full:
					personNode.AddElement( "name", person.Name );
					break;
				case DataStructure.FakeData:
				case DataStructure.FakeDataOldFormat:
					personNode.AddElement( "name", person.Name );
					break;
				case DataStructure.OriginalData:
				case DataStructure.OriginalDataOldFormat:
					personNode.AddElement( "name", person.OriginalName );
					break;
			}

			personNode.AddElement( "tmdb_id", person.TmdbId.ToString() );
			personNode.AddElement( "imdb_id", person.ImdbId );
			personNode.AddElement( "image_url", person.ImageUrl );
			personNode.AddElement( "function", person.Function.ToString() );
			personNode.AddElement( "gender", person.Gender.ToString() );
			personNode.AddElement( "birthday", person.Birthday );
			personNode.AddElement( "deathday", person.Deathday );
			personNode.AddElement( "country", person.Country );

			if ( dataStructure == DataStructure.Full )
			{
				XmlNode additionalNode = doc.CreateElement( "additional" );
				additionalNode.AddElement( "original_name", person.OriginalName );
				additionalNode.AddElement( "place_of_birth", person.PlaceOfBirth );
				additionalNode.AddElement( "info", person.Info );
				additionalNode.AddElement( "movieRegistrations", person.MovieRegistrations.ToString() );
				additionalNode.AddElement( "otherInfo", person.OtherInfo );
				personNode.AppendChild( additionalNode );
			}

			if ( dataStructure == DataStructure.Full || dataStructure == DataStructure.FakeData || dataStructure == DataStructure.OriginalData )
			{
				XmlNode dataNode = doc.CreateElement( "data" );
				dataNode.AddAttribute( "professionSkill", person.ProfessionSkill.ToString() );
				dataNode.AddAttribute( "fame", person.Fame.ToString() );
				dataNode.AddAttribute( "success", person.Success.ToString() );
				dataNode.AddAttribute( "power", person.Power.ToString() );
				dataNode.AddAttribute( "humor", person.Humor.ToString() );
				dataNode.AddAttribute( "charisma", person.Charisma.ToString() );
				dataNode.AddAttribute( "eroticAura", person.EroticAura.ToString() );
				dataNode.AddAttribute( "characterSkill", person.CharacterSkill.ToString() );
				dataNode.AddAttribute( "scandalizing", person.Scandalizing.ToString() );
				dataNode.AddAttribute( "priceFactor", person.PriceFactor.ToString() );
				dataNode.AddAttribute( "topGenre1", person.TopGenre1.ToString() );
				dataNode.AddAttribute( "topGenre2", person.TopGenre2.ToString() );
				personNode.AppendChild( dataNode );
			}

			return personNode;
		}

		public XmlNode SetMovieDetailNode( XmlDocument doc, XmlElement element, TVTMovieExtended movie, DataStructure dataStructure )
		{
			XmlNode movieNode, dataNode;

			if ( movie.IsSeries )
				throw new NotImplementedException();

			movieNode = doc.CreateElement( "movie" );
			element.AppendChild( movieNode );

			switch ( dataStructure )
			{
				case DataStructure.Full:
					movieNode.AddElement( "title_de", movie.TitleDE );
					movieNode.AddElement( "title_en", movie.TitleEN );
					movieNode.AddElement( "original_title_de", movie.OriginalTitleDE );
					movieNode.AddElement( "original_title_en", movie.OriginalTitleEN );
					movieNode.AddElement( "description_de", movie.DescriptionDE );
					movieNode.AddElement( "description_en", movie.DescriptionEN );
					movieNode.AddElement( "original_description_de", movie.OriginalDescriptionDE );
					movieNode.AddElement( "original_description_en", movie.OriginalDescriptionEN );
					movieNode.AddElement( "description_tmdb", movie.DescriptionMovieDB );
					break;
				case DataStructure.FakeData:
					movieNode.AddElement( "title_de", movie.TitleDE );
					movieNode.AddElement( "title_en", movie.TitleEN );
					movieNode.AddElement( "description_de", movie.DescriptionDE );
					movieNode.AddElement( "description_en", movie.DescriptionEN );
					break;
				case DataStructure.FakeDataOldFormat:
					movieNode.AddElement( "title", movie.TitleDE );
					movieNode.AddElement( "description", movie.DescriptionDE );
					break;
				case DataStructure.OriginalData:
					movieNode.AddElement( "title_de", movie.OriginalTitleDE );
					movieNode.AddElement( "title_en", movie.OriginalTitleEN );
					movieNode.AddElement( "description_de", movie.OriginalDescriptionDE );
					movieNode.AddElement( "description_en", movie.OriginalDescriptionEN );
					break;
				case DataStructure.OriginalDataOldFormat:
					movieNode.AddElement( "title", movie.OriginalTitleDE );
					movieNode.AddElement( "description", movie.OriginalDescriptionDE );
					break;
			}

			//movieNode.AddElement( "version", movie.DataVersion.ToString() );

			dataNode = doc.CreateElement( "data" );

			if ( dataStructure != DataStructure.FakeDataOldFormat && dataStructure != DataStructure.OriginalDataOldFormat )
			{
				movieNode.AddElement( "image_url", movie.ImageUrl );

				XmlNode referencesNode = doc.CreateElement( "references" );
				referencesNode.AddAttribute( "tmdb_id", movie.TmdbId.ToString() );
				referencesNode.AddAttribute( "imdb_id", movie.ImdbId );
				referencesNode.AddAttribute( "rt_id", movie.RottenTomatoesId.HasValue ? movie.RottenTomatoesId.Value.ToString() : "" );
				movieNode.AppendChild( referencesNode );

				//neues Format
				dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
				dataNode.AddAttribute( "actor_ids", movie.Actors.Select( x => x.Id ).ToContentString( ";" ) );
				dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.Name : "" );
				dataNode.AddAttribute( "director_id", movie.Director != null ? movie.Director.Id.ToString() : "" );
			}
			else
			{
				//altes Format
				dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
				dataNode.AddAttribute( "director", movie.Director.Name );
			}

			dataNode.AddAttribute( "country", movie.Country );
			dataNode.AddAttribute( "year", movie.Year.ToString() );

			switch ( dataStructure )
			{
				case DataStructure.Full:
					dataNode.AddAttribute( "main_genre", movie.MainGenre.ToString() );
					dataNode.AddAttribute( "sub_genre", movie.SubGenre.ToString() );
					dataNode.AddAttribute( "show_genre", movie.ShowGenre.ToString() );
					dataNode.AddAttribute( "reportage_genre", movie.ReportageGenre.ToString() );
					break;
				case DataStructure.FakeData:
				case DataStructure.OriginalData:
					dataNode.AddAttribute( "main_genre", movie.MainGenre.ToString() );
					dataNode.AddAttribute( "sub_genre", movie.SubGenre.ToString() );
					dataNode.AddAttribute( "show_genre", movie.ShowGenre.ToString() );
					dataNode.AddAttribute( "reportage_genre", movie.ReportageGenre.ToString() );
					break;
				case DataStructure.FakeDataOldFormat:
				case DataStructure.OriginalDataOldFormat:
					dataNode.AddAttribute( "genre", movie.GenreOldVersion.ToString() );
					break;
			}

			dataNode.AddAttribute( "blocks", movie.Blocks.ToString() );
			dataNode.AddAttribute( "time", movie.LiveHour.ToString() );
			if ( dataStructure != DataStructure.FakeDataOldFormat && dataStructure != DataStructure.OriginalDataOldFormat )
				dataNode.AddAttribute( "flags", movie.Flags.ToContentString( " " ) );

			dataNode.AddAttribute( "price", movie.PriceRate.ToString() );
			dataNode.AddAttribute( "critics", movie.CriticsRate.ToString() );
			dataNode.AddAttribute( "speed", movie.ViewersRate.ToString() );
			dataNode.AddAttribute( "outcome", movie.BoxOfficeRate.ToString() );

			movieNode.AppendChild( dataNode );

			if ( dataStructure == DataStructure.Full )
			{

				XmlNode additionalNode = doc.CreateElement( "additional" );
				additionalNode.AddAttribute( "main_genre_raw", movie.MainGenreRaw.ToString() );
				additionalNode.AddAttribute( "sub_genre_raw", movie.SubGenreRaw.ToString() );
				additionalNode.AddAttribute( "genre_old_version", movie.GenreOldVersion.ToString() );
				additionalNode.AddAttribute( "price_rate_old", movie.PriceRateOld.ToString() );
				additionalNode.AddAttribute( "critic_rate_old", movie.CriticRateOld.ToString() );
				additionalNode.AddAttribute( "speed_rate_old", movie.SpeedRateOld.ToString() );
				additionalNode.AddAttribute( "boxoffice_rate_old", movie.BoxOfficeRateOld.ToString() );
				additionalNode.AddAttribute( "budget", movie.Budget.ToString() );
				additionalNode.AddAttribute( "revenue", movie.Revenue.ToString() );
				movieNode.AppendChild( additionalNode );
			}			

			return movieNode;
		}

		public ITVTowerDatabase<TVTMovieExtended> LoadXML( string filename, ITVTowerDatabase<TVTMovieExtended> database )
		{
			var result = database;
			int version = 0;

			var doc = new XmlDocument();
			doc.Load( filename );

			var allMovies = doc.GetElementsByTagName( "allmovies" );

			foreach ( XmlNode xmlMovie in allMovies )
			{
				if ( xmlMovie.HasAttribute( "version" ) )
					version = xmlMovie.GetAttributeInteger( "version" );

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
								}
							}

							break;
					}

					ConvertOldMovieData( movie, version );
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

		private void ConvertOldMovieData( TVTMovieExtended movie, int version )
		{
			if ( version == 0 )
			{
				GenreConverter( movie );
			}
			else
			{
				movie.MainGenre = (TVTGenre)movie.MainGenreRaw;
				movie.SubGenre = (TVTGenre)movie.SubGenreRaw;
			}
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
