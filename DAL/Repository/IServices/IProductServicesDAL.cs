using Entities.CommonModels;
using Entities.CommonModels.ProductsCatalogModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace DAL.Repository.IServices
{
    public interface IProductServicesDAL
    {
      Task<string> CreateNewProductDAL(ProductEntity FormData);
        Task<List<ShippingMethodEntity>> GetShippingMethodsListDAL(ShippingMethodEntity FormData);
        Task<List<InventoryMethodEntity>> GetInventoryMethodsListDAL(InventoryMethodEntity FormData);
        Task<List<WarehouseEntity>> GetWarehousesListDAL(WarehouseEntity FormData);
        Task<List<ProductAttributeEntity>> GetProductAttributesListDAL(ProductAttributeEntity FormData);
        Task<List<TagEntity>> GetProductTagsListForCreateProductPageDAL(TagEntity FormData);
        Task<List<HtmlDropDownRemoteData>> GetProductAttributeValuesByAttributeID(string ProductAttributeId);
        Task<ProductEntity?> GetProductDetailsById(int ProductId);
        Task<string> UpdateProductDAL(ProductEntity FormData);
        Task<List<ProductEntity>> GetProductList(ProductEntity FormData);
        Task<List<ProductEntity>> GetProductsReviewsDAL(ProductEntity FormData);
        Task<List<ProductReviewEntity>> GetProductReviewsByProductIdDAL(ProductReviewEntity FormData);
        Task<string> UploadBulkProductsFromExcelDAL(ProductExcelUpload FormData);
        Task<List<AttachmentEntity>> GetAttachmentsListForImageUploadPageDAL(AttachmentEntity FormData);
        Task<List<ProductAttributeEntity>> GetProductVariantsDAL(ProductAttributeEntity FormData);
        Task<List<ProductVariantDetail>> GetProductVariantsDetailByIdDAL(int ProductAttributeId);
        Task<string> SaveUpdateProductVariantDAL(ProductVariantDetail FormData, int DataOperationType);
        Task<List<ProductsCategoriesMapping>> ReadProductCategoriesById(int ProductId);
        Task<bool> DeleteAnyProductImage(int AttachmentID);
        Task<string> UpdateProductImgColorMappingDAL(ProductPicturesMappingEntity FormData);
        Task<ProductDigitalFileMappingEntity?> GetProductDigitalFileInfoByIdDAL(int ProductId);
        Task<bool> CheckIfProductDigitalDAL(int ProductId);
       
    }
}
