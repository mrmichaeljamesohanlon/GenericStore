using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderRefundRequest
    {
        public int RefundRequestId { get; set; }
        public int OrderId { get; set; }
        public int? TaskId { get; set; }
        public string RefundReasonDesc { get; set; } = null!;
        public int RefundReasonTypeId { get; set; }
        public int CurrencyId { get; set; }
        public bool IsFullRefund { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Currency Currency { get; set; } = null!;
        public virtual RequestsQueue? Task { get; set; }
    }
}
