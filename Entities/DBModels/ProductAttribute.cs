using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ProductAttribute
    {
        public ProductAttribute()
        {
            OrderProductAttributeMappings = new HashSet<OrderProductAttributeMapping>();
            ProductProductAttributeMappings = new HashSet<ProductProductAttributeMapping>();
        }

        public int ProductAttributeId { get; set; }
        public string AttributeName { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? AttributeSqlTableName { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderProductAttributeMapping> OrderProductAttributeMappings { get; set; }
        public virtual ICollection<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; }
    }
}
