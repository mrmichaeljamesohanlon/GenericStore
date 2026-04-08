using Entities.CommonModels.SalesModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface ISalesServicesDAL
    {
        Task<List<OrderStatusesEntity>> GetOrderStatusesList(OrderStatusesEntity FormData);
        Task<List<OrderEntity>> GetOrdersListDAL(OrderEntity FormData);
        Task<OrderEntity?> GetOrderDetailByIdDAL(OrderEntity FormData);
        Task<string> UpdateOrderShippingItemsDetailDAL(OrderShippingDetailEntity FormData);
        Task<List<OrderNoteEntity>> GetOrderNotesListDAL(OrderNoteEntity FormData);
        Task<string> SaveOrderNoteReplyDAL(OrderNoteEntity FormData);
        Task<string> UpdateOrderStatusDAL(int OrderId, int LatestStatusId, int UserId);
         Task<List<OrderVariantDetail>> GetOrderVariantsDetailByIdDAL(int OrderId);
        Task<List<OrderRefundReasonType>> GetOrderRefundReasonTypeListDAL(OrderRefundReasonType FormData);
        Task<DigitalOrderInfoEntity?> GetDigitalOrderInfoForCustomerByIdDAL(int order_item_id, int user_id);

    }
}
