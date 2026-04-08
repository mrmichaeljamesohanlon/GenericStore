using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface ITasksManagementServices
    {
        Task<List<RequestsQueueEntity>> GetRequestQueueListDAL(RequestsQueueEntity FormData);
        Task<List<RequestStatusEntity>> GetRequestStatusListDAL(RequestStatusEntity FormData);
        Task<List<RequestTypeEntity>> GetRequestTypeListDAL(RequestTypeEntity FormData);
        Task<VendorsAccountRequestEntity?> GetVendorAccountCreationRequestByTaskIdDAL(int TaskID);
        Task<string> UpdateRequestsQueueStatusDAL(RequestsQueueEntity FormData);
        Task<RequestsQueueEntity>? GetRequestQueueEntityByIdDAL(RequestsQueueEntity FormData);
        Task<RequestsQueueEntity> CreateRequestQueueDAL(RequestsQueueEntity FormData, int DataOperationType);
        Task<string> SaveOrderRefundRequestDataDAL(OrderRefundRequestEntity FormData);
        Task<OrderRefundRequestEntity?> GetOrderRefundRequestByTaskIdDAL(int TaskID);
        Task<string> UpdateOrderDataAndStatusAfterSuccessfullRefundDAL(int OrderId, int? LoginUserId);
    }
}
