using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;

namespace TVTower.DBEditor
{
	public class TVTBindingListDatabase : ITVTDatabase
	{
		public SortedBindingList<TVTProgramme> MovieData { get; set; }
        public SortedBindingList<TVTEpisode> EpisodeData { get; set; }        
		public SortedBindingList<TVTPerson> PersonData { get; set; }
        public SortedBindingList<TVTAdvertising> AdvertisingData { get; set; }
        public SortedBindingList<TVTNews> NewsData { get; set; }        

		public void Initialize()
		{
			if ( MovieData == null )
				MovieData = new SortedBindingList<TVTProgramme>();

            if (EpisodeData == null)
                EpisodeData = new SortedBindingList<TVTEpisode>();

            if (PersonData == null)
                PersonData = new SortedBindingList<TVTPerson>();

            if (AdvertisingData == null)
                AdvertisingData = new SortedBindingList<TVTAdvertising>();

            if (NewsData == null)
                NewsData = new SortedBindingList<TVTNews>();                 
		}

		public void SetMovieBindingList( SortedBindingList<TVTProgramme> movieData )
		{
			this.MovieData = movieData;
		}

		public void SetPersonBindingList( SortedBindingList<TVTPerson> personData )
		{
			this.PersonData = personData;
		}

		#region ITVTowerDatabase Members

		public void AddMovie( TVTProgramme movie )
		{
			MovieData.Add( movie );
		}

		public void AddMovies( IEnumerable<TVTProgramme> movies )
		{
			foreach ( var movie in movies )
				AddMovie( movie );
		}

        public void AddEpisode(TVTEpisode episode)
        {
            EpisodeData.Add(episode);
        }

        public void AddEpisodes(IEnumerable<TVTEpisode> episodes)
        {
            foreach (var episode in episodes)
                AddEpisode(episode);
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

        public void AddAdvertising(TVTAdvertising advertising)
        {
            AdvertisingData.Add(advertising);
        }

        public void AddAdvertisings(IEnumerable<TVTAdvertising> advertisings)
        {
            foreach (var person in advertisings)
                AddAdvertising(person);
        }

        public void AddNews(TVTNews news)
        {
            NewsData.Add(news);
        }

        public void AddNews(IEnumerable<TVTNews> news)
        {
            foreach (var currNews in news)
                AddNews(currNews);
        }

		public IEnumerable<TVTProgramme> GetAllMovies( bool withSeries = false )
		{
			var result = new List<TVTProgramme>();
			if ( withSeries )
				result.AddRange( MovieData );
			else
				result.AddRange( MovieData.Where( x => x.ProgrammeType != TVTProgrammeType.Series ) );
			return result;
		}

		public IEnumerable<TVTProgramme> GetAllSeries()
		{
			var result = new List<TVTProgramme>();
            result.AddRange(MovieData.Where(x => x.ProgrammeType == TVTProgrammeType.Series));
			return result;
		}

        public IEnumerable<TVTEpisode> GetAllEpisodes()
        {
            var result = new List<TVTEpisode>();
            result.AddRange(EpisodeData);
            return result;
        }        

		public IEnumerable<TVTPerson> GetAllPeople()
		{
			var result = new List<TVTPerson>();
			result.AddRange( PersonData );
			return result;
		}

        public IEnumerable<TVTAdvertising> GetAllAdvertisings()
        {
            var result = new List<TVTAdvertising>();
            result.AddRange(AdvertisingData);
            return result;
        }

        public IEnumerable<TVTNews> GetAllNews()
        {
            var result = new List<TVTNews>();
            result.AddRange(NewsData);
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
            var result = PersonData.FirstOrDefault(x => x.FullName != null ? x.FullName.Trim() == name.Trim() : false);
            if (result != null)
                return result;
            else
                return PersonData.FirstOrDefault(x => x.FakeFullName != null ? x.FakeFullName.Trim() == name.Trim() : false);
		}

        public void RefreshPersonProgrammeCount()
        {
            foreach (var person in this.PersonData)
            {
                person.ProgrammeCount = 0;
            }

            foreach(var movie in this.MovieData)
            {
                foreach(var person in movie.Participants)
                {
                    var currPerson = GetPersonById(person.Id);
                    currPerson.ProgrammeCount++;                    
                }

                if (movie.Director != null)
                {
                    var currPerson = GetPersonById(movie.Director.Id);
                    currPerson.ProgrammeCount++;
                }
            }

            foreach (var episode in this.EpisodeData)
            {
                foreach (var person in episode.Participants)
                {
                    var currPerson = GetPersonById(person.Id);
                    currPerson.ProgrammeCount++;
                }

                if (episode.Director != null)
                {
                    var currPerson = GetPersonById(episode.Director.Id);
                    currPerson.ProgrammeCount++;
                }
            }

            foreach (var person in PersonData)
            {
                if (person.ProgrammeCount >= 8)
                    person.Prominence = 1;
                else if (person.ProgrammeCount >= 4)
                    person.Prominence = 2;
                else
                    person.Prominence = 3;
            }
        }

        public void RefreshStatus()
        {
            foreach (var person in this.PersonData)
            {
                person.RefreshStatus();
            }

            foreach (var movie in this.MovieData)
            {
                movie.RefreshStatus();
            }

            foreach (var episode in this.EpisodeData)
            {
                episode.RefreshStatus();
            }

            foreach (var advertising in this.AdvertisingData)
            {
                advertising.RefreshStatus();
            }

            foreach (var news in this.NewsData)
            {
                news.RefreshStatus();
            }  
        }

		#endregion
	}
}
