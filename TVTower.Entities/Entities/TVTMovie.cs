using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTMovie : TVTEpisode
	{
		//public int? DataVersion { get; set; }

		public TVTPerson Director { get; set; }
		public List<TVTPerson> Actors { get; set; }

		public string Country { get; set; }				//ISO-3166-1 ALPHA-2    (http://de.wikipedia.org/wiki/ISO-3166-1-Kodierliste)
		public int Year { get; set; }					//YYYY   = 1900+

		public TVTGenre MainGenre { get; set; }
		public TVTGenre SubGenre { get; set; }
		public TVTShowGenre ShowGenre { get; set; }
		public TVTReportageGenre ReportageGenre { get; set; }

		public int Blocks { get; set; }					//1 - 5
		public int? LiveHour { get; set; }				//0 - 23

		public List<TVTMovieFlags> Flags { get; set; }

		public string ImdbId { get; set; }				//IMDb = Internet Movie Database
		public int? TmdbId { get; set; }					//The Movie DB		
		public int? RottenTomatoesId { get; set; }		//rottentomatoes.com

		public string ImageUrl { get; set; }			//Von hier kann die Bildquelle geladen werden

		//Für Serien
		public bool IsSeries { get; set; }
		public List<TVTEpisode> Episodes { get; set; }

		public TVTMovie()
		{
			Flags = new List<TVTMovieFlags>();
		}
	}
}
