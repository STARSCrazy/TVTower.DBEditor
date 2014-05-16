using System.Collections.Generic;

namespace TVTower.Entities.Entities
{
	public class TVTAdvertising : TVTEntity
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }
		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public string FakeTitleDE { get; set; }
		public string FakeTitleEN { get; set; }

		public int MinAudience { get; set; }	//0 - 100
		public int MinImage { get; set; }		//0 - 100
		public int Repetitions { get; set; }	//0 - 10
		public int Profit { get; set; }			//0 - 1000
		public int Penalty { get; set; }		//0 - 1000
		public int Duration { get; set; }		//0 - 10

		public TVTTargetGroup TargetGroup { get; set; }

		public List<TVTProgrammeGenre> AllowedMovieGenres { get; set; }
		public List<TVTProgrammeGenre> ProhibitedMovieGenres { get; set; }
		public List<TVTProgrammeType> AllowedProgrammeTypes { get; set; }
		public List<TVTProgrammeType> ProhibitedProgrammeTypes { get; set; }

		public List<TVTPressureGroup> ProPressureGroups { get; set; }
		public List<TVTPressureGroup> ContraPressureGroups { get; set; }
	}
}