using System.Collections.Generic;
using System;

namespace TVTower.Entities
{
    public interface ITVTEntity
    {
        Guid Id { get; set; }
        string AltId { get; set; }

        TVTDataType DataType { get; set; }
        TVTDataStatus DataStatus { get; set; }

        object Tag { get; set; }
        string AdditionalInfo { get; set; }

        void GenerateGuid();
    }
}
