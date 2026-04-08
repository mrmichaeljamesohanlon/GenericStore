using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            BankAccountTrans = new HashSet<BankAccountTran>();
            OrdersPayments = new HashSet<OrdersPayment>();
        }

        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<BankAccountTran> BankAccountTrans { get; set; }
        public virtual ICollection<OrdersPayment> OrdersPayments { get; set; }
    }
}
