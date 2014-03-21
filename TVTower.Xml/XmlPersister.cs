using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml
{
	public enum DatabaseVersion
	{
		V2 = 2,
		V3 = 3
	}

	public enum DataStructure
	{
		Full,
		FakeData,
		OriginalData
	}

	public class XmlPersister
	{
		public const int CURRENT_VERSION = 3;

		public void SaveXML( ITVTowerDatabase<TVTMovieExtended> database, string filename, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			XmlDocument doc = new XmlDocument();

			var declaration = doc.CreateXmlDeclaration( "1.0", "utf-8", null );
			doc.AppendChild( declaration );

			var tvgdb = doc.CreateElement( "tvgdb" );
			doc.AppendChild( tvgdb );

			var version = doc.CreateElement( "version" );
			version.AddAttribute( "value", ((int)dbVersion).ToString() );
			version.AddAttribute( "comment", "Export from TVTowerDBEditor" );
			version.AddAttribute( "exportDate", DateTime.Now.ToString() );
			tvgdb.AppendChild( version );

			var allmovies = doc.CreateElement( "allmovies" );
			//allmovies.AddElement( "version", CURRENT_VERSION.ToString() );
			tvgdb.AppendChild( allmovies );

			foreach ( var movie in database.GetAllMovies() )
			{
				SetMovieDetailNode( doc, allmovies, movie, dbVersion, dataStructure );
			}

			if ( ((int)dbVersion) >= 3 )
			{
				var allpeople = doc.CreateElement( "allpeople" );
				//allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
				tvgdb.AppendChild( allpeople );

				foreach ( var person in database.GetAllPeople() )
				{
					SetPersonDetailNode( doc, allpeople, person, dbVersion, dataStructure );
				}
			}

			var exportOptions = doc.CreateElement( "exportOptions" );
			exportOptions.AddAttribute( "onlyFakes", (dataStructure == DataStructure.FakeData).ToString().ToLower() );
			exportOptions.AddAttribute( "onlyCustom", "false" );
			exportOptions.AddAttribute( "dataStructure", dataStructure.ToString() );
			tvgdb.AppendChild( exportOptions );

			stopWatch.Stop();

			var time = doc.CreateElement( "time" );
			time.AddAttribute( "value", stopWatch.ElapsedMilliseconds.ToString() + "ms" );
			tvgdb.AppendChild( time );

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "	";

			using ( XmlWriter writer = XmlWriter.Create( filename, settings ) )
			{
				doc.Save( writer );
			}
		}

		public XmlNode SetPersonDetailNode( XmlDocument doc, XmlElement element, TVTPerson person, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			XmlNode personNode;

			personNode = doc.CreateElement( "person" );
			{
				personNode.AddAttribute( "id", person.Id.ToString() );
				if ( dataStructure == DataStructure.Full )
					personNode.AddAttribute( "status", person.DataStatus.ToString() );
			}
			element.AppendChild( personNode );

			switch ( dataStructure )
			{
				case DataStructure.Full:
					personNode.AddElement( "name", person.Name );
					break;
				case DataStructure.FakeData:
					personNode.AddElement( "name", person.Name );
					break;
				case DataStructure.OriginalData:
					personNode.AddElement( "name", person.OriginalName );
					break;
			}

			personNode.AddElement( "function", person.Functions.ToContentString( ";" ) );
			personNode.AddElement( "gender", person.Gender.ToString() );
			personNode.AddElement( "birthday", person.Birthday );
			personNode.AddElement( "deathday", person.Deathday );
			personNode.AddElement( "country", person.Country );

			personNode.AddElement( "image_url", person.ImageUrl );

			XmlNode referencesNode = doc.CreateElement( "references" );
			referencesNode.AddAttribute( "tmdb_id", person.TmdbId.ToString() );
			referencesNode.AddAttribute( "imdb_id", person.ImdbId );
			personNode.AppendChild( referencesNode );

			//personNode.AddElement( "tmdb_id", person.TmdbId.ToString() );
			//personNode.AddElement( "imdb_id", person.ImdbId );
			//personNode.AddElement( "image_url", person.ImageUrl );

			if ( dataStructure == DataStructure.Full )
			{
				XmlNode additionalNode = doc.CreateElement( "additional" );
				additionalNode.AddAttribute( "original_name", person.OriginalName );
				additionalNode.AddAttribute( "place_of_birth", person.PlaceOfBirth );
				additionalNode.AddAttribute( "info", person.Info );
				additionalNode.AddAttribute( "movieRegistrations", person.MovieRegistrations.ToString() );
				additionalNode.AddAttribute( "otherInfo", person.OtherInfo );
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

		public XmlNode SetMovieDetailNode( XmlDocument doc, XmlElement element, TVTMovieExtended movie, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			XmlNode movieNode, dataNode;

			if ( movie.IsSeries )
				throw new NotImplementedException();

			movieNode = doc.CreateElement( "movie" );
			if ( dbVersion != DatabaseVersion.V2 )
			{
				movieNode.AddAttribute( "id", movie.Id.ToString() );
				movieNode.AddAttribute( "status", movie.DataStatus.ToString() );
			}
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
					if ( dbVersion == DatabaseVersion.V2 )
					{
						movieNode.AddElement( "title", movie.TitleDE );
						movieNode.AddElement( "description", movie.DescriptionDE );
					}
					else
					{
						movieNode.AddElement( "title_de", movie.TitleDE );
						movieNode.AddElement( "title_en", movie.TitleEN );
						movieNode.AddElement( "description_de", movie.DescriptionDE );
						movieNode.AddElement( "description_en", movie.DescriptionEN );
					}
					break;
				case DataStructure.OriginalData:
					if ( dbVersion == DatabaseVersion.V2 )
					{
						movieNode.AddElement( "title", movie.OriginalTitleDE );
						movieNode.AddElement( "description", movie.OriginalDescriptionDE );
					}
					else
					{
						movieNode.AddElement( "title_de", movie.OriginalTitleDE );
						movieNode.AddElement( "title_en", movie.OriginalTitleEN );
						movieNode.AddElement( "description_de", movie.OriginalDescriptionDE );
						movieNode.AddElement( "description_en", movie.OriginalDescriptionEN );
					}
					break;
			}

			//movieNode.AddElement( "version", movie.DataVersion.ToString() );

			dataNode = doc.CreateElement( "data" );

			if ( dbVersion == DatabaseVersion.V2 )
			{
				//altes Format
				dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
				dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.Name : "" );
			}
			else
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

			dataNode.AddAttribute( "country", movie.Country );
			dataNode.AddAttribute( "year", movie.Year.ToString() );

			if ( dbVersion == DatabaseVersion.V2 )
			{
				dataNode.AddAttribute( "genre", movie.GenreOldVersion.ToString() );
			}
			else
			{
				dataNode.AddAttribute( "main_genre", ((int)movie.MainGenre).ToString() );
				dataNode.AddAttribute( "sub_genre", ((int)movie.SubGenre).ToString() );
				dataNode.AddAttribute( "show_genre", ((int)movie.ShowGenre).ToString() );
				dataNode.AddAttribute( "reportage_genre", ((int)movie.ReportageGenre).ToString() );
			}

			dataNode.AddAttribute( "blocks", movie.Blocks.ToString() );
			dataNode.AddAttribute( "time", movie.LiveHour.ToString() );

			if ( dbVersion != DatabaseVersion.V2 )
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
			TVTDataStatus defaultStatus = TVTDataStatus.FakeWithRefId;

			var doc = new XmlDocument();
			doc.Load( filename );

			var versionElement = doc.GetElementsByTagName( "version" );
			if ( versionElement[0].HasAttribute( "value" ) )
			{
				version = versionElement[0].GetAttributeInteger( "value" );

				if ( version == 1 )
					throw new NotSupportedException( "database version '1' is not supported." );
			}

			if ( version == 2 )
			{
				var exportOptions = doc.GetElementsByTagName( "exportOptions" );
				if ( bool.Parse( exportOptions[0].GetAttribute( "onlyFakes" ) ) )
				{
					defaultStatus = TVTDataStatus.Fake;
				}
			}

			var allMovies = doc.GetElementsByTagName( "allmovies" );

			foreach ( XmlNode xmlMovie in allMovies )
			{
				foreach ( XmlNode childNode in xmlMovie.ChildNodes )
				{
					var movie = new TVTMovieExtended();
					if ( version == 2 )
					{
						movie.GenerateGuid();
						movie.DataStatus = defaultStatus;
					}

					switch ( childNode.Name )
					{
						case "movie":

							foreach ( XmlLinkedNode attrib in childNode.Attributes )
							{
								switch ( attrib.Name )
								{
									case "id":
										movie.Id = Guid.Parse( attrib.GetElementValue() );
										break;
									case "status":
										movie.DataStatus = (TVTDataStatus)Enum.Parse( typeof( TVTDataStatus ), attrib.GetElementValue() );
										break;
								}
							}

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
										if ( movieChild.HasAttribute( "actorIds" ) )
											movie.Actors = ToPersonList( movieChild.GetAttribute( "actorIds" ), result );
										else
											movie.Actors = ToPersonListByName( movieChild.GetAttribute( "actors" ), result, defaultStatus, TVTPersonFunction.Actor );

										if ( movieChild.HasAttribute( "directorId" ) )
											movie.Director = result.GetPersonByStringId( movieChild.GetAttribute( "directorId" ) );
										else
											movie.Director = GetPersonByNameOrCreate( movieChild.GetAttribute( "director" ), result, defaultStatus, TVTPersonFunction.Actor );

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

		private List<TVTPerson> ToPersonListByName( string names, ITVTowerDatabase<TVTMovieExtended> database, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			var result = new List<TVTPerson>();
			if ( !string.IsNullOrEmpty( names ) )
			{
				var array = names.Split( ',' );
				foreach ( var aValue in array )
				{
					var personName = aValue.Trim();

					var person = database.GetPersonByName( personName );
					if ( person == null )
					{
						person = new TVTPerson();
						person.GenerateGuid();
						person.DataStatus = defaultStatus;
						person.Name = personName;
						person.Functions.Add( functionForNew );
						database.AddPerson( person );
					}

					result.Add( person );
				}
			}
			return result;
		}

		private TVTPerson GetPersonByNameOrCreate( string name, ITVTowerDatabase<TVTMovieExtended> database, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			if ( !string.IsNullOrEmpty( name ) )
			{
				var person = database.GetPersonByName( name );

				if ( person == null )
				{
					person = new TVTPerson();
					person.GenerateGuid();
					person.DataStatus = defaultStatus;
					person.Name = name;
					person.Functions.Add( functionForNew );
					database.AddPerson( person );
				}
				return person;
			}
			else
				return null;
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
			if ( version <= 2 ) //Alte BlitzMax-Datenbank
			{
				movie.GenreOldVersion = movie.MainGenreRaw;
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
					movie.ShowGenre = TVTShowGenre.Undefined;
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
