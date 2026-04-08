using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class OrderStatusesMapping
    {
        public int OrderStatusMappingId { get; set; }
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual OrderStatus Status { get; set; } = null!;
    }
}
