using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrdersPayment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string MilestoneName { get; set; } = null!;
        public decimal MilestoneValue { get; set; }
        public int CurrencyId { get; set; }
        public bool IsCompleted { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Guid { get; set; }
        public string? TransactionNo { get; set; }
        public string? StripeResponseJson { get; set; }
        public string? StripeBalanceTransactionId { get; set; }
        public string? StripeChargeId { get; set; }
        public string? PayPalResponseJson { get; set; }

        public virtual Currency Currency { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    }
}
