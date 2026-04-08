using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            DiscountManufacturersMappings = new HashSet<DiscountManufacturersMapping>();
            Products = new HashSet<Product>();
        }

        public int ManufacturerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? AddressId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<DiscountManufacturersMapping> DiscountManufacturersMappings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
