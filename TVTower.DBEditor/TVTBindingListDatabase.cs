using System;
using System.Collections.Generic;
using System.Linq;
using TVTower.Entities;

namespace TVTower.DBEditor
{
	public class TVTBindingListDatabase : ITVTDatabase
	{
		public SortedBindingList<TVTProgramme> MovieData { get; set; }
		public SortedBindingList<TVTPerson> PersonData { get; set; }

		public void Initialize()
		{
			if ( MovieData == null )
				MovieData = new SortedBindingList<TVTProgramme>();

			if ( PersonData == null )
				PersonData = new SortedBindingList<TVTPerson>();
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

		public void AddPerson( TVTPerson person )
		{
			PersonData.Add( person );
		}

		public void AddPeople( IEnumerable<TVTPerson> people )
		{
			foreach ( var person in people )
				AddPerson( person );
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
        }

		#endregion
	}
}
