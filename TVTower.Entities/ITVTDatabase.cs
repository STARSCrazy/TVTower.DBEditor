using System;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public interface ITVTDatabase
	{
		void AddProgramme( TVTProgramme movie );
		void AddProgrammes( IEnumerable<TVTProgramme> movies );

		//void AddEpisode( TVTEpisode episode );
		//void AddEpisodes( IEnumerable<TVTEpisode> episodes );

		void AddPerson( TVTPerson person );
		void AddPeople( IEnumerable<TVTPerson> people );

		void AddAdvertising( TVTAdvertising advertising );
		void AddAdvertisings( IEnumerable<TVTAdvertising> advertisings );

		void AddNews( TVTNews news );
		void AddNews( IEnumerable<TVTNews> news );

		IEnumerable<TVTProgramme> GetAllProgrammes( bool withSeries = false, bool withEpisodes = false );
		IEnumerable<TVTProgramme> GetAllSeries();
		IEnumerable<TVTProgramme> GetAllEpisodes();
		IEnumerable<TVTPerson> GetAllPeople();
		IEnumerable<TVTAdvertising> GetAllAdvertisings();
		IEnumerable<TVTNews> GetAllNews();

		IEnumerable<TVTProgramme> GetEpisodesOfSeries( Guid seriesId );

		TVTPerson GetPersonById( Guid id );
		TVTPerson GetPersonByStringId( string id );
		TVTPerson GetPersonByTmdbId( int tmdbId );
		TVTPerson GetPersonByName( string name );

		TVTNews GetNewsThreadInitial( string threadId );

		void RefreshPersonProgrammeCount();
		void RefreshReferences();
		void RefreshStatus();

		void Clear();
	}
}
