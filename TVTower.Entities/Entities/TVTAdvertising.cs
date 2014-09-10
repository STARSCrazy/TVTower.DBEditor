using System.Collections.Generic;

namespace TVTower.Entities
{
    public class TVTAdvertising : TVTEntity, ITVTNames
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }
		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public string FakeTitleDE { get; set; }
		public string FakeTitleEN { get; set; }
        public string FakeDescriptionDE { get; set; }
        public string FakeDescriptionEN { get; set; }

        public bool Infomercial { get; set; }
        public int Quality { get; set; }        //Wenn als Infomercial gesendet!

        public bool FlexibleProfit { get; set; }
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
	}
}