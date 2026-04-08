using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            OrderProductAttributeMappings = new HashSet<OrderProductAttributeMapping>();
            OrderShippingDetails = new HashSet<OrderShippingDetail>();
        }

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? OrderItemGuid { get; set; }
        public decimal Price { get; set; }
        public decimal? ItemPriceTotal { get; set; }
        public int? Quantity { get; set; }
        public decimal? OrderItemShippingChargesTotal { get; set; }
        public decimal? OrderItemAttributeChargesTotal { get; set; }
        public decimal? OrderItemTaxTotal { get; set; }
        public int? DiscountId { get; set; }
        public int? VendorCommissionId { get; set; }
        public decimal? OrderItemDiscountTotal { get; set; }
        public decimal? OrderItemTotal { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual VendorsCommissionSetup? VendorCommission { get; set; }
        public virtual ICollection<OrderProductAttributeMapping> OrderProductAttributeMappings { get; set; }
        public virtual ICollection<OrderShippingDetail> OrderShippingDetails { get; set; }
    }
}
