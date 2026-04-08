using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderNote
    {
        public int OrderNoteId { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; } = null!;
        public int? ParentOrderNoteId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
