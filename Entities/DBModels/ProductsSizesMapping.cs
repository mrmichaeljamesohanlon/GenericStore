using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductsSizesMapping
    {
        public int ProductSizeMappingId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
