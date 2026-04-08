using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderShippingDetail
    {
        public int ShippingDetailId { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int? ShipperId { get; set; }
        public int? ShippingMethodId { get; set; }
        public int? ShippingStatusId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool? ReceivedByActualBuyer { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverMobile { get; set; }
        public string? ReceiverIdentityNo { get; set; }
        public string? TrackingNo { get; set; }
        public string? ShipperComment { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual OrderItem OrderItem { get; set; } = null!;
        public virtual User? Shipper { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual OrderStatus? ShippingStatus { get; set; }
    }
}
