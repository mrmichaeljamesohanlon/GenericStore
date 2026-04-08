using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface IDiscountsServicesDAL
    {
        Task<List<DiscountEntity>> GetDiscountsListDAL(DiscountEntity FormData);
         Task<List<DiscountTypeEntity>> GetDiscountTypesListDAL(DiscountTypeEntity FormData);
        Task<List<ProductEntity>> GetProductsListForDiscountDAL(ProductEntity FormData);
        Task<List<CategoryEntity>> GetCategoriesListForDiscountDAL(CategoryEntity FormData);
         Task<string> InsertUpdateDiscountDAL(DiscountEntity FormData);
        Task<DiscountEntity?> GetDiscountDetailsById(int DiscountId);
        Task<List<DiscountProductsMappingEntity>> GetDiscountProductsMappingListDAL(DiscountProductsMappingEntity FormData);
        Task<List<DiscountCategoriesMappingEntity>> GetDiscountCategoriesMappingListDAL(DiscountCategoriesMappingEntity FormData);
        Task<List<ContactUsEntity>> GetContactUsListDAL(ContactUsEntity FormData);
        Task<List<SubscriberEntity>> GetSubscribersListDAL(SubscriberEntity FormData);
        Task<List<HomeScreenBannerEntity>> GetHomeScreenBannersListDAL(HomeScreenBannerEntity FormData);
        Task<string> SaveUpdateHomeScreenBannerDAL(HomeScreenBannerEntity FormData, int DataOperationType);
        Task<List<DiscountsCampaignEntity>> GetDiscountsCampaignDAL(DiscountsCampaignEntity FormData);
        Task<DiscountsCampaignEntity?> GetCampaignDetailByIdDAL(int CampaignId);
        Task<string> SaveUpdateDiscountsCampaignDAL(DiscountsCampaignEntity FormData, int DataOperationType);

    }
}
