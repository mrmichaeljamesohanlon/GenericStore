using Entities.CommonModels;
using Entities.CommonModels.SalesModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace Entities.MainModels
{
    public class SalesModel
    {
        public List<ProductEntity>? ProductsList { get; set; }
        public List<OrderEntity>? OrdersList { get; set; }
        public List<OrderStatusesEntity>? OrderStatusesList { get; set; }
        public List<OrderRefundReasonType>? OrderRefundReasonTypesList { get; set; }
        public List<UserEntity>? VendorsList { get; set; }
        public List<UserEntity>? ShippersList { get; set; }
        public List<ShippingMethodEntity>? ShippingMethodsList { get; set; }
        public List<OrderNoteEntity>? OrderNotesList { get; set; }
        public List<OrderShippingDetailEntity>? OrderShippingDetailList { get; set; }
        public List<OrderItemEntity>? OrderItemsList { get; set; }
        public List<OrdersPaymentEntity>? OrderPaymentsList { get; set; }
        public List<OrderVariantDetail>? OrderVariantsList { get; set; }
        public OrderEntity? OrderObj { get; set; }
        public UserAddressEntity? OrderShippingMasterData { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
