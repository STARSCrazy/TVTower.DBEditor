using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
    public interface ITVTEntity
    {
        Guid Id { get; set; }
        string AltId { get; set; }

        TVTDataType DataType { get; set; }
        TVTDataContent DataContent { get; set; }
        TVTDataStatus DataStatusDE { get; set; }
        TVTDataStatus DataStatusEN { get; set; }

        bool ApprovedDE { get; set; }
        bool ApprovedEN { get; set; }
        bool Incorrect { get; set; }

        object Tag { get; set; }
        string AdditionalInfo { get; set; }

        void GenerateGuid();
    }
}
