using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderProductAttributeMapping
    {
        public int OrderAttributeMappingId { get; set; }
        public int ProductAttributeId { get; set; }
        public int OrderItemId { get; set; }
        public int AttributeValue { get; set; }
        public decimal? AttrAdditionalPrice { get; set; }

        public virtual OrderItem OrderItem { get; set; } = null!;
        public virtual ProductAttribute ProductAttribute { get; set; } = null!;
    }
}
