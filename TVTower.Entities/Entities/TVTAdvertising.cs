using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTAdvertising : TVTEntity, ITVTNamesBasic
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }
		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public bool Infomercial { get; set; }
		public int Quality { get; set; }        //Wenn als Infomercial gesendet!

		public bool FixPrice { get; set; }
		public float MinAudience { get; set; }	//0 - 100
		public int MinImage { get; set; }		//0 - 100
		public int Repetitions { get; set; }	//0 - 10
		public int Duration { get; set; }		//0 - 10
		public int Profit { get; set; }			//0 - 1000
		public int Penalty { get; set; }		//0 - 1000		

		public TVTTargetGroup TargetGroup { get; set; }

		public List<TVTProgrammeGenre> AllowedGenres { get; set; }
		public List<TVTProgrammeGenre> ProhibitedGenres { get; set; }
		public List<TVTProgrammeType> AllowedProgrammeTypes { get; set; }
		public List<TVTProgrammeType> ProhibitedProgrammeTypes { get; set; }

		public List<TVTPressureGroup> ProPressureGroups { get; set; }
		public List<TVTPressureGroup> ContraPressureGroups { get; set; }

		public TVTAdvertising()
		{
			ProPressureGroups = new List<TVTPressureGroup>();
			ContraPressureGroups = new List<TVTPressureGroup>();
		}
	}
}