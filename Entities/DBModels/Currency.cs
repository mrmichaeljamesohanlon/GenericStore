using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Currency
    {
        public Currency()
        {
            Countries = new HashSet<Country>();
            OrderRefundRequests = new HashSet<OrderRefundRequest>();
            OrdersPayments = new HashSet<OrdersPayment>();
        }

        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string? CurrencyCode { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<OrderRefundRequest> OrderRefundRequests { get; set; }
        public virtual ICollection<OrdersPayment> OrdersPayments { get; set; }
    }
}
