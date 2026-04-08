using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Size
    {
        public int SizeId { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public decimal? Inches { get; set; }
        public decimal? Centimeters { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
