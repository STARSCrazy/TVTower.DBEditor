using System;

namespace TVTower.Entities
{
	public abstract class TVTEntity : ITVTEntity
	{
		public Guid Id { get; set; }
        public string AltId { get; set; }

        public TVTDataType DataType { get; set; }
        public TVTDataStatus DataStatus { get; set; }

        public object Tag { get; set; }
        public string AdditionalInfo { get; set; }
        public bool Approved { get; set; } //TODO: Kommt wieder weg!

		public void GenerateGuid()
		{
			Id = Guid.NewGuid();
		}

        public virtual TVTDataStatus RefreshStatus()
        {
            DataStatus = TVTDataStatus.Complete;

            if (Id == Guid.Empty)
            {
                DataStatus = TVTDataStatus.Incorrect;
                return DataStatus;
            }

            if (Approved)
            {
                DataStatus = TVTDataStatus.Approved;
            }

            return DataStatus;
        }
	}
}
