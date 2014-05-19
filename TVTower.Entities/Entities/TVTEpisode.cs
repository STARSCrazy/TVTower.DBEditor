using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public TVTPerson Director { get; set; }
        public List<TVTPerson> Participants { get; set; }

        public int CriticsRate { get; set; }
        public int ViewersRate { get; set; }

        public WeakReference<TVTProgramme> SeriesMaster { get; set; }
        public int? EpisodeIndex { get; set; }

        public override TVTDataStatus RefreshStatus()
        {
            var baseStatus = base.RefreshStatus();
            if (baseStatus == TVTDataStatus.Incorrect)
                return baseStatus;

            if (string.IsNullOrEmpty(TitleDE) ||
                string.IsNullOrEmpty(TitleEN) ||
                string.IsNullOrEmpty(DescriptionDE) ||
                string.IsNullOrEmpty(DescriptionEN) ||
                !SeriesMaster.IsAlive ||
                SeriesMaster.Target == null ||
                EpisodeIndex == 0)
            {
                DataStatus = TVTDataStatus.Incomplete;
                return DataStatus;
            }
            else
            {
                if (string.IsNullOrEmpty(FakeTitleDE) ||
                    string.IsNullOrEmpty(FakeTitleEN))
                {
                    DataStatus = TVTDataStatus.NoFakes;
                }
            }

            return DataStatus;
        }
    }
}
