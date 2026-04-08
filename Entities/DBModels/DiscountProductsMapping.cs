using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountProductsMapping
    {
        public int DiscountProductMappingId { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
