using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class RawOrder
    {
        public int RawOrderId { get; set; }
        public int UserId { get; set; }
        public string? OrderNote { get; set; }
        public string CartJsonData { get; set; } = null!;
        public string? CouponCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal? ShippingSubTotal { get; set; }
        public decimal? OrderTotal { get; set; }
        public string PaymentJsonResponse { get; set; } = null!;
        public string MainOrderExceptionMsg { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
