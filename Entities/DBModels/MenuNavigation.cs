using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class MenuNavigation
    {
        public int MenuNavigationId { get; set; }
        public string MenuNavigationName { get; set; } = null!;
        public int? ParentMenuNavigationId { get; set; }
        public int? DisplaySeqNo { get; set; }
        public string? LinkUrl { get; set; }
        public string? IconClass { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string? LocalizationJsonData { get; set; }
        public int? EntityId { get; set; }
    }
}
