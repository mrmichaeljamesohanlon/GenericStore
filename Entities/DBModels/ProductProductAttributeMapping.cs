using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductProductAttributeMapping
    {
        public int ProductAttributeMappingId { get; set; }
        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }
        public int AttributeValue { get; set; }
        public int? PriceAdjustmentType { get; set; }
        public decimal? PriceAdjustment { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ProductAttribute ProductAttribute { get; set; } = null!;
    }
}
