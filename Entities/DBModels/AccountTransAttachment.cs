using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AccountTransAttachment
    {
        public int AcountTransAttachId { get; set; }
        public int TransId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; } = null!;
        public virtual BankAccountTran Trans { get; set; } = null!;
    }
}
