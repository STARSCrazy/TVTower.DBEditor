using System;
using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTEpisode : TVTEntity, ITVTEpisode
	{
		public string TitleDE { get; set; }
		public string TitleEN { get; set; }

		public string DescriptionDE { get; set; }
		public string DescriptionEN { get; set; }

		public string FakeTitleDE { get; set; }
		public string FakeTitleEN { get; set; }

		public string FakeDescriptionDE { get; set; } //Optional
		public string FakeDescriptionEN { get; set; } //Optional

		//public TVTPerson Director { get; set; }
		//public List<TVTPerson> Participants { get; set; }
		public IndexingList<TVTStaff> Staff { get; set; }

		public int CriticsRate { get; set; }
		public int ViewersRate { get; set; }

		public WeakReference<TVTProgramme> SeriesMaster { get; set; }
		public Guid SeriesId { get; set; } //Redundanz
		public int? EpisodeIndex { get; set; }

		public TVTEpisode()
		{
			Staff = new IndexingList<TVTStaff>();
		}

		public override TVTDataStatus RefreshStatus()
		{
			var baseStatus = base.RefreshStatus();
			if ( baseStatus == TVTDataStatus.Incorrect )
				return baseStatus;

			if ( SeriesMaster == null ||
				!SeriesMaster.IsAlive ||
				SeriesMaster.Target == null ||
				EpisodeIndex == 0 ||
				SeriesId == Guid.Empty )
			{
				DataStatus = TVTDataStatus.Incomplete;
				return DataStatus;
			}
			else
			{
				if ( !string.IsNullOrEmpty( TitleDE ) &&
					!string.IsNullOrEmpty( DescriptionDE ) &&
					!string.IsNullOrEmpty( FakeTitleDE ) )
				{
					DataStatus = TVTDataStatus.OnlyDE;
					return DataStatus;
				}

				if ( !string.IsNullOrEmpty( TitleEN ) &&
					!string.IsNullOrEmpty( DescriptionEN ) &&
					!string.IsNullOrEmpty( FakeTitleEN ) )
				{
					DataStatus = TVTDataStatus.OnlyEN;
					return DataStatus;
				}

				if ( DataType != TVTDataType.Fictitious )
				{
					if ( string.IsNullOrEmpty( FakeTitleDE ) ||
						string.IsNullOrEmpty( FakeTitleEN ) )
					{
						DataStatus = TVTDataStatus.NoFakes;
					}
				}

				//if ( string.IsNullOrEmpty( TitleDE ) ||
				//    string.IsNullOrEmpty( DescriptionDE ) ||
				//    string.IsNullOrEmpty( TitleEN ) ||
				//    string.IsNullOrEmpty( DescriptionEN ) )
				//{
				//    DataStatus = TVTDataStatus.Incomplete;

				//    if ( DataType != TVTDataType.Fictitious )
				//    {
				//        if ( string.IsNullOrEmpty( FakeTitleDE ) ||
				//            string.IsNullOrEmpty( FakeTitleEN ) )
				//        {
				//            DataStatus = TVTDataStatus.NoFakes;
				//        }
				//    }
				//}
			}

			return DataStatus;
		}

		public override void RefreshReferences( ITVTDatabase database )
		{
			if ( SeriesMaster != null && SeriesMaster.IsAlive )
				SeriesId = SeriesMaster.TargetGeneric.Id;
		}
	}
}
