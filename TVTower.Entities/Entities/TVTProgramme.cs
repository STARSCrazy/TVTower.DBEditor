using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
    public class TVTProgramme : TVTEntity, ITVTEpisode
	{
		public TVTProgrammeType ProgrammeType { get; set; }
                
        public string TitleDE { get; set; }
        public string TitleEN { get; set; }
        public string DescriptionDE { get; set; }
        public string DescriptionEN { get; set; }

        public string FakeTitleDE { get; set; }
        public string FakeTitleEN { get; set; }

        public string DescriptionMovieDB { get; set; }

        public TVTPerson Director { get; set; }
        public List<TVTPerson> Participants { get; set; } //Kann sich in den Episoden unterscheiden

        public int BettyBonus { get; set; }		//0 - 10
        public int PriceMod { get; set; }		//0 - 255
        public int CriticsRate { get; set; }	//0 - 255
        public int ViewersRate { get; set; }	//0 - 255	-	auch als Speed bekannt.
        public int BoxOfficeRate { get; set; }	//0 - 255	-	auch als Outcome bekannt.     

        public int? EpisodeIndex { get; set; }
        public WeakReference<TVTProgramme> SeriesMaster { get; set; }


        //Fields not for episodes
        public string Country { get; set; }				//ISO-3166-1 ALPHA-2    (http://de.wikipedia.org/wiki/ISO-3166-1-Kodierliste)
		public int Year { get; set; }					//YYYY   = 1900+
		public int ValidUntilYear { get; set; }			//YYYY   = 1900+ für Live-Events

		public TVTProgrammeGenre MainGenre { get; set; }
		public TVTProgrammeGenre SubGenre { get; set; }

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
		public List<ITVTEpisode> Episodes { get; set; }
		public TVTMovieAdditional MovieAdditional { get; set; }


		public TVTProgramme()
		{
			Flags = new List<TVTMovieFlag>();
            TargetGroups = new List<TVTTargetGroup>();
		}
	}
}
