using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class LocalizationCommonJson
    {
        public int LocalCommonDataId { get; set; }
        public int LocalizationTableId { get; set; }
        public int PrimaryKeyId { get; set; }
        public string? LocalizationJsonData { get; set; }

        public virtual LocalizationTable LocalizationTable { get; set; } = null!;
    }
}
