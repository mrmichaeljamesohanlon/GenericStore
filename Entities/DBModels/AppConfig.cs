using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AppConfig
    {
        public int AppConfigId { get; set; }
        public string AppConfigKey { get; set; } = null!;
        public string AppConfigValue { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
