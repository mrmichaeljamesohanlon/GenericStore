using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderNotes = new HashSet<OrderNote>();
            OrderShippingDetails = new HashSet<OrderShippingDetail>();
            OrderStatusesMappings = new HashSet<OrderStatusesMapping>();
            OrdersPayments = new HashSet<OrdersPayment>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDateUtc { get; set; }
        public int? LatestStatusId { get; set; }
        public int ShippingAddressId { get; set; }
        public decimal? OrderSubtotalInclTax { get; set; }
        public decimal? OrderSubtotalExclTax { get; set; }
        public decimal? OrderTotalDiscountAmount { get; set; }
        public decimal? OrderTotalShippingCharges { get; set; }
        public decimal? OrderTotalAttributeCharges { get; set; }
        public decimal? OrderTax { get; set; }
        public decimal OrderTotal { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual OrderStatus? LatestStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderNote> OrderNotes { get; set; }
        public virtual ICollection<OrderShippingDetail> OrderShippingDetails { get; set; }
        public virtual ICollection<OrderStatusesMapping> OrderStatusesMappings { get; set; }
        public virtual ICollection<OrdersPayment> OrdersPayments { get; set; }
    }
}
