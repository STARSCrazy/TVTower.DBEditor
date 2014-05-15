using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTDatabase
	{
		void AddMovie( TVTProgramme movie );
		void AddMovies( IEnumerable<TVTProgramme> movies );

		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

		IEnumerable<TVTProgramme> GetAllMovies( bool withSeries = false );
		IEnumerable<TVTProgramme> GetAllSeries();
		IEnumerable<TVTPerson> GetAllPeople();

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );
	}
}
