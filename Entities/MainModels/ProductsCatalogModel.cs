using Entities.CommonModels;
using Entities.CommonModels.ProductsCatalogModule;
using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace Entities.MainModels
{
    public class ProductsCatalogModel
    {
        public List<ManufacturerEntity>? ManufacturerList { get; set; }
        public List<UserEntity>? UsersList { get; set; }
        public List<CategoryEntity>? CategoryList { get; set; }
        public List<ProductReviewEntity>? ProductReviewList { get; set; }
        public List<ShippingMethodEntity>? ShippingMethodsList { get; set; }
        public List<InventoryMethodEntity>? InventoryMethodsList { get; set; }
        public List<WarehouseEntity>? WarehousesList { get; set; }
        public List<ProductAttributeEntity>? ProductAttributesList { get; set; }
        public List<ProductVariantDetail>? ProductVariantDetailList { get; set; }
        public List<DiscountEntity>? DiscountsList { get; set; }
        public List<ColorEntity>? ColorsList { get; set; }
        public List<AttachmentEntity>? AttachmentsList { get; set; }
        public ProductEntity? ProductObj { get; set; }
        public ProductReviewEntity? ProductReviewObj { get; set; }
        public ProductVariantDetail? ProductVariantDetailObj { get; set; }
        public List<ProductEntity>? ProductsList { get; set; }
        public ProductDigitalFileMappingEntity? ProductDigitalFileMappingObj { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
