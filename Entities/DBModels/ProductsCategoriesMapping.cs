using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductsCategoriesMapping
    {
        public int ProductCategoryMappingId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
