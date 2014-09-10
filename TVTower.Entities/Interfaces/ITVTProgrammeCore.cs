using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
	public interface ITVTProgrammeCore : ITVTNames
	{
		//TVTPerson Director { get; set; }
		//List<TVTPerson> Participants { get; set; } //Kann sich in den Episoden unterscheiden
		IndexingList<TVTStaff> Staff { get; set; }
	}
}
