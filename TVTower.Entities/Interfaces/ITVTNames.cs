using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
    public interface ITVTNames : ITVTNamesBasic
	{
        string FakeTitleDE { get; set; }
        string FakeTitleEN { get; set; }
	}
}
