using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Category
    {
        public Category()
        {
            DiscountCategoriesMappings = new HashSet<DiscountCategoriesMapping>();
            ProductsCategoriesMappings = new HashSet<ProductsCategoriesMapping>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public int? AttachmentId { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Attachment? Attachment { get; set; }
        public virtual ICollection<DiscountCategoriesMapping> DiscountCategoriesMappings { get; set; }
        public virtual ICollection<ProductsCategoriesMapping> ProductsCategoriesMappings { get; set; }
    }
}
