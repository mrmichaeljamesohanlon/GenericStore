using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Product
    {
        public Product()
        {
            DiscountProductsMappings = new HashSet<DiscountProductsMapping>();
            OrderItems = new HashSet<OrderItem>();
            ProductDigitalFileMappings = new HashSet<ProductDigitalFileMapping>();
            ProductPicturesMappings = new HashSet<ProductPicturesMapping>();
            ProductProductAttributeMappings = new HashSet<ProductProductAttributeMapping>();
            ProductReviews = new HashSet<ProductReview>();
            ProductShippingMethodsMappings = new HashSet<ProductShippingMethodsMapping>();
            ProductsCategoriesMappings = new HashSet<ProductsCategoriesMapping>();
            ProductsTagsMappings = new HashSet<ProductsTagsMapping>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string FullDescription { get; set; } = null!;
        public int VendorId { get; set; }
        public int? ManufacturerId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsTaxExempt { get; set; }
        public bool? IsShippingFree { get; set; }
        public int? EstimatedShippingDays { get; set; }
        public decimal? ShippingCharges { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public bool? AllowCustomerReviews { get; set; }
        public int? ProductViewCount { get; set; }
        public int? ProductSalesCount { get; set; }
        public bool? IsReturnAble { get; set; }
        public bool? IsDigitalProduct { get; set; }
        public bool? IsDiscountAllowed { get; set; }
        public DateTime? SellStartDatetimeUtc { get; set; }
        public DateTime? SellEndDatetimeUtc { get; set; }
        public string? Sku { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? WarehouseId { get; set; }
        public int? InventoryMethodId { get; set; }
        public int? StockQuantity { get; set; }
        public bool? IsBoundToStockQuantity { get; set; }
        public bool? DisplayStockQuantity { get; set; }
        public int? OrderMinimumQuantity { get; set; }
        public int? OrderMaximumQuantity { get; set; }
        public bool? MarkAsNew { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public bool? IsActive { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual User Vendor { get; set; } = null!;
        public virtual ICollection<DiscountProductsMapping> DiscountProductsMappings { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductDigitalFileMapping> ProductDigitalFileMappings { get; set; }
        public virtual ICollection<ProductPicturesMapping> ProductPicturesMappings { get; set; }
        public virtual ICollection<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ProductShippingMethodsMapping> ProductShippingMethodsMappings { get; set; }
        public virtual ICollection<ProductsCategoriesMapping> ProductsCategoriesMappings { get; set; }
        public virtual ICollection<ProductsTagsMapping> ProductsTagsMappings { get; set; }
    }
}
