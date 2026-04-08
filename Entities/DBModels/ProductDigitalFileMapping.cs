using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductDigitalFileMapping
    {
        public int ProductDigitalFileMappingId { get; set; }
        public int ProductId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
