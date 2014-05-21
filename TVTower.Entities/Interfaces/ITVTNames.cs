using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
    public interface ITVTNames : ITVTEntity
	{
        string TitleDE { get; set; }
        string TitleEN { get; set; }
        string DescriptionDE { get; set; }
        string DescriptionEN { get; set; }

        string FakeTitleDE { get; set; }
        string FakeTitleEN { get; set; }
	}
}
