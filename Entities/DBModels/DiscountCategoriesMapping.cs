using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountCategoriesMapping
    {
        public int DiscountCategoryMappingId { get; set; }
        public int DiscountId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Discount Discount { get; set; } = null!;
    }
}
