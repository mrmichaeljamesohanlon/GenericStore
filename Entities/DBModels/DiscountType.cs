using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountType
    {
        public DiscountType()
        {
            Discounts = new HashSet<Discount>();
        }

        public int DiscountTypeId { get; set; }
        public string DiscountTypeName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
