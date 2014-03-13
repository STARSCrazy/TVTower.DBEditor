
namespace TVTower.Entities
{
	public class TVTEpisode : TVTEntity
	{
		public int? EpisodeNumber { get; set; }

		public string TitleDE { get; set; }
		public string TitleEN { get; set; }

		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public int BettyBonus { get; set; }		//0 - 10
		public int PriceRate { get; set; }		//0 - 255
		public int CriticsRate { get; set; }	//0 - 255
		public int ViewersRate { get; set; }	//0 - 255	-	auch als Speed bekannt.
		public int BoxOfficeRate { get; set; }	//0 - 255	-	auch als Outcome bekannt.
	}
}
