using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTProgramme : TVTEpisode
	{
		public TVTProgrammeType ProgrammeType { get; set; }

		public string Country { get; set; }				//ISO-3166-1 ALPHA-2    (http://de.wikipedia.org/wiki/ISO-3166-1-Kodierliste)
		public int Year { get; set; }					//YYYY   = 1900+
		public int ValidUntilYear { get; set; }			//YYYY   = 1900+ für Live-Events

		public TVTMovieGenre MainGenre { get; set; }
		public TVTMovieGenre SubGenre { get; set; }
		public TVTShowGenre ShowGenre { get; set; }
		public TVTReportageGenre ReportageGenre { get; set; }
		public TVTEventGenre EventGenre { get; set; }

		public int Blocks { get; set; }					//1 - 5
		public int? LiveHour { get; set; }				//0 - 23

		public List<TVTMovieFlag> Flags { get; set; }
		public List<TVTTargetGroup> TargetGroups { get; set; }
		public List<TVTPressureGroup> ProPressureGroups { get; set; }
		public List<TVTPressureGroup> ContraPressureGroups { get; set; }

		public string ImdbId { get; set; }				//IMDb = Internet Movie Database
		public int? TmdbId { get; set; }					//The Movie DB		
		public int? RottenTomatoesId { get; set; }		//rottentomatoes.com

		public string ImageUrl { get; set; }			//Von hier kann die Bildquelle geladen werden

		//Für Serien
		public List<TVTEpisode> Episodes { get; set; }

		public TVTMovieAdditional MovieAdditional { get; set; }

		public TVTProgramme()
		{
			Flags = new List<TVTMovieFlag>();
		}
	}
}
