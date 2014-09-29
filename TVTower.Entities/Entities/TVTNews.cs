using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTNews : TVTEntity, ITVTNamesBasic
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }
		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public TVTNewsType NewsType { get; set; }
		public string NewsThreadId { get; set; }
		public TVTNewsGenre Genre { get; set; }

		public int Price { get; set; }		//0 - 1000		
		public int Topicality { get; set; } //0 - 100

		public int FixYear { get; set; }
		public int AvailableAfterDays { get; set; }
		public int YearRangeFrom { get; set; }
		public int YearRangeTo { get; set; }
		public int MinHoursAfterPredecessor { get; set; }
		public int TimeRangeFrom { get; set; } //-1 oder 0 - 24
		public int TimeRangeTo { get; set; }  //-1 oder 0 - 24

		public string Resource1Type { get; set; }
		public string Resource2Type { get; set; }
		public string Resource3Type { get; set; }
		public string Resource4Type { get; set; }

		public List<TVTNewsEffect> Effects { get; set; }

		public List<TVTPressureGroup> ProPressureGroups { get; set; }
		public List<TVTPressureGroup> ContraPressureGroups { get; set; }

		public TVTNews NewsThreadInitial { get; set; }

		public TVTNews()
		{
			Effects = new List<TVTNewsEffect>();
			ProPressureGroups = new List<TVTPressureGroup>();
			ContraPressureGroups = new List<TVTPressureGroup>();
		}

		public override TVTDataStatus RefreshStatus()
		{
			var baseStatus = base.RefreshStatus();
			if ( baseStatus == TVTDataStatus.Incorrect )
				return baseStatus;

			//if ( NewsType != TVTNewsType.InitialNews && string.IsNullOrEmpty( NewsThreadId ) )
			//{
			//    DataStatus = TVTDataStatus.Incorrect;
			//    return DataStatus;
			//}

			//if (NewsType == TVTNewsType.FollowingNews && Predecessor == null)
			//{
			//    DataStatus = TVTDataStatus.Incorrect;
			//    return DataStatus;
			//}

			//if (Predecessor != null && NewsThreadId != Predecessor.NewsThreadId)
			//{
			//    DataStatus = TVTDataStatus.Incorrect;
			//    return DataStatus;
			//}

			if ( DataStatus == TVTDataStatus.Complete )
			{
				DataStatus = TVTDataStatus.Approved;
				Approved = true; //TODO
				return DataStatus;
			}

			return DataStatus;
		}

		public override void RefreshReferences( ITVTDatabase database )
		{
			base.RefreshReferences( database );

			if ( !string.IsNullOrEmpty( NewsThreadId ) )
			{
				NewsThreadInitial = database.GetNewsThreadInitial( NewsThreadId );
			}
		}
	}
}