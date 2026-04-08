using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AttrRam
    {
        public int RamId { get; set; }
        public string RamName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
