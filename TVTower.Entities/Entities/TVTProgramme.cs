using System.Collections.Generic;
using System.Linq;
using CodeKnight.Core;
using System;

namespace TVTower.Entities
{
    public class TVTProgramme : TVTEntity, ITVTProgrammeCore
    {
        public TVTProductType ProductType { get; set; }
        public TVTProgrammeType ProgrammeType { get; set; }

        public string TitleDE { get; set; }
        public string TitleEN { get; set; }
        public string DescriptionDE { get; set; }
        public string DescriptionEN { get; set; }

        public string FakeTitleDE { get; set; }
        public string FakeTitleEN { get; set; }

        public string FakeDescriptionDE { get; set; } //Optional
        public string FakeDescriptionEN { get; set; } //Optional

        public string DescriptionMovieDB { get; set; }

        public IndexingList<TVTStaff> Staff { get; set; }

        public int BettyBonus { get; set; }		//0 - 10
        public float PriceMod { get; set; }		//0 - 10 (Kommas)
        public int CriticsRate { get; set; }	//0 - 255
        public int ViewersRate { get; set; }	//0 - 255	-	auch als Speed bekannt.
        public int BoxOfficeRate { get; set; }	//0 - 255	-	auch als Outcome bekannt.     

        //Fields not for episodes
        public string Country { get; set; }				//ISO-3166-1 ALPHA-2    (http://de.wikipedia.org/wiki/ISO-3166-1-Kodierliste)
        public int Year { get; set; }					//YYYY   = 1900+
        public TVTDistributionChannel DistributionChannel { get; set; }

        public TVTProgrammeGenre MainGenre { get; set; }
        public TVTProgrammeGenre SubGenre { get; set; }

        public int Blocks { get; set; }					//1 - 5
        public int? LiveHour { get; set; }				//0 - 23

        public List<TVTProgrammeFlag> Flags { get; set; }
        public List<TVTTargetGroup> TargetGroups { get; set; }
        public List<TVTPressureGroup> ProPressureGroups { get; set; }
        public List<TVTPressureGroup> ContraPressureGroups { get; set; }

        public string ImdbId { get; set; }				//IMDb = Internet Movie Database
        public int? TmdbId { get; set; }					//The Movie DB		
        public int? RottenTomatoesId { get; set; }		//rottentomatoes.com
        public string ImageUrl { get; set; }			//Von hier kann die Bildquelle geladen werden

        //Für Serien
        public List<TVTProgramme> Episodes { get; set; }
        public TVTMovieAdditional MovieAdditional { get; set; }

        //Für Episoden
        public WeakReference<TVTProgramme> SeriesMaster { get; set; }
        public string MasterId { get; set; } //Redundanz
        public int? EpisodeIndex { get; set; }

        public TVTProgramme()
        {
            Staff = new IndexingList<TVTStaff>();
            Flags = new List<TVTProgrammeFlag>();
            TargetGroups = new List<TVTTargetGroup>();
            ProPressureGroups = new List<TVTPressureGroup>();
            ContraPressureGroups = new List<TVTPressureGroup>();

            BettyBonus = -1;
            PriceMod = -1;
            CriticsRate = -1;
            ViewersRate = -1;
            BoxOfficeRate = -1;
            Year = -1;
            Blocks = -1;
        }

        public override TVTDataStatus RefreshStatus()
        {
            var baseStatus = base.RefreshStatus();
            if ( baseStatus == TVTDataStatus.Incorrect )
                return baseStatus;

            if ( ProductType == TVTProductType.Series )
            {
                var EpisodesIndexes = new List<int>();

                if ( Episodes.Count == 0 )
                    return TVTDataStatus.Incomplete;

                foreach ( var episode in Episodes )
                {
                    var episodeStatus = episode.RefreshStatus();
                    if ( episodeStatus == TVTDataStatus.Incorrect || episodeStatus == TVTDataStatus.Incomplete )
                    {
                        DataStatus = episodeStatus;
                        return DataStatus;
                    }

                    if ( !episode.EpisodeIndex.HasValue || EpisodesIndexes.Contains( episode.EpisodeIndex.Value ) )
                    {
                        DataStatus = TVTDataStatus.Incorrect;
                        return DataStatus;
                    }
                    else
                        EpisodesIndexes.Add( episode.EpisodeIndex.Value );
                }
            }

            if ( ProductType == TVTProductType.Programme &&
                (
                string.IsNullOrEmpty( Country ) ||
                MainGenre == TVTProgrammeGenre.Undefined ||
                MainGenre == TVTProgrammeGenre.Undefined_Show ||
                MainGenre == TVTProgrammeGenre.Undefined_Reportage ||
                Blocks == 0 ||
                Year == 0
                )
                )
            {
                DataStatus = TVTDataStatus.Incomplete;
                return DataStatus;
            }
            else if ( ProductType == TVTProductType.Episode &&
                (
                SeriesMaster == null ||
                !SeriesMaster.IsAlive ||
                SeriesMaster.Target == null ||
                EpisodeIndex == 0 ||
                string.IsNullOrEmpty(MasterId)
                )
                )
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

                //if ( string.IsNullOrEmpty( TitleDE ) ||
                //    string.IsNullOrEmpty( DescriptionDE ) ||
                //    string.IsNullOrEmpty( TitleEN ) ||
                //    string.IsNullOrEmpty( DescriptionEN ) )
                //{
                //    DataStatus = TVTDataStatus.Incomplete;

                if ( DataType != TVTDataType.Fictitious )
                {
                    if ( string.IsNullOrEmpty( FakeTitleDE ) ||
                        string.IsNullOrEmpty( FakeTitleEN ) )
                    {
                        DataStatus = TVTDataStatus.NoFakes;
                    }
                }
                //}
            }



            return DataStatus;
        }

        public override void RefreshReferences( ITVTDatabase database )
        {
            if ( ProductType == TVTProductType.Episode )
            {
                if ( SeriesMaster != null && SeriesMaster.IsAlive )
                    MasterId = SeriesMaster.TargetGeneric.Id.ToString();
            }
            else if ( this.ProductType == TVTProductType.Series )
            {
                Episodes = database.GetEpisodesOfSeries( this.Id ).OrderBy( x => x.EpisodeIndex ).ToList<TVTProgramme>();
                MasterId = Id.ToString();
            }
        }
    }
}