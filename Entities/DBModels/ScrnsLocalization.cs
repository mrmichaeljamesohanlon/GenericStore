using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ScrnsLocalization
    {
        public int ScrnLocalizationId { get; set; }
        public int ScreenId { get; set; }
        public int LanguageId { get; set; }
        public int AppModuleId { get; set; }
        public string? LabelsJsonData { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual AppModule AppModule { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
        public virtual Entity Screen { get; set; } = null!;
    }
}
