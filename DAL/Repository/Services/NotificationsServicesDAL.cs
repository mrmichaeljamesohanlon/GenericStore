using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class NotificationsServicesDAL : INotificationsServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;


        //--Constructor of the class
        public NotificationsServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }


        public async Task<List<AdminPanelNotificationEntity>> GetAdminPanelNotificationsListDAL(AdminPanelNotificationEntity FormData)
        {

            List<AdminPanelNotificationEntity> result = new List<AdminPanelNotificationEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.NotificationId > 0)
                    {
                        SearchParameters.Append("AND MTBL.NotificationId =  @0 ", FormData.NotificationId);
                    }


                    if (FormData.IsReadNullProperty!=null)
                    {
                        SearchParameters.Append("AND MTBL.IsRead =  @0 ", FormData.IsReadNullProperty);
                    }


                    if (!String.IsNullOrEmpty(FormData.Title))
                    {
                        SearchParameters.Append("AND MTBL.Title LIKE  @0", "%" + FormData.Title + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , NT.NotificationTypeName , USR.FirstName as ReadByFirstName")
                      .From(" AdminPanelNotifications MTBL")
                      .InnerJoin("NotificationTypes NT").On("NT.NotificationTypeID = MTBL.NotificationTypeID")
                      .LeftJoin("Users USR").On("USR.UserId = MTBL.ReadBy")
                      .Where("MTBL.NotificationID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.NotificationID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AdminPanelNotificationEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<string> MarkAllAdminNotificationsAsReadDAL(int UserID)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    context.Execute(@"update AdminPanelNotifications set IsRead = 1 , ReadBy = @UserID", new {UserID = UserID});
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


        public async Task<string> MarkOnlySelectedAdminNotificationsAsReadDAL(AdminPanelNotificationEntity FormData)
        {
            string result = "";


            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@"UPDATE AdminPanelNotifications  SET IsRead = 1  , ReadBy = @UserId
                    WHERE AdminPanelNotifications.NotificationID in ( 
                    SELECT NotificationId FROM OPENJSON(@SelectedNotificationsIdsForReadJson) 
                    WITH ( 
                    NotificationId	INT '$.NotificationId' 
                    )
                    )",
                        new
                        {
                            SelectedNotificationsIdsForReadJson = FormData.SelectedNotificationsIdsForReadJson,
                            UserId = FormData.UserId
                        }
                        , commandType: CommandType.Text);
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


        public async Task<int> GetHeaderAdminUnreadNotificationsCountDAL()
        {

            int result = 0;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {



                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) AS HeaderUnreadNotificationCount")
                      .From(" AdminPanelNotifications MTBL")
                      .Where("MTBL.IsRead = 0");
                   
                    var TotalCount = context.Fetch<AdminPanelNotificationEntity>(ppSql)?.FirstOrDefault()?.HeaderUnreadNotificationCount;
                    result = TotalCount ??  0 ;
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
