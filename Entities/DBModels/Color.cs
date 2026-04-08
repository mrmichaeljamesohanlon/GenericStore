using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Color
    {
        public Color()
        {
            ProductPicturesMappings = new HashSet<ProductPicturesMapping>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; } = null!;
        public string? HexCode { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<ProductPicturesMapping> ProductPicturesMappings { get; set; }
    }
}
