using System;
using System.Linq;
using TVTower.DBEditor;
using System.Collections.Generic;
using TVTower.Entities;

namespace TVTower.DBEditor
{
	public class TVTBindingListDatabase<T> : ITVTowerDatabase<T>
		where T : TVTMovie
	{
		public SortedBindingList<T> MovieData { get; set; }
		public SortedBindingList<TVTPerson> PersonData { get; set; }

		public void Initialize()
		{
			if ( MovieData == null )
				MovieData = new SortedBindingList<T>();

			if ( PersonData == null )
				PersonData = new SortedBindingList<TVTPerson>();
		}

		public void SetMovieBindingList( SortedBindingList<T> movieData )
		{
			this.MovieData = movieData;
		}

		public void SetPersonBindingList( SortedBindingList<TVTPerson> personData )
		{
			this.PersonData = personData;
		}

		#region ITVTowerDatabase Members

		public void AddMovie( T movie )
		{
			MovieData.Add( movie );
		}

		public void AddMovies( IEnumerable<T> movies )
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

		public IEnumerable<T> GetAllMovies()
		{
			var result = new List<T>();
			result.AddRange(MovieData);
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

		#endregion
	}
}
