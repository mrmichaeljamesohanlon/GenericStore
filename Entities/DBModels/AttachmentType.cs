using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AttachmentType
    {
        public AttachmentType()
        {
            Attachments = new HashSet<Attachment>();
        }

        public int AttachmentTypeId { get; set; }
        public string AttachmentTypeName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
