using DAL.Repository.IServices;
using Entities.DBModels;
using Helpers.CommonHelpers.ICommonHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.CommonHelpers
{
    public class OrderHelper :  IOrderHelper
    {

        private readonly IConfiguration _configuration;
        private readonly IApiOperationServicesDAL _apiOperationServicesDAL;

        public OrderHelper(IConfiguration configuration, IApiOperationServicesDAL apiOperationServicesDAL)
        {
            _configuration = configuration;
            _apiOperationServicesDAL = apiOperationServicesDAL;
        }


        public async Task<string?> SaveCustomerOrderInDbWithRetry(Dictionary<string, object>? requestParametersRawOrder, Apiconfiguration? ApiConfiguration)
        {
            string? result = "";


            try
            {

                int tryTimes = 0;
                while (tryTimes < 2)
                {
                    try
                    {
                        result = await _apiOperationServicesDAL.GetApiData(requestParametersRawOrder, ApiConfiguration);

                      
                        break;
                    }
                    catch (Exception ex)
                    {


                        //-- Do nothing and just retry
                        if (tryTimes == 1)//-- If two operation failed in the try block
                        {
                            string MainOrderExceptionMsg = ex.Message;

                            //-- create a raw order
                            try
                            {
                                //--Get Api Configuration for raw order creation
                                string UrlNameForRawOrder = "create-raw-order";
                                var ApiConfigurationForRawOrder = await this._apiOperationServicesDAL.GetAPIConfiguration(UrlNameForRawOrder);
                                requestParametersRawOrder?.Add("MainOrderExceptionMsg", MainOrderExceptionMsg);

                                result = await _apiOperationServicesDAL.GetApiData(requestParametersRawOrder, ApiConfigurationForRawOrder);

                                break;
                            }
                            catch
                            {
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        tryTimes++; //-- Ensure whether exception or not, retry time++ here
                    }
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
}
