using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface ICommonServicesDAL
    {
        Task<string> LogRunTimeExceptionDAL(string ExceptionMessage, string? StackTrace, string? Source);
        Task<bool> DeleteAnyRecordDAL(string primarykeyValue, string primaryKeyColumn, string tableName, int SqlDeleteType = 1);
        Task<UserEntity?> GetUserLogin(string EmailAddress, string Password);
        Task<int> SaveUpdateAttachmentDAL(AttachmentEntity FormData);
        Task<List<AppConfigEntity>> GetAppConfigsValuesAsyncDAL(AppConfigEntity FormData);
        List<AppConfigEntity> GetAppConfigsValuesDAL(AppConfigEntity FormData);
        Task<ScrnsLocalizationEntity?> GetScreenLocalizationJsonDataDAL(ScrnsLocalizationEntity FormData);
        Task<List<ScrnsLocalizationEntity>> TestTestDAL(ScrnsLocalizationEntity FormData);
        Task<List<LocalizationCommonJsonEntity>> GetCommonLocalizationJsonDataListDAL(LocalizationCommonJsonEntity FormData);
        Task<string> SaveDynamicLocalizationLabelDAL(LocalizationCommonJsonEntity FormData);
    }
}
