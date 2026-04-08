using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderRefundReasonType
    {
        public int RefundReasonTypeId { get; set; }
        public string ReasonName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
