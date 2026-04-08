using DAL.Repository.IServices;
using DAL.Repository.Services;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Entities.MainModels;
using Helpers.AuthorizationHelpers;
using Helpers.CommonHelpers;
using Helpers.CommonHelpers.Enums;
using Helpers.ConversionHelpers;
using MailKit.Search;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Text.RegularExpressions;


namespace AdminPanel.Helpers.TaskManagement
{
    public class TaskManagementExternsions : ITaskManagementExternsions
    {


        private readonly ITasksManagementServices _tasksManagementServices;
        private readonly ISessionManager _sessionManag;
        private readonly IUserManagementServicesDAL _userManagementServicesDAL;
        private readonly ISalesServicesDAL _salesServicesDAL;
        private readonly IConstants _constants;
        private readonly IBasicDataServicesDAL _basicDataServicesDAL;


        public TaskManagementExternsions(ITasksManagementServices tasksManagementServices, ISessionManager sessionManag, IUserManagementServicesDAL userManagementServicesDAL,
            ISalesServicesDAL salesServicesDAL, IConstants constants, IBasicDataServicesDAL basicDataServicesDAL)
        {
            this._tasksManagementServices = tasksManagementServices;
            this._sessionManag = sessionManag;
            this._userManagementServicesDAL = userManagementServicesDAL;
            this._salesServicesDAL = salesServicesDAL;
            this._constants = constants;
            this._basicDataServicesDAL = basicDataServicesDAL;
        }

        public async Task<string> PersistVendorRequest(int TaskId)
        {
            // ✅ Main Model
            TasksManagementModel model = new TasksManagementModel();

            string result = string.Empty;



            // Get vendor request detail
            model.vendorsAccountRequestObj = new VendorsAccountRequestEntity();
            model.vendorsAccountRequestObj = await this._tasksManagementServices.GetVendorAccountCreationRequestByTaskIdDAL(TaskId);

            UserEntity FormData = new UserEntity
            {
                CreatedBy = Convert.ToInt32(await this._sessionManag.GetLoginUserIdFromSession()),
                FirstName = model?.vendorsAccountRequestObj?.FirstName,
                MiddleName = model?.vendorsAccountRequestObj?.MiddleName,
                LastName = model?.vendorsAccountRequestObj?.LastName,
                EmailAddress = model?.vendorsAccountRequestObj?.EmailAddress,
                UserTypeId = (int)UserTypesEnum.Vendor,
                PhoneNo = model?.vendorsAccountRequestObj?.PhoneNo,
                MobileNo = model?.vendorsAccountRequestObj?.MobileNo,
                DateOfBirth = model?.vendorsAccountRequestObj?.DateOfBirth,
                Gender = model?.vendorsAccountRequestObj?.Gender,
                Password = model?.vendorsAccountRequestObj?.Password,
                IsActive = true,
                IsVerified = true,
                CountryId = model?.vendorsAccountRequestObj?.AddressOneCountryId,
                StateProvinceId = model?.vendorsAccountRequestObj?.AddressOneStateId,
                CityId = model?.vendorsAccountRequestObj?.AddressTwoCityId,
                AddressLineOne = model?.vendorsAccountRequestObj?.AddressOne,
                AddressLineTwo = null,
                PostalCode = model?.vendorsAccountRequestObj?.AddressOnePostalCode,
                ProfilePictureUrl = null,
                DataOperationType = (int)DataOperationType.Insert,
            };

            result = await _userManagementServicesDAL.CreateUpdateUserDAL(FormData);

            await Task.FromResult(result);
            return result;
        }

