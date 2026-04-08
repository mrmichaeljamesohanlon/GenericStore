using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class VendorsCommissionSetup
    {
        public VendorsCommissionSetup()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int VendorCommissionId { get; set; }
        public int UserId { get; set; }
        public string CommissionType { get; set; } = null!;
        public decimal CommissionValue { get; set; }
        public DateTime ApplicableFrom { get; set; }
        public DateTime ApplicableTo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
