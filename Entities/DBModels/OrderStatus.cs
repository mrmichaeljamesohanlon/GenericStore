using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderShippingDetails = new HashSet<OrderShippingDetail>();
            OrderStatusesMappings = new HashSet<OrderStatusesMapping>();
            Orders = new HashSet<Order>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<OrderShippingDetail> OrderShippingDetails { get; set; }
        public virtual ICollection<OrderStatusesMapping> OrderStatusesMappings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
