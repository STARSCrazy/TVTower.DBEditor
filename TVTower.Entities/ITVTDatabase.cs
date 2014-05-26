using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTDatabase
	{
		void AddMovie( TVTProgramme movie );
		void AddMovies( IEnumerable<TVTProgramme> movies );

        void AddEpisode(TVTEpisode episode);
        void AddEpisodes(IEnumerable<TVTEpisode> episodes);

		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

        void AddAdvertising(TVTAdvertising advertising);
        void AddAdvertisings(IEnumerable<TVTAdvertising> advertisings);

        void AddNews(TVTNews news);
        void AddNews(IEnumerable<TVTNews> news);

		IEnumerable<TVTProgramme> GetAllMovies( bool withSeries = false );
		IEnumerable<TVTProgramme> GetAllSeries();
        IEnumerable<TVTEpisode> GetAllEpisodes();
		IEnumerable<TVTPerson> GetAllPeople();
        IEnumerable<TVTAdvertising> GetAllAdvertisings();
        IEnumerable<TVTNews> GetAllNews();

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );

        void RefreshPersonProgrammeCount();
	}
}
