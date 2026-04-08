using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Language
    {
        public Language()
        {
            ScrnsLocalizations = new HashSet<ScrnsLocalization>();
        }

        public int LanguageId { get; set; }
        public string LanguageCode { get; set; } = null!;
        public string LanguageName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? FlagUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<ScrnsLocalization> ScrnsLocalizations { get; set; }
    }
}
