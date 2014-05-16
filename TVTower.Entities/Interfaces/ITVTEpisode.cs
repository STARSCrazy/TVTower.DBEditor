
using System.Collections.Generic;
using System;
namespace TVTower.Entities
{
    public interface ITVTEpisode : ITVTEntity
	{
        string TitleDE { get; set; }
        string TitleEN { get; set; }
        string DescriptionDE { get; set; }
        string DescriptionEN { get; set; }

        string FakeTitleDE { get; set; }
        string FakeTitleEN { get; set; }

        string DescriptionMovieDB { get; set; }

        TVTPerson Director { get; set; }
        List<TVTPerson> Participants { get; set; } //Kann sich in den Episoden unterscheiden

        int BettyBonus { get; set; }		//0 - 10
        int PriceMod { get; set; }		//0 - 255
        int CriticsRate { get; set; }	//0 - 255
        int ViewersRate { get; set; }	//0 - 255	-	auch als Speed bekannt.
        int BoxOfficeRate { get; set; }	//0 - 255	-	auch als Outcome bekannt.     

        int? EpisodeIndex { get; set; }   
	}
}
