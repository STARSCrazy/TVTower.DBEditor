using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Entities
{
	public class TVTResource
	{
		public string Key { get; set; }
		public string Schema { get; set; }
		public Dictionary<string, string> ValuesDE { get; set; }
		public Dictionary<string, string> ValuesEN { get; set; }
	}
}