        public async Task<string> PersistOrderRefundRequest(int TaskId)
        {
            string result = string.Empty;



            // Get order refund request detail
            var OrderRefundRequestObj = new OrderRefundRequestEntity();
            OrderRefundRequestObj = await this._tasksManagementServices.GetOrderRefundRequestByTaskIdDAL(TaskId);

            //-- Get order detail by id
            if (OrderRefundRequestObj != null)
            {
                OrderEntity OrderFormData = new OrderEntity()
                {

                    OrderId = OrderRefundRequestObj.OrderId,

                };
                var OrderObj = await _salesServicesDAL.GetOrderDetailByIdDAL(OrderFormData);
                bool IsOrderStatusActive = (OrderObj?.LatestStatusId ?? 0) == (int)OrderStatusesEnum.Active ? true : false;
                var OrderPaymentsList = JsonConvert.DeserializeObject<List<OrdersPaymentEntity>>(OrderObj?.OrderPaymentDetailsJson ?? "[]");
                var OrderPaymentsObj = OrderPaymentsList?.FirstOrDefault();


                if (OrderObj != null && OrderObj.OrderId > 0 && OrderObj.OrderTotal > 0 && IsOrderStatusActive == true &&
                       OrderPaymentsList != null && OrderPaymentsList.Any(x => x.PaymentMethodId == (int)PaymentMethodsEnum.PayPal || x.PaymentMethodId == (int)PaymentMethodsEnum.Stripe))
                {

                    if (OrderPaymentsObj != null && OrderPaymentsObj?.PaymentMethodId == (int)PaymentMethodsEnum.Stripe)
                    {

                        result = await RefundPaymentUsingStripe((OrderRefundRequestObj?.OrderId ?? 0), Convert.ToDecimal(OrderRefundRequestObj?.RefundAmount ?? 0), OrderPaymentsObj.StripeChargeId, OrderPaymentsObj.CurrencyId);
                        if (result == "The refund was successful")
                        {
                            result = "Saved Successfully!";
                        }
                        else
                        {
                            result = "An error occured. Please try again!";
                        }

                    }
                    else if (OrderPaymentsList?.FirstOrDefault()?.PaymentMethodId == (int)PaymentMethodsEnum.PayPal)
                    {

                        result = await RefundPaymentUsingStripe((OrderRefundRequestObj?.OrderId ?? 0), Convert.ToDecimal(OrderRefundRequestObj?.RefundAmount ?? 0), OrderPaymentsObj.StripeChargeId, OrderPaymentsObj.CurrencyId);

                        if (result == "The refund was successful")
                        {
                            result = "Saved Successfully!";
                        }
                        else
                        {
                            result = "An error occured. Please try again!";
                        }
                    }
                    else
                    {
                        result = "No refund payment method supported for this order!";
                    }

                    //--persist data in DB if success
                    if (result == "Saved Successfully!")
                    {
                        int? LoginUserId = await this._sessionManag.GetLoginUserIdFromSession();
                        result = await _tasksManagementServices.UpdateOrderDataAndStatusAfterSuccessfullRefundDAL(OrderRefundRequestObj?.OrderId ?? 0, LoginUserId);
                        if (result != "Saved Successfully!")
                        {
                            result = "Order refund successfully but order status not updated on database!";
                        }
                       
                    }

                }
                else
                {
                    result = "Order status is other than Active. Can not be refunded!";

                }


            }




            await Task.FromResult(result);
            return result;
        }

        private async Task<string> RefundPaymentUsingStripe(int OrderId, decimal Amount,string? StripeChargeId, int CurrencyId)
        {

            string result = string.Empty;

            StripeConfiguration.ApiKey = _constants.GetAppSettingKeyValue("AppSetting", "StripeSecretKey");

            //--Get currency code by currency id
            CurrencyEntity currencyEntity = new CurrencyEntity
            {
                CurrencyId = CurrencyId,
                PageNo = 1,
                PageSize = 1
            };
            var CurrenciesList = await _basicDataServicesDAL.GetCurrenciesListDAL(currencyEntity);
            string? currency = string.Empty;
            if (CurrenciesList!=null && CurrenciesList.Count() > 0)
            {
                currency = CurrenciesList?.FirstOrDefault()?.CurrencyCode;
            }
            else
            {
                throw new Exception("Invalid currency code in stripe refund method");
            }

         
            var refundAmount = currency == "usd" ? (long)(Amount * 100) : (long)Amount; // Custom refund amount in cents


            var service = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                Charge = StripeChargeId,
                Amount = (long)refundAmount,
                //Reason = ""
            };

            var refund = await service.CreateAsync(refundOptions);

            if (refund.Status == "succeeded")
            {
                // The refund was successful
                result = "The refund was successful";
            }
            else
            {
                // The refund failed
                result = "The refund failed";
            }


            await Task.FromResult(result);
            return result;
        }

        private async Task<string> RefundPaymentUsingPayPal(int OrderId, decimal Amount, string? StripeChargeId, int CurrencyId)
        {

            string result = string.Empty;

         

            await Task.FromResult(result);
            return result;
        }
    }
}
