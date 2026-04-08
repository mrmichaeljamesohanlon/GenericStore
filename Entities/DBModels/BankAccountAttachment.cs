using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankAccountAttachment
    {
        public int BankAccountAttachId { get; set; }
        public int BankAccountId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; } = null!;
    }
}
