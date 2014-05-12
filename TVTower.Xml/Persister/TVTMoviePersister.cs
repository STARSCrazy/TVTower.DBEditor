using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTMoviePersister : TVTEpisodePersister<TVTMovie>
	{
		TVTDataStatus defaultStatus = TVTDataStatus.Fake;

		public override void Load( XmlNode xmlNode, TVTMovie movie, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Load( xmlNode, movie, database, dbVersion, dataStructure );

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
					case "references":
						movie.TmdbId = movieChild.GetAttributeInteger( "tmdb_id" );
						movie.ImdbId = movieChild.GetAttribute( "imdb_id" );
						movie.RottenTomatoesId = movieChild.GetAttributeInteger( "rt_id" );
						break;
					case "image_url":
						movie.ImageUrl = movieChild.GetElementValue();
						break;
					case "data":
						if ( movieChild.HasAttribute( "actorIds" ) )
							movie.Actors = ToPersonList( movieChild.GetAttribute( "actorIds" ), database );
						else
							movie.Actors = ToPersonListByName( movieChild.GetAttribute( "actors" ), database, defaultStatus, TVTPersonFunction.Actor );

						if ( movieChild.HasAttribute( "directorId" ) )
							movie.Director = database.GetPersonByStringId( movieChild.GetAttribute( "directorId" ) );
						else
							movie.Director = GetPersonByNameOrCreate( movieChild.GetAttribute( "director" ), database, defaultStatus, TVTPersonFunction.Director );

						movie.Country = movieChild.GetAttribute( "country" );
						movie.Year = movieChild.GetAttributeInteger( "year" );
						movie.Blocks = movieChild.GetAttributeInteger( "blocks" );
						movie.LiveHour = movieChild.GetAttributeInteger( "time" );
						movie.Flags = ToFlagList( movieChild.GetAttribute( "flags" ) );
						movie.TargetGroups = ToTargetGroupList( movieChild.GetAttribute( "target_groups" ) );
						movie.ProPressureGroups = ToPressureGroupList( movieChild.GetAttribute( "pro_pressure_groups" ) );
						movie.ContraPressureGroups = ToPressureGroupList( movieChild.GetAttribute( "contra_pressure_groups" ) );

						//TODO movie.MovieAdditional.GenreOldVersion = movieChild.GetAttributeInteger( "genre" );
						break;
				}
			}
		}

		public override void Save( XmlNode xmlNode, TVTMovie movie, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Save( xmlNode, movie, dbVersion, dataStructure );

			//var nameParser = new TVTNameAndDescriptionPersister();
			//nameParser.Save( xmlNode, episode.Name, dbVersion, dataStructure );

			//    XmlNode movieNode, dataNode;

			//    if ( movie.IsSeries )
			//        throw new NotImplementedException();

			//    movieNode = doc.CreateElement( "movie" );
			//    if ( dbVersion != DatabaseVersion.V2 )
			//    {
			//        movieNode.AddAttribute( "id", movie.Id.ToString() );
			//        movieNode.AddAttribute( "status", movie.DataStatus.ToString() );
			//    }
			//    element.AppendChild( movieNode );


			var dataNode = xmlNode.OwnerDocument.CreateElement( "data" );

			if ( dbVersion == DatabaseVersion.V2 )
			{
				//altes Format
				dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.FakeFullName ).ToContentString( ", " ) );
				dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.FakeFullName : "" );
			}
			else
			{
				xmlNode.AddElement( "image_url", movie.ImageUrl );

				XmlNode referencesNode = xmlNode.OwnerDocument.CreateElement( "references" );
				referencesNode.AddAttribute( "tmdb_id", movie.TmdbId.ToString() );
				referencesNode.AddAttribute( "imdb_id", movie.ImdbId );
				referencesNode.AddAttribute( "rt_id", movie.RottenTomatoesId.HasValue ? movie.RottenTomatoesId.Value.ToString() : "" );
				xmlNode.AppendChild( referencesNode );

				//neues Format
				dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.FakeFullName ).ToContentString( ", " ) );
				dataNode.AddAttribute( "actor_ids", movie.Actors.Select( x => x.Id ).ToContentString( ";" ) );
				dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.FakeFullName : "" );
				dataNode.AddAttribute( "director_id", movie.Director != null ? movie.Director.Id.ToString() : "" );
			}

			dataNode.AddAttribute( "country", movie.Country );
			dataNode.AddAttribute( "year", movie.Year.ToString() );

			//    if ( dbVersion == DatabaseVersion.V2 )
			//    {
			//        dataNode.AddAttribute( "genre", movie.GenreOldVersion.ToString() );
			//    }
			//    else
			//    {
			//        dataNode.AddAttribute( "main_genre", ((int)movie.MainGenre).ToString() );
			//        dataNode.AddAttribute( "sub_genre", ((int)movie.SubGenre).ToString() );
			//        dataNode.AddAttribute( "show_genre", ((int)movie.ShowGenre).ToString() );
			//        dataNode.AddAttribute( "reportage_genre", ((int)movie.ReportageGenre).ToString() );
			//    }

			//    dataNode.AddAttribute( "blocks", movie.Blocks.ToString() );
			//    dataNode.AddAttribute( "time", movie.LiveHour.ToString() );

			//    if ( dbVersion != DatabaseVersion.V2 )
			//    {
			//        dataNode.AddAttribute( "flags", movie.Flags.ToContentString( " " ) );
			//        dataNode.AddAttribute( "target_groups", movie.TargetGroups.ToContentString( " " ) );
			//        dataNode.AddAttribute( "pro_pressure_groups", movie.ProPressureGroups.ToContentString( " " ) );
			//        dataNode.AddAttribute( "contra_pressure_groups", movie.ContraPressureGroups.ToContentString( " " ) );
			//    }

			//    dataNode.AddAttribute( "price", movie.PriceRate.ToString() );
			//    dataNode.AddAttribute( "critics", movie.CriticsRate.ToString() );
			//    dataNode.AddAttribute( "speed", movie.ViewersRate.ToString() );
			//    dataNode.AddAttribute( "outcome", movie.BoxOfficeRate.ToString() );

			//    movieNode.AppendChild( dataNode );

			//    if ( dataStructure == DataStructure.Full )
			//    {

			//        XmlNode additionalNode = doc.CreateElement( "additional" );
			//        additionalNode.AddAttribute( "main_genre_raw", movie.MovieAdditional.MainGenreRaw.ToString() );
			//        additionalNode.AddAttribute( "sub_genre_raw", movie.MovieAdditional.SubGenreRaw.ToString() );
			//        additionalNode.AddAttribute( "genre_old_version", movie.MovieAdditional.GenreOldVersion.ToString() );
			//        additionalNode.AddAttribute( "price_rate_old", movie.MovieAdditional.PriceRateOld.ToString() );
			//        additionalNode.AddAttribute( "critic_rate_old", movie.MovieAdditional.CriticRateOld.ToString() );
			//        additionalNode.AddAttribute( "speed_rate_old", movie.MovieAdditional.SpeedRateOld.ToString() );
			//        additionalNode.AddAttribute( "boxoffice_rate_old", movie.MovieAdditional.BoxOfficeRateOld.ToString() );
			//        additionalNode.AddAttribute( "budget", movie.MovieAdditional.Budget.ToString() );
			//        additionalNode.AddAttribute( "revenue", movie.MovieAdditional.Revenue.ToString() );
			//        movieNode.AppendChild( additionalNode );
			//    }

			//    return movieNode;




            //if ( (int)dbVersion > 2 )
            //    xmlNode.AddAttribute( "betty", episode.BettyBonus.ToString() );

            //xmlNode.AddAttribute( "price", episode.PriceRate.ToString() );
            //xmlNode.AddAttribute( "critics", episode.CriticsRate.ToString() );
            //xmlNode.AddAttribute( "speed", episode.ViewersRate.ToString() );
            //xmlNode.AddAttribute( "outcome", episode.BoxOfficeRate.ToString() );
		}

		private List<TVTPerson> ToPersonList( string value, ITVTDatabase database )
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

        [Obsolete("Gibt ne neue Variant emit gleichem Namen.")]
		private List<TVTPerson> ToPersonListByName( string names, ITVTDatabase database, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
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
						person.ConvertFakeFullname(personName);
						person.Functions.Add( functionForNew );
						database.AddPerson( person );
					}

					result.Add( person );
				}
			}
			return result;
		}

		private TVTPerson GetPersonByNameOrCreate( string name, ITVTDatabase database, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown )
		{
			if ( !string.IsNullOrEmpty( name ) )
			{
				var person = database.GetPersonByName( name );

				if ( person == null )
				{
					person = new TVTPerson();
					person.GenerateGuid();
					person.DataStatus = defaultStatus;
					person.ConvertFakeFullname(name);
					person.Functions.Add( functionForNew );
					database.AddPerson( person );
				}
				return person;
			}
			else
				return null;
		}

		private List<TVTMovieFlag> ToFlagList( string value )
		{
			var result = new List<TVTMovieFlag>();
			if ( !string.IsNullOrEmpty( value ) )
			{
				var array = value.Split( ' ' );
				foreach ( var aValue in array )
				{
					TVTMovieFlag outFlag;
					if ( Enum.TryParse<TVTMovieFlag>( aValue, out outFlag ) )
					{
						result.Add( outFlag );
					}
				}
			}
			return result;
		}

		private List<TVTTargetGroup> ToTargetGroupList( string value )
		{
			var result = new List<TVTTargetGroup>();
			if ( !string.IsNullOrEmpty( value ) )
			{
				var array = value.Split( ' ' );
				foreach ( var aValue in array )
				{
					TVTTargetGroup outFlag;
					if ( Enum.TryParse<TVTTargetGroup>( aValue, out outFlag ) )
					{
						result.Add( outFlag );
					}
				}
			}
			return result;
		}

		private List<TVTPressureGroup> ToPressureGroupList( string value )
		{
			var result = new List<TVTPressureGroup>();
			if ( !string.IsNullOrEmpty( value ) )
			{
				var array = value.Split( ' ' );
				foreach ( var aValue in array )
				{
					TVTPressureGroup outFlag;
					if ( Enum.TryParse<TVTPressureGroup>( aValue, out outFlag ) )
					{
						result.Add( outFlag );
					}
				}
			}
			return result;
		}
	}
}
;