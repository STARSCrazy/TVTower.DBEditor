using System;
using System.Collections.Generic;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTMoviePersister<T> : TVTEpisodePersister<T>
		where T : TVTMovie
	{
		TVTDataStatus defaultStatus = TVTDataStatus.Fake;

		public override void Load( XmlNode xmlNode, T movie, ITVTDatabase database )
		{
			base.Load( xmlNode, movie, database );

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
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
						if ( movieChild.HasAttribute( "actorIds" ) )
							movie.Actors = ToPersonList( movieChild.GetAttribute( "actorIds" ), database );
						else
							movie.Actors = ToPersonListByName( movieChild.GetAttribute( "actors" ), database, defaultStatus, TVTPersonFunction.Actor );

						if ( movieChild.HasAttribute( "directorId" ) )
							movie.Director = database.GetPersonByStringId( movieChild.GetAttribute( "directorId" ) );
						else
							movie.Director = GetPersonByNameOrCreate( movieChild.GetAttribute( "director" ), database, defaultStatus, TVTPersonFunction.Actor );

						movie.Country = movieChild.GetAttribute( "country" );
						movie.Year = movieChild.GetAttributeInteger( "year" );
						movie.Blocks = movieChild.GetAttributeInteger( "blocks" );
						movie.LiveHour = movieChild.GetAttributeInteger( "time" );
						movie.Flags = ToFlagList( movieChild.GetAttribute( "flags" ) );
						movie.TargetGroups = ToTargetGroupList( movieChild.GetAttribute( "target_groups" ) );
						movie.ProPressureGroups = ToPressureGroupList( movieChild.GetAttribute( "pro_pressure_groups" ) );
						movie.ContraPressureGroups = ToPressureGroupList( movieChild.GetAttribute( "contra_pressure_groups" ) );
						break;
				}
			}
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
						person.Name = personName;
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
					person.Name = name;
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