using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class HomeScreenBanner
    {
        public int BannerId { get; set; }
        public string? TopTitle { get; set; }
        public string MainTitle { get; set; } = null!;
        public string? BottomTitle { get; set; }
        public string? LeftButtonText { get; set; }
        public string? LeftButtonUrl { get; set; }
        public string? RightButtonText { get; set; }
        public string? RightButtonUrl { get; set; }
        public int AttachmentId { get; set; }
        public bool IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public int ThemeTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Attachment Attachment { get; set; } = null!;
    }
}
