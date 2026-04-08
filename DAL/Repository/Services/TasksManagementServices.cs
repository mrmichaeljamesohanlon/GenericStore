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
    public class TasksManagementServices : ITasksManagementServices
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public TasksManagementServices(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }


        public async Task<List<RequestsQueueEntity>> GetRequestQueueListDAL(RequestsQueueEntity FormData)
        {

            List<RequestsQueueEntity> result = new List<RequestsQueueEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.TaskId > 0)
                    {
                        SearchParameters.Append("AND MTBL.TaskId =  @0 ", FormData.TaskId);
                    }

                    if (FormData.RequestTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RequestTypeID =  @0 ", FormData.RequestTypeId);
                    }
                    if (FormData.RequestStatusId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RequestStatusID =  @0 ", FormData.RequestStatusId);
                    }

                    if (FormData.ReferenceId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ReferenceId =  @0 ", FormData.ReferenceId);
                    }



                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, RT.RequestTypeName , RS.StatusKey as  StatusKeyName")
                      .From(" RequestsQueue MTBL")
                      .InnerJoin("RequestType RT").On("RT.RequestTypeID = MTBL.RequestTypeID")
                      .InnerJoin("RequestStatus RS").On("RS.RequestStatusID =  MTBL.RequestStatusID")

                      .Where("MTBL.TaskID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.TaskID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RequestsQueueEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<List<RequestStatusEntity>> GetRequestStatusListDAL(RequestStatusEntity FormData)
        {

            List<RequestStatusEntity> result = new List<RequestStatusEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RequestStatusId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RequestStatusId =  @0 ", FormData.RequestStatusId);
                    }

                    if (!String.IsNullOrEmpty(FormData.StatusKey))
                    {
                        SearchParameters.Append("AND MTBL.StatusKey LIKE  @0", "%" + FormData.StatusKey + "%");
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
                      .From(" RequestStatus MTBL")
                      .Where("MTBL.RequestStatusId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.RequestStatusId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RequestStatusEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<List<RequestTypeEntity>> GetRequestTypeListDAL(RequestTypeEntity FormData)
        {

            List<RequestTypeEntity> result = new List<RequestTypeEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RequestTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RequestTypeId =  @0 ", FormData.RequestTypeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.RequestTypeName))
                    {
                        SearchParameters.Append("AND MTBL.RequestTypeName LIKE  @0", "%" + FormData.RequestTypeName + "%");
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
                      .From(" RequestType MTBL")

                      .Where("MTBL.RequestTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.RequestTypeId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<RequestTypeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<VendorsAccountRequestEntity?> GetVendorAccountCreationRequestByTaskIdDAL(int TaskID)
        {

            VendorsAccountRequestEntity? result = new VendorsAccountRequestEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {



                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 MTBL.*")
                      .From(" VendorsAccountRequest MTBL")
                      .Where("MTBL.TaskID  = @0", TaskID);

                    result = context.Fetch<VendorsAccountRequestEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<string> UpdateRequestsQueueStatusDAL(RequestsQueueEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    context.Execute(@"UPDATE TOP(1) RequestsQueue set RequestStatusID = @RequestStatusId  , ModifiedOn = GETDATE() , ModifiedBy = @LoginUserId, StatusChangeDate = GETDATE() WHERE TaskId = @TaskId;",
                      new
                      {
                          RequestStatusId = FormData.RequestStatusId,
                          TaskId = FormData.TaskId,
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


        public async Task<RequestsQueueEntity> CreateRequestQueueDAL(RequestsQueueEntity FormData, int DataOperationType)
        {
            RequestsQueueEntity? result = new RequestsQueueEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {


                    int TaskId = context.ExecuteScalar<int>(@"INSERT INTO RequestsQueue( RequestTypeID, ReferenceId, RequestStatusID, Comment,  CreatedOn, CreatedBy)  
                    VALUES(@RequestTypeId, @ReferenceId,@RequestStatusId, @Comment,  GETDATE(), @LoginUserId);   
                       SELECT SCOPE_IDENTITY()",
                       new
                       {
                           RequestTypeId = FormData.RequestTypeId,
                           ReferenceId = FormData.ReferenceId,
                           RequestStatusId = FormData.RequestStatusId,
                           Comment = FormData.Comment,
                           LoginUserId = FormData.LoginUserId,

                       });


                    if (TaskId > 0)
                    {
                        RequestsQueueEntity requestsQueueEntity = new RequestsQueueEntity
                        {
                            TaskId = TaskId
                        };
                        result = await GetRequestQueueEntityByIdDAL(requestsQueueEntity);
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


        public async Task<RequestsQueueEntity?> GetRequestQueueEntityByIdDAL(RequestsQueueEntity FormData)
        {

            RequestsQueueEntity? result = null;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var ppSql = PetaPoco.Sql.Builder.Select(@"MTBL.*, RT.RequestTypeName , RS.StatusKey as  StatusKeyName")
                      .From(" RequestsQueue MTBL")
                      .InnerJoin("RequestType RT").On("RT.RequestTypeID = MTBL.RequestTypeID")
                      .InnerJoin("RequestStatus RS").On("RS.RequestStatusID =  MTBL.RequestStatusID")

                      .Where("MTBL.TaskID = @0", FormData.TaskId);

                    result = context.Fetch<RequestsQueueEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<string> SaveOrderRefundRequestDataDAL(OrderRefundRequestEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    context.Execute(@"insert into OrderRefundRequest(	OrderID	,TaskId ,RefundReasonDesc,	RefundReasonTypeID,CurrencyId , IsFullRefund,	RefundAmount,	CreatedOn,	CreatedBy)
                    values(@OrderId, @TaskId	, @RefundReasonDesc,	@RefundReasonTypeId, @CurrencyId, @IsFullRefund ,	@RefundAmount,	GETDATE(),	@LoginUserId);",
                      new
                      {
                          OrderId = FormData.OrderId,
                          TaskId = FormData.TaskId,
                          RefundReasonDesc = FormData.RefundReasonDesc ?? "",
                          RefundReasonTypeId = FormData.RefundReasonTypeId,
                          CurrencyId = FormData.CurrencyId,
                          IsFullRefund = FormData.IsFullRefund,
                          RefundAmount = FormData.RefundAmount,
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

        public async Task<OrderRefundRequestEntity?> GetOrderRefundRequestByTaskIdDAL(int TaskID)
        {

            OrderRefundRequestEntity? result = new OrderRefundRequestEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {



                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 MTBL.*, ORRT.ReasonName ")
                      .From(" OrderRefundRequest MTBL")
                      .InnerJoin("RequestsQueue RQ").On("RQ.TaskID =  MTBL.TaskID")
                      .InnerJoin("OrderRefundReasonType ORRT").On("ORRT.RefundReasonTypeID =  MTBL.RefundReasonTypeID")
                      .Where("MTBL.TaskID  = @0", TaskID);

                    result = context.Fetch<OrderRefundRequestEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<string> UpdateOrderDataAndStatusAfterSuccessfullRefundDAL(int OrderId, int? LoginUserId)
        {
            string result = "";

            using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
            {
                try
                {
                    dbConnection.Open();
                    dbConnection.Execute(@"DECLARE @OrderStatusMappingID INT;
                    DECLARE @LatestStatusID INT = (select top 1 StatusID from OrderStatuses where StatusName = 'Refunded');
                    INSERT INTO OrderStatusesMapping(OrderID,	StatusID,	IsActive,	CreatedOn,	CreatedBy)
                    VALUES(@OrderId,	@LatestStatusID,	1,	GETDATE(),	@LoginUserId);
                    SET @OrderStatusMappingID = SCOPE_IDENTITY();

                    UPDATE TOP(40) OrderStatusesMapping SET IsActive = 0, ModifiedOn = GETDATE() , ModifiedBy = @LoginUserId
                    WHERE OrderID = @OrderId AND OrderStatusMappingID != @OrderStatusMappingID;

                    UPDATE TOP(1) Orders SET LatestStatusID = @LatestStatusID WHERE OrderID =@OrderId;

                    UPDATE TOP(50) OrderShippingDetail SET ShippingStatusID = @LatestStatusID WHERE OrderID = @OrderId 
                    AND (ShippingStatusID is  null OR ShippingStatusID ! = (SELECT TOP 1 StatusID FROM OrderStatuses OS WHERE StatusName = 'Completed'));",
                      new
                      {
                          OrderId = OrderId,
                          LoginUserId = LoginUserId,
                         
                      }, commandType: CommandType.Text);
                    dbConnection.Close();

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
