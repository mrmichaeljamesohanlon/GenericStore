using DAL.DBContext;
using DAL.Repository.IServices;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class CommonServicesDAL : ICommonServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;

        //--Constructor of the class
        public CommonServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
        }

        public virtual async Task<string> LogRunTimeExceptionDAL(string ExceptionMessage, string? StackTrace, string? Source)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;

                    var QueryResponse = context.Execute(@";EXEC [dbo].[SP_LogRunTimeException] @ExceptionMessage,@StackTrace , @Source",
                        new { ExceptionMessage = ExceptionMessage, StackTrace = StackTrace, Source = Source });

                    result = "Saved Successfully";
                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public async Task<bool> DeleteAnyRecordDAL(string primarykeyValue, string primaryKeyColumn, string tableName, int SqlDeleteType = 1)
        {
            bool result = false;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    if (SqlDeleteType == 1)
                    {
                        string deleteQuery = String.Format("DELETE TOP(1) FROM {0} WHERE {1}='{2}'", tableName, primaryKeyColumn, primarykeyValue);
                        context.Execute(deleteQuery);
                        result = true;
                    }
                    else
                    {
                        context.EnableAutoSelect = false;
                        context.Execute(@";EXEC [dbo].[SP_AdmPanel_DeleteAnyRecord] @tableName, @primaryKeyColumn, @primarykeyValue",
                            new
                            {
                                tableName = tableName,
                                primaryKeyColumn = primaryKeyColumn,
                                primarykeyValue = primarykeyValue,
                            });

                        result = true;
                    }


                    await Task.FromResult(result);
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }


        public async Task<UserEntity?> GetUserLogin(string EmailAddress, string Password)
        {
            UserEntity? result = new UserEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    context.EnableAutoSelect = false;
                    result = context.Fetch<UserEntity>(@";EXEC [dbo].[SP_GetUserLogin] @EmailAddress , @Password",
                        new { EmailAddress = EmailAddress, Password = Password }).FirstOrDefault();


                    await Task.FromResult(result);
                    return result;

                }
                catch (Exception)
                {
                    throw;
                }


            }


        }

        public async Task<int> SaveUpdateAttachmentDAL(AttachmentEntity FormData)
        {
            int result = 0;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {



                    if (FormData.AttachmentId < 1)
                    {
                        result = context.ExecuteScalar<int>(@"INSERT INTO Attachments(AttachmentName, AttachmentTypeID, AttachmentURL, IsByteArray,IsActive, IsCommonImageUpload , CreatedOn, CreatedBy)
                        VALUES(@AttachmentName, 1, @AttachmentUrl, 0,1,  @IsCommonImageUpload  , GETDATE(), @UserId)
                        SELECT @@@IDENTITY AS AttachmentId;",
                        new
                        {
                            AttachmentName = FormData.AttachmentName,
                            AttachmentUrl = FormData.AttachmentUrl,
                            IsCommonImageUpload = FormData.IsCommonImageUpload ?? false,
                            IsActive = FormData.IsActive,
                            UserId = FormData.UserId,

                        });

                    }
                    else if (FormData.AttachmentId > 0)
                    {
                        context.Execute(@"UPDATE top(1) Attachments SET
                        AttachmentName=@AttachmentName, AttachmentTypeID=1, AttachmentURL=@AttachmentUrl, IsActive=1 where AttachmentID=@AttachmentId",
                       new
                       {
                           AttachmentId = FormData.AttachmentId,
                           AttachmentName = FormData.AttachmentName,
                           AttachmentUrl = FormData.AttachmentUrl,
                           IsActive = FormData.IsActive,
                           UserId = FormData.UserId,

                       });
                        result = FormData.AttachmentId;
                    }



                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public async Task<List<AppConfigEntity>> GetAppConfigsValuesAsyncDAL(AppConfigEntity FormData)
        {
            List<AppConfigEntity> result = new List<AppConfigEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AppConfigId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AppConfigId =  @0 ", FormData.AppConfigId);
                    }
                    if (!String.IsNullOrWhiteSpace(FormData.AppConfigKey))
                    {
                        SearchParameters.Append("AND MTBL.AppConfigKey in (select value from string_split(@0, ','))", FormData.AppConfigKey);//--comma seperated also accepted
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" AppConfigs MTBL")
                      .Where("MTBL.AppConfigId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AppConfigId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AppConfigEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;

                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public List<AppConfigEntity> GetAppConfigsValuesDAL(AppConfigEntity FormData)
        {
            List<AppConfigEntity> result = new List<AppConfigEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AppConfigId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AppConfigId =  @0 ", FormData.AppConfigId);
                    }
                    if (!String.IsNullOrWhiteSpace(FormData.AppConfigKey))
                    {
                        SearchParameters.Append("AND MTBL.AppConfigKey in (select value from string_split(@0, ','))", FormData.AppConfigKey);//--comma seperated also accepted
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" AppConfigs MTBL")
                      .Where("MTBL.AppConfigId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AppConfigId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AppConfigEntity>(ppSql);

                    return result;

                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<ScrnsLocalizationEntity?> GetScreenLocalizationJsonDataDAL(ScrnsLocalizationEntity FormData)
        {
            ScrnsLocalizationEntity? result = new ScrnsLocalizationEntity();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AppModuleId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AppModuleId =  @0 ", FormData.AppModuleId);
                    }
                    if (FormData.ScreenId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ScreenId =  @0 ", FormData.ScreenId);
                    }
                    if (FormData.LanguageId > 0)
                    {
                        SearchParameters.Append("AND MTBL.LanguageId =  @0 ", FormData.LanguageId);
                    }
                    if (FormData.ScrnLocalizationId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ScrnLocalizationId =  @0 ", FormData.ScrnLocalizationId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" MTBL.*, ENT.EntityName AS ScreenName, LNG.LanguageName")
                      .From(" ScrnsLocalization MTBL")
                      .InnerJoin("Entities AS ENT").On("ENT.EntityID=MTBL.ScreenID")
                      .InnerJoin("Languages LNG").On("LNG.LanguageID = MTBL.LanguageID")
                      .Where("MTBL.IsActive = 1")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ScrnLocalizationID DESC");

                    result = context.Fetch<ScrnsLocalizationEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;

                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<List<ScrnsLocalizationEntity>> TestTestDAL(ScrnsLocalizationEntity FormData)
        {
            List<ScrnsLocalizationEntity> result = new List<ScrnsLocalizationEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AppModuleId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AppModuleId =  @0 ", FormData.AppModuleId);
                    }
                    if (FormData.ScreenId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ScreenId =  @0 ", FormData.ScreenId);
                    }
                    if (FormData.LanguageId > 0)
                    {
                        SearchParameters.Append("AND MTBL.LanguageId =  @0 ", FormData.LanguageId);
                    }
                    if (FormData.ScrnLocalizationId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ScrnLocalizationId =  @0 ", FormData.ScrnLocalizationId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" MTBL.*, ENT.EntityName AS ScreenName, LNG.LanguageName")
                      .From(" ScrnsLocalization MTBL")
                      .InnerJoin("Entities AS ENT").On("ENT.EntityID=MTBL.ScreenID")
                      .InnerJoin("Languages LNG").On("LNG.LanguageID = MTBL.LanguageID")
                      .Where("MTBL.IsActive = 1")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ScrnLocalizationID DESC");

                    result = context.Fetch<ScrnsLocalizationEntity>(ppSql).ToList();

                    await Task.FromResult(result);
                    return result;

                }
            }
            catch (Exception)
            {

                throw;
            }



        }


        public async Task<List<LocalizationCommonJsonEntity>> GetCommonLocalizationJsonDataListDAL(LocalizationCommonJsonEntity FormData)
        {

            List<LocalizationCommonJsonEntity> result = new List<LocalizationCommonJsonEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.LocalCommonDataId > 0)
                    {
                        SearchParameters.Append("AND MTBL.LocalCommonDataId =  @0 ", FormData.LocalCommonDataId);
                    }
                    if (FormData.LocalizationTableId > 0)
                    {
                        SearchParameters.Append("AND MTBL.LocalizationTableId =  @0 ", FormData.LocalizationTableId);
                    }
                    if (FormData.PrimaryKeyId > 0)
                    {
                        SearchParameters.Append("AND MTBL.PrimaryKeyId =  @0 ", FormData.PrimaryKeyId);
                    }



                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , LTAB.TableName")
                      .From(" LocalizationCommonJson MTBL")
                    .InnerJoin("LocalizationTables LTAB").On("LTAB.LocalizationTableID = MTBL.LocalizationTableID")
                      .Where("MTBL.LocalCommonDataId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.LocalCommonDataID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<LocalizationCommonJsonEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<string> SaveDynamicLocalizationLabelDAL(LocalizationCommonJsonEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    if (FormData.LocalCommonDataId > 0)
                    {
                        context.Execute(@"UPDATE LocalizationCommonJson SET LocalizationJsonData = @LocalizationJsonData where LocalCommonDataId = @LocalCommonDataId",
                                                                 new
                                                                 {
                                                                     LocalCommonDataId = FormData.LocalCommonDataId,
                                                                     LocalizationJsonData = FormData.LocalizationJsonData,
                                                                     LoginUserId = FormData.LoginUserId,

                                                                 });

                    }
                    else
                    {

                        context.Execute(@"INSERT INTO LocalizationCommonJson(LocalizationTableID,	PrimaryKeyID,	LocalizationJsonData)VALUES(@LocalizationTableId ,@PrimaryKeyId, @LocalizationJsonData)",
                                                                 new
                                                                 {
                                                                     LocalizationTableId = FormData.LocalizationTableId,
                                                                     LocalizationJsonData = FormData.LocalizationJsonData,
                                                                     PrimaryKeyId = FormData.PrimaryKeyId,
                                                                     LoginUserId = FormData.LoginUserId,

                                                                 });
                    }



                    result = "Saved Successfully!";


                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


    }
}
