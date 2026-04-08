using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountManufacturersMapping
    {
        public int DiscountManufacturerMappingId { get; set; }
        public int DiscountId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual Manufacturer Manufacturer { get; set; } = null!;
    }
}
