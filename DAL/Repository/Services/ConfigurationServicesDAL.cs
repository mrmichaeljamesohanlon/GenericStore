using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.CommonModels;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;


namespace DAL.Repository.Services
{
    public class ConfigurationServicesDAL : IConfigurationServicesDAL
    {

        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;


        //--Constructor of the class
        public ConfigurationServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }



        public async Task<List<EntityEntity>> GetEntitiesListDAL(EntityEntity FormData)
        {

            List<EntityEntity> result = new List<EntityEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.EntityId > 0)
                    {
                        SearchParameters.Append("AND MTBL.EntityId =  @0 ", FormData.EntityId);
                    }

                    if (FormData.EntityTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.EntityTypeId =  @0 ", FormData.EntityTypeId);
                    }
                    if (FormData.AppModuleId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AppModuleId =  @0 ", FormData.AppModuleId);
                    }

                    if (!String.IsNullOrEmpty(FormData.EntityName))
                    {
                        SearchParameters.Append("AND MTBL.EntityName LIKE  @0", "%" + FormData.EntityName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , ETYP.EntityTypeName")
                      .From(" Entities MTBL")
                      .LeftJoin("EntityTypes ETYP").On("MTBL.EntityTypeID=ETYP.EntityTypeID")
                      .Where("MTBL.EntityId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.EntityName ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<EntityEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }

        public async Task<EntityEntity?> GetEntityDetailByIdDAL(int EntityId)
        {

            EntityEntity? result = new EntityEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {


                    var ppSql = PetaPoco.Sql.Builder.Select(@"MTBL.* , ETYP.EntityTypeName")
                      .From(" Entities MTBL")
                      .LeftJoin("EntityTypes ETYP").On("MTBL.EntityTypeID=ETYP.EntityTypeID")
                      .Where("MTBL.EntityId = @0", EntityId);

                    result = context.Fetch<EntityEntity>(ppSql)?.FirstOrDefault();

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }


        public async Task<List<RightEntity>> GetRightsListDAL(RightEntity FormData)
        {

            List<RightEntity> result = new List<RightEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RightId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RightId =  @0 ", FormData.RightId);
                    }

                    if (!String.IsNullOrEmpty(FormData.RightName))
                    {
                        SearchParameters.Append("AND MTBL.RightName LIKE  @0", "%" + FormData.RightName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Rights MTBL")
                      .Where("MTBL.RightId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.RightId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RightEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }

        public async Task<List<RolesEntity>> GetRolesListDAL(RolesEntity FormData)
        {

            List<RolesEntity> result = new List<RolesEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RoleId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RoleId =  @0 ", FormData.RoleId);
                    }

                    if (!String.IsNullOrEmpty(FormData.RoleName))
                    {
                        SearchParameters.Append("AND MTBL.RoleName LIKE  @0", "%" + FormData.RoleName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Roles MTBL")
                      .Where("MTBL.RoleId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.RoleId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RolesEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }

        public async Task<List<RoleRightEntity>> GetRolesRightsListDAL(RoleRightEntity FormData)
        {

            List<RoleRightEntity> result = new List<RoleRightEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RoleId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RoleId =  @0 ", FormData.RoleId);
                    }

                    if (FormData.RoleRightId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RoleRightId =  @0 ", FormData.RoleRightId);
                    }

                    if (FormData.EntityId > 0)
                    {
                        SearchParameters.Append("AND MTBL.EntityId =  @0 ", FormData.EntityId);
                    }

                  

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" RoleRights MTBL")
                      .Where("MTBL.RoleRightId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.RoleRightId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RoleRightEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }

        public async Task<string> SaveUpdateRoleRightsDAL(int RoleId, string recordValueJson, int UserID)
        {
            string result = "";


            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute("SP_AdmPanel_SaveUpdateRoleRights",
                        new
                        {
                            RoleId = RoleId,
                            recordValueJson = recordValueJson,
                            UserID = UserID
                        }
                        , commandType: CommandType.StoredProcedure);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;

                }




            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<string> UpdateSiteLogoDAL(AppConfigEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    context.Execute(@"IF(@AppConfigValue IS NOT NULL AND @AppConfigValue!= '')
                                    BEGIN
	                                    UPDATE TOP(1) AppConfigs SET AppConfigValue = @AppConfigValue , ModifiedBy = @LoginUserId , ModifiedOn =GETDATE() WHERE AppConfigID = @AppConfigId
                                    END",
                                         new
                                         {
                                             AppConfigId = FormData.AppConfigId,
                                             AppConfigValue = FormData.AppConfigValue,
                                             LoginUserId = FormData.LoginUserId,

                                         });
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

        public async Task<List<LanguageEntity>> GetLanguagesListDAL(LanguageEntity FormData)
        {

            List<LanguageEntity> result = new List<LanguageEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.LanguageId > 0)
                    {
                        SearchParameters.Append("AND MTBL.LanguageId =  @0 ", FormData.LanguageId);
                    }

                    if (!String.IsNullOrEmpty(FormData.LanguageName))
                    {
                        SearchParameters.Append("AND MTBL.LanguageName LIKE  @0", "%" + FormData.LanguageName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.LanguageCode))
                    {
                        SearchParameters.Append("AND MTBL.LanguageCode LIKE  @0", "%" + FormData.LanguageCode + "%");
                    }



                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Languages MTBL")
                      .Where("MTBL.LanguageId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.LanguageId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<LanguageEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }





        }

		public async Task<List<AppModuleEntity>> GetAppModulesListDAL(AppModuleEntity FormData)
		{

			List<AppModuleEntity> result = new List<AppModuleEntity>();

			using (var context = _contextHelper.GetDataContextHelper())
			{
				try
				{

					var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



					if (FormData.AppModuleId > 0)
					{
						SearchParameters.Append("AND MTBL.AppModuleId =  @0 ", FormData.AppModuleId);
					}

					if (!String.IsNullOrEmpty(FormData.AppModuleName))
					{
						SearchParameters.Append("AND MTBL.AppModuleName LIKE  @0", "%" + FormData.AppModuleName + "%");
					}
					

					var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
					  .From(" AppModules MTBL")
					  .Where("MTBL.AppModuleId is not null")
					  .Append(SearchParameters)
					 .OrderBy("MTBL.AppModuleId ASC")
					.Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

					result = context.Fetch<AppModuleEntity>(ppSql);

					await Task.FromResult(result);
					return result;


				}
				catch (Exception)
				{

					throw;
				}

			}





		}

		public async Task<string> SaveScreenLocalizationLabelDAL(ScrnsLocalizationEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    if (FormData.ScrnLocalizationId > 0)
                    {
                        context.Execute(@"UPDATE ScrnsLocalization
                        SET ScreenID = @ScreenId, LanguageID = @LanguageId, AppModuleID = @AppModuleId, LabelsJsonData = @LabelsJsonData, IsActive=1, ModifiedOn=GETDATE(), ModifiedBy=@LoginUserId where ScrnLocalizationId = @ScrnLocalizationId",
                                                               new
                                                               {
                                                                   ScrnLocalizationId = FormData.ScrnLocalizationId,
                                                                   ScreenId = FormData.ScreenId,
                                                                   LanguageId = FormData.LanguageId,
                                                                   AppModuleId = FormData.AppModuleId,
                                                                   LabelsJsonData = FormData.LabelsJsonData,
                                                                   LoginUserId = FormData.LoginUserId,

                                                               });
                    }
                    else
                    {
                        context.Execute(@"insert into ScrnsLocalization(ScreenID ,LanguageID, AppModuleID, LabelsJsonData, IsActive, CreatedOn, CreatedBy)
                         values(@ScreenId ,@LanguageId, @AppModuleId, @LabelsJsonData, 1, GETDATE(), @LoginUserId);",
                                       new
                                       {
                                           ScrnLocalizationId = FormData.ScrnLocalizationId,
                                           ScreenId = FormData.ScreenId,
                                           LanguageId = FormData.LanguageId,
                                           AppModuleId = FormData.AppModuleId,
                                           LabelsJsonData = FormData.LabelsJsonData,
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

        public async Task<List<MenuNavigationEntity>> GetNavMenusListForLocalizationDAL(MenuNavigationEntity FormData)
        {

            List<MenuNavigationEntity> result = new List<MenuNavigationEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.MenuNavigationId > 0)
                    {
                        SearchParameters.Append("AND MTBL.MenuNavigationId =  @0 ", FormData.MenuNavigationId);
                    }


                    if (!String.IsNullOrEmpty(FormData.MenuNavigationName))
                    {
                        SearchParameters.Append("AND MTBL.MenuNavigationName LIKE  @0", "%" + FormData.MenuNavigationName + "%");
                    }



                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, PRNT.MenuNavigationName AS ParentMenuNavigationName")
                      .From(" MenuNavigations MTBL")
                      .LeftJoin("MenuNavigations PRNT").On("PRNT.MenuNavigationID=  MTBL.ParentMenuNavigationID")
                      .Where("MTBL.MenuNavigationId is not null AND MTBL.IsActive = 1")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.MenuNavigationID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<MenuNavigationEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                   

                }
                catch (Exception)
                {

                    throw;
                }

            }

        }



        public async Task<string> SaveMenuLocalizationLabelDAL(MenuNavigationEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {


                    context.Execute(@"UPDATE MenuNavigations
                        SET LocalizationJsonData = @LocalizationJsonData, ModifiedOn=GETDATE(), ModifiedBy=@LoginUserId where MenuNavigationId = @MenuNavigationId",
                                                             new
                                                             {
                                                                 MenuNavigationId = FormData.MenuNavigationId,
                                                                 LocalizationJsonData = FormData.LocalizationJsonData,
                                                                 LoginUserId = FormData.LoginUserId,

                                                             });


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
