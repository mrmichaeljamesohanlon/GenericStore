using Entities.CommonModels.HomeModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IServices
{
    public interface IHomeServicesDAL
    {

         Task<List<TwoDimensionChart>> GetChartSalesPerMonthDataDAL(string FromDate, string ToDate, int LoginUserId);
        Task<List<TwoDimensionChart>> GetChartOrderRevenuePerMonthDataDAL(string FromDate, string ToDate, int LoginUserId);
        Task<List<SalesPopularCategoriesChart>> GetSalesPopularCategoriesDataDAL(string FromDate, string ToDate, int LoginUserId);
        Task<List<TwoDimensionChart>> GetChartCustomersLocationWiseDataDAL(string FromDate, string ToDate, int LoginUserId);
        Task<List<TwoDimensionChart>> GetChartPopularProductsDataDAL(string FromDate, string ToDate, int LoginUserId);
        Task<DashboardLifeTimeStatistics?> GetDashboardLifetimeStatisticsDAL(string FromDate, string ToDate, int LoginUserId);
    }
}
