using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class LocalizationTable
    {
        public LocalizationTable()
        {
            LocalizationCommonJsons = new HashSet<LocalizationCommonJson>();
        }

        public int LocalizationTableId { get; set; }
        public string TableName { get; set; } = null!;

        public virtual ICollection<LocalizationCommonJson> LocalizationCommonJsons { get; set; }
    }
}
