
namespace TVTower.Entities.Entities
{
	public class TVTAdvertising : TVTEntity
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }

		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public TVTTargetGroup TargetGroup { get; set; }
		public int MinAudience { get; set; }
		public int MinImage { get; set; }
		public int Repetitions { get; set; }
		public int Profit { get; set; }
		public int Penalty { get; set; }
		public int Duration { get; set; }
	}
}