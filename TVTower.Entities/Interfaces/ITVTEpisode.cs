using System.Collections.Generic;
using System;
using CodeKnight.Core;

namespace TVTower.Entities
{
    public interface ITVTEpisode : ITVTProgrammeCore
	{
        int CriticsRate { get; set; }	//0 - 255
        int ViewersRate { get; set; }	//0 - 255	-	auch als Speed bekannt.

        WeakReference<TVTProgramme> SeriesMaster { get; set; }
        int? EpisodeIndex { get; set; }
	}
}
