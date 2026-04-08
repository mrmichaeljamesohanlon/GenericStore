using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Discount
    {
        public Discount()
        {
            DiscountCategoriesMappings = new HashSet<DiscountCategoriesMapping>();
            DiscountCitiesMappings = new HashSet<DiscountCitiesMapping>();
            DiscountManufacturersMappings = new HashSet<DiscountManufacturersMapping>();
            DiscountProductsMappings = new HashSet<DiscountProductsMapping>();
            DiscountUsageHistories = new HashSet<DiscountUsageHistory>();
        }

        public int DiscountId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int DiscountTypeId { get; set; }
        public int DiscountValueType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? CouponCode { get; set; }
        public int? MaxQuantity { get; set; }
        public bool IsBoundToMaxQuantity { get; set; }
        public bool? IsCouponCodeRequired { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual DiscountType DiscountType { get; set; } = null!;
        public virtual ICollection<DiscountCategoriesMapping> DiscountCategoriesMappings { get; set; }
        public virtual ICollection<DiscountCitiesMapping> DiscountCitiesMappings { get; set; }
        public virtual ICollection<DiscountManufacturersMapping> DiscountManufacturersMappings { get; set; }
        public virtual ICollection<DiscountProductsMapping> DiscountProductsMappings { get; set; }
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }
    }
}
