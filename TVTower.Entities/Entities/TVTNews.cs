using System;
using System.Collections.Generic;
using TVTower.Entities.Enums;

namespace TVTower.Entities
{
	public class TVTNews : TVTEntity
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }
		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public TVTNewsGenre Genre { get; set; }

		public int TimeRangeBegin { get; set; } //-1 oder 0 - 24
		public int TimeRangeEnd { get; set; }  //-1 oder 0 - 24
		public int EpisodeIndex { get; set; }

		public int Price { get; set; }		//0 - 1000		
		public int Topicality { get; set; } //0 - 100

		public TVTNewsEffect Effect { get; set; }
		public int EffectValue1 { get; set; }
		public int EffectValue2 { get; set; }
		public int EffectValue3 { get; set; }
		public int EffectValue4 { get; set; }

		public List<Guid> MakeEventProgrammesAvailable { get; set; }
		public List<Guid> MakeEventProgrammesUnavailable { get; set; }

		public List<TVTPressureGroup> ProPressureGroups { get; set; }
		public List<TVTPressureGroup> ContraPressureGroups { get; set; }
	}
}