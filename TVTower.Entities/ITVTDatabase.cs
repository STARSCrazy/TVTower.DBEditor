using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTDatabase
	{
		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

		IEnumerable<TVTPerson> GetAllPeople();

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );
	}

	public interface ITVTowerDatabase<T> : ITVTDatabase
		where T : TVTMovie
	{
		void AddMovie( T movie );
		void AddMovies( IEnumerable<T> movies );

		IEnumerable<T> GetAllMovies( bool withSeries = false );
		IEnumerable<T> GetAllSeries( );
	}
}
