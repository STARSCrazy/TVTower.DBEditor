using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;

namespace TVTower.DBEditor
{
	public class TVTBindingListDatabase : ITVTDatabase
	{
		public SortedBindingList<TVTMovie> MovieData { get; set; }
		public SortedBindingList<TVTPerson> PersonData { get; set; }

		public void Initialize()
		{
			if ( MovieData == null )
				MovieData = new SortedBindingList<TVTMovie>();

			if ( PersonData == null )
				PersonData = new SortedBindingList<TVTPerson>();
		}

		public void SetMovieBindingList( SortedBindingList<TVTMovie> movieData )
		{
			this.MovieData = movieData;
		}

		public void SetPersonBindingList( SortedBindingList<TVTPerson> personData )
		{
			this.PersonData = personData;
		}

		#region ITVTowerDatabase Members

		public void AddMovie( TVTMovie movie )
		{
			MovieData.Add( movie );
		}

		public void AddMovies( IEnumerable<TVTMovie> movies )
		{
			foreach ( var movie in movies )
				AddMovie( movie );
		}

		public void AddPerson( TVTPerson person )
		{
			PersonData.Add( person );
		}

		public void AddPeople( IEnumerable<TVTPerson> people )
		{
			foreach ( var person in people )
				AddPerson( person );
		}

		public IEnumerable<TVTMovie> GetAllMovies( bool withSeries = false )
		{
			var result = new List<TVTMovie>();
			if ( withSeries )
				result.AddRange( MovieData );
			else
				result.AddRange( MovieData.Where( x => !x.IsSeries ) );
			return result;
		}

		public IEnumerable<TVTMovie> GetAllSeries()
		{
			var result = new List<TVTMovie>();
			result.AddRange( MovieData.Where( x => x.IsSeries ) );
			return result;
		}

		public IEnumerable<TVTPerson> GetAllPeople()
		{
			var result = new List<TVTPerson>();
			result.AddRange( PersonData );
			return result;
		}

		public TVTPerson GetPersonById( Guid id )
		{
			return PersonData.FirstOrDefault( x => x.Id == id );
		}

		public TVTPerson GetPersonByStringId( string id )
		{
			Guid guidId;
			if ( Guid.TryParse( id, out guidId ) )
				return GetPersonById( guidId );
			else
				return null;
		}

		public TVTPerson GetPersonByTmdbId( int tmdbId )
		{
			return PersonData.FirstOrDefault( x => x.TmdbId == tmdbId );
		}

		public TVTPerson GetPersonByName( string name )
		{
			return PersonData.FirstOrDefault( x => x.Name.Trim() == name.Trim() );
		}

		#endregion
	}
}
