using System;

namespace TVTower.Entities
{
	public class TVTEntity
	{
		public Guid Id { get; set; }
        public string AltId { get; set; }

        public TVTDataType DataType { get; set; }
		public TVTDataContent DataContent { get; set; }
        public TVTDataStatus DataStatusDE { get; set; }
        public TVTDataStatus DataStatusEN { get; set; }

        public bool ApprovedDE { get; set; }
        public bool ApprovedEN { get; set; }
        public bool Incorrect { get; set; }

        public object Tag { get; set; }
        public string AdditionalInfo { get; set; }        

		public void GenerateGuid()
		{
			Id = Guid.NewGuid();
		}
	}
}
