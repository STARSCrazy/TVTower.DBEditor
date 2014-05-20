using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Entities
{
	public class TVTResourceSchema
	{
		public string Name { get; set; }
		public List<TVTResourceSchemaField> Definitions { get; set; }
	}

	public class TVTResourceSchemaField
	{
		public string Key { get; set; }
		public string Type { get; set; }
		public Dictionary<string, string> PossibleValuesDE { get; set; }
		public Dictionary<string, string> PossibleValuesEN { get; set; }
	}

	public enum TVTResourceSchemaFieldType
	{
		Text,
		Select,
		Reference
	}
}
