using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductShippingMethodsMapping
    {
        public int ProductShippingMethodMappingId { get; set; }
        public int ShippingMethodId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ShippingMethod ShippingMethod { get; set; } = null!;
    }
}
