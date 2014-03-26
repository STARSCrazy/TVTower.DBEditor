using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTDatabase
	{
		void AddMovie( TVTMovie movie );
		void AddMovies( IEnumerable<TVTMovie> movies );

		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

		IEnumerable<TVTMovie> GetAllMovies( bool withSeries = false );
		IEnumerable<TVTMovie> GetAllSeries();
		IEnumerable<TVTPerson> GetAllPeople();

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );
	}
}
