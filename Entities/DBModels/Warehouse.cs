using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
