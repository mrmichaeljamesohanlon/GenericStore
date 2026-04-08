using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Tag
    {
        public Tag()
        {
            ProductsTagsMappings = new HashSet<ProductsTagsMapping>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<ProductsTagsMapping> ProductsTagsMappings { get; set; }
    }
}
