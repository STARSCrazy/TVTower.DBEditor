using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTowerDatabase<T>
		where T : TVTMovie
	{
		void AddMovie( T movie );
		void AddMovies( IEnumerable<T> movies );

		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

		IEnumerable<T> GetAllMovies();
		IEnumerable<TVTPerson> GetAllPeople();

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );
	}
}
