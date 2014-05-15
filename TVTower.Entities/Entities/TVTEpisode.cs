
using System.Collections.Generic;
namespace TVTower.Entities
{
	public class TVTEpisode : TVTEntity
	{
		public int? EpisodeIndex { get; set; }

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
	}
}
