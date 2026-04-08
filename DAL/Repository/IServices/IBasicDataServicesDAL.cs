using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface IBasicDataServicesDAL
    {
        UserEntity? GetUserDataByUserID(int UserId);
        Task<List<ColorEntity>> GetColorsListDAL(ColorEntity FormData);
        Task<string> SaveUpdateColorDAL(ColorEntity FormData, int DataOperationType);
        Task<List<CategoryEntity>> GetCategoriesListDAL(CategoryEntity FormData);
        Task<string> SaveUpdateCategoryDAL(CategoryEntity FormData, int DataOperationType);
        Task<List<CategoryEntity>> GetParentCategoriesListDAL();
        Task<List<SizeEntity>> GetSizeListDAL(SizeEntity FormData);
        Task<string> SaveUpdateSizeDAL(SizeEntity FormData, int DataOperationType);
        Task<List<ManufacturerEntity>> GetManufacturerListDAL(ManufacturerEntity FormData);
        Task<string> SaveUpdateManufacturerDAL(ManufacturerEntity FormData, int DataOperationType);
        Task<List<CurrencyEntity>> GetCurrenciesListDAL(CurrencyEntity FormData);
        Task<string> SaveUpdateCurrencyDAL(CurrencyEntity FormData, int DataOperationType);
        Task<List<AttachmentTypeEntity>> GetAttachmentTypesListDAL(AttachmentTypeEntity FormData);
        Task<List<PaymentMethodEntity>> GetPaymentMethodsListDAL(PaymentMethodEntity FormData);
        Task<string> SaveUpdatePaymentMethodDAL(PaymentMethodEntity FormData, int DataOperationType);
        Task<List<TagEntity>> GetTagsListDAL(TagEntity FormData);
        Task<string> SaveUpdateTagDAL(TagEntity FormData, int DataOperationType);
        List<MenuNavigation> GetNavMenusList(MenuNavigation FormData);
        List<RoleRightEntity>? GetUserRoleRightsForSession(int UserID);
    }
}
