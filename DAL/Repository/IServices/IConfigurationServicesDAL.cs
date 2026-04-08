using System;
using Entities.CommonModels;
using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;
namespace DAL.Repository.IServices
{
    public interface IConfigurationServicesDAL
    {
        Task<List<EntityEntity>> GetEntitiesListDAL(EntityEntity FormData);
        Task<List<RightEntity>> GetRightsListDAL(RightEntity FormData);
        Task<List<RolesEntity>> GetRolesListDAL(RolesEntity FormData);
        Task<List<RoleRightEntity>> GetRolesRightsListDAL(RoleRightEntity FormData);
        Task<string> SaveUpdateRoleRightsDAL(int RoleId, string recordValueJson, int UserID);
        Task<string> UpdateSiteLogoDAL(AppConfigEntity FormData, int DataOperationType);
        Task<List<LanguageEntity>> GetLanguagesListDAL(LanguageEntity FormData);
        Task<string> SaveScreenLocalizationLabelDAL(ScrnsLocalizationEntity FormData);
        Task<List<MenuNavigationEntity>> GetNavMenusListForLocalizationDAL(MenuNavigationEntity FormData);
        Task<string> SaveMenuLocalizationLabelDAL(MenuNavigationEntity FormData);
        Task<List<AppModuleEntity>> GetAppModulesListDAL(AppModuleEntity FormData);
        Task<EntityEntity?> GetEntityDetailByIdDAL(int EntityId);

    }
}
