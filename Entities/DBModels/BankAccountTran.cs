using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankAccountTran
    {
        public BankAccountTran()
        {
            AccountTransAttachments = new HashSet<AccountTransAttachment>();
        }

        public int TransId { get; set; }
        public int BankAccountId { get; set; }
        public int EventId { get; set; }
        public decimal TransAmount { get; set; }
        public string TransType { get; set; } = null!;
        public string? TransTitle { get; set; }
        public string? Description { get; set; }
        public DateTime ProcessingDate { get; set; }
        public int TransCurrencyId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? TransJsonData { get; set; }
        public string? TransExtraAttributesData { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<AccountTransAttachment> AccountTransAttachments { get; set; }
    }
}
