using System;

namespace TVTower.Entities
{
	public abstract class TVTEntity : ITVTEntity
	{
		public string Id { get; set; }
		public string OldId { get; set; }

		public bool OnlyReference { get; set; }

		public TVTDataType DataType { get; set; }
		public TVTDataStatus DataStatus { get; set; }
		public TVTDataRoot DataRoot { get; set; }

		public object Tag { get; set; }
		public string AdditionalInfo { get; set; }
		public bool Approved { get; set; } //TODO: Kommt wieder weg!

		public string CreatorId { get; set; }
		public string EditorId { get; set; }
		public DateTime LastModified { get; set; }

		public bool IsNew { get; set; } //IsNew-Flag für das schreiben in die DB.
		public bool IsChanged { get; set; } //IsChanged-Flag für das schreiben in die DB.

		public TVTEntity MySelf
		{
			get { return this; }
		}

		public virtual void GenerateGuid()
		{
			throw new NotImplementedException();
		}

		public virtual TVTDataStatus RefreshStatus()
		{
			DataStatus = TVTDataStatus.Complete;

			if ( string.IsNullOrEmpty( Id ) )
			{
				DataStatus = TVTDataStatus.Incorrect;
				return DataStatus;
			}

			if ( Approved )
			{
				DataStatus = TVTDataStatus.Approved;
			}

			return DataStatus;
		}

		public virtual void RefreshReferences( ITVTDatabase database )
		{
		}

		public override string ToString()
		{
			return Id.ToString();
		}
	}
}
