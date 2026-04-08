using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductsColorsMapping
    {
        public int ProductColorMappingId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
