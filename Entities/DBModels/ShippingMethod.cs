using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class ShippingMethod
    {
        public ShippingMethod()
        {
            OrderShippingDetails = new HashSet<OrderShippingDetail>();
            ProductShippingMethodsMappings = new HashSet<ProductShippingMethodsMapping>();
        }

        public int ShippingMethodId { get; set; }
        public string? MethodName { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<OrderShippingDetail> OrderShippingDetails { get; set; }
        public virtual ICollection<ProductShippingMethodsMapping> ProductShippingMethodsMappings { get; set; }
    }
}
