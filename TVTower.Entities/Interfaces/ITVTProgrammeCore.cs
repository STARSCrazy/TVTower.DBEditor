
using System.Collections.Generic;
using System;
namespace TVTower.Entities
{
    public interface ITVTProgrammeCore : ITVTEntity
	{
        string TitleDE { get; set; }
        string TitleEN { get; set; }
        string DescriptionDE { get; set; }
        string DescriptionEN { get; set; }

        string FakeTitleDE { get; set; }
        string FakeTitleEN { get; set; }

        TVTPerson Director { get; set; }
        List<TVTPerson> Participants { get; set; } //Kann sich in den Episoden unterscheiden
	}
}
