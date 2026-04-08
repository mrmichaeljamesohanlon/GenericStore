using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AttrHdd
    {
        public int HddId { get; set; }
        public string HddName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
