using System;

namespace TVTower.Entities
{
	public class TVTEntity
	{
		public Guid Id { get; set; }

		public TVTDataStatus DataStatus { get; set; }

		public void GenerateGuid()
		{
			Id = Guid.NewGuid();
		}
	}
}
