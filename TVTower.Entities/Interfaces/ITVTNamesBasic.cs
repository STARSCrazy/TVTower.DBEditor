using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
    public interface ITVTNamesBasic : ITVTEntity
	{
        string TitleDE { get; set; }
        string TitleEN { get; set; }
        string DescriptionDE { get; set; }
        string DescriptionEN { get; set; }
	}
}
