using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.CommonModels.HomeModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class HomeServicesDAL : IHomeServicesDAL
    {

        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;


        //--Constructor of the class
        public HomeServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }


        public async Task<List<TwoDimensionChart>> GetChartSalesPerMonthDataDAL(string FromDate, string ToDate, int LoginUserId)
        {

            List<TwoDimensionChart> result = new List<TwoDimensionChart>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");


                    if ( LoginUserId > 0)
                    {
                        SearchParameters.Append("AND PRD.VendorID = @0 ", LoginUserId);
                    }

                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)<=@0", ToDate);
                    }

                    //var ppSql = PetaPoco.Sql.Builder.Append(@" ;WITH CTE_Main AS (")
                    //  .Select("DISTINCT TOP 12 YEAR(ORD.OrderDateUTC) as Year  , DATENAME(month,ORD.OrderDateUTC) AS ChartLabel ,MONTH(ORD.OrderDateUTC) AS MonthInNumber , COUNT(*) OVER(PARTITION BY MONTH(ORD.OrderDateUTC)) as ChartValue")
                    //  .From("Orders ORD")
                    //  .Where("ORD.OrderId is not null")
                    //  .Append(SearchParameters)
                    //  .Append(")")
                    //  .Select(" * FROM CTE_Main CTM")
                    // .OrderBy("CTM.Year DESC
                    // 

                    var ppSql = PetaPoco.Sql.Builder.Append(@"SELECT DISTINCT TOP 12 YEAR(ORD.OrderDateUTC) as Year  , DATENAME(month,ORD.OrderDateUTC) AS ChartLabel ,MONTH(ORD.OrderDateUTC) AS MonthInNumber , COUNT(*) OVER(PARTITION BY MONTH(ORD.OrderDateUTC)) as ChartValue
                                                            FROM (
                                                            SELECT DISTINCT ORD.OrderID ,ORD.OrderDateUTC FROM Orders ORD
                                                            INNER JOIN OrderItems OI ON ORD.OrderID =  OI.OrderID
                                                            INNER JOIN PRODUCTS PRD ON PRD.ProductID = OI.ProductID
                                                            WHERE ORD.OrderId is not null")
                        .Append(SearchParameters)
                        .Append(") SUB")
                        .Append("JOIN Orders ORD ON ORD.OrderID = SUB.OrderID")
                        .Append("ORDER BY YEAR(ORD.OrderDateUTC) ");



                    result = context.Fetch<TwoDimensionChart>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }


        public async Task<List<TwoDimensionChart>> GetChartOrderRevenuePerMonthDataDAL(string FromDate, string ToDate, int LoginUserId)
        {

            List<TwoDimensionChart> result = new List<TwoDimensionChart>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");


                    if (LoginUserId > 0)
                    {
                        SearchParameters.Append("AND PRD.VendorID = @0", LoginUserId);
                    }

                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)<=@0", ToDate);
                    }

                    //var ppSql = PetaPoco.Sql.Builder.Append(@" ;WITH CTE_Main AS (")
                    //  .Select("DISTINCT TOP 12 YEAR(ORD.OrderDateUTC) as Year  , DATENAME(month,ORD.OrderDateUTC) AS ChartLabel ,MONTH(ORD.OrderDateUTC) AS MonthInNumber ,  SUM(isnull(ORD.OrderTotal,0)) OVER(PARTITION BY MONTH(ORD.OrderDateUTC)) as ChartValue")
                    //  .From("Orders ORD")
                    //  .Where("ORD.OrderId is not null")
                    //  .Append(SearchParameters)
                    //  .Append(")")
                    //  .Select(" * FROM CTE_Main CTM")
                    // .OrderBy("CTM.Year DESC");

                    var ppSql = PetaPoco.Sql.Builder.Append(@"SELECT DISTINCT TOP 12 YEAR(ORD.OrderDateUTC) as Year  , DATENAME(month,ORD.OrderDateUTC) AS ChartLabel ,MONTH(ORD.OrderDateUTC) AS MonthInNumber ,  SUM(isnull(ORD.OrderTotal,0)) OVER(PARTITION BY MONTH(ORD.OrderDateUTC)) as ChartValue 
	                                                        FROM (
		                                                        SELECT DISTINCT ORD.OrderID ,ORD.OrderDateUTC FROM Orders ORD
		                                                        INNER JOIN OrderItems OI ON ORD.OrderID =  OI.OrderID
		                                                        INNER JOIN PRODUCTS PRD ON PRD.ProductID = OI.ProductID
		                                                        WHERE ORD.OrderId is not null")
                     .Append(SearchParameters)
                    .Append(") SUB")
                     .Append("JOIN Orders ORD ON ORD.OrderID = SUB.OrderID")
                     .Append("ORDER BY YEAR(ORD.OrderDateUTC) ");


                    result = context.Fetch<TwoDimensionChart>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }


        public async Task<List<SalesPopularCategoriesChart>> GetSalesPopularCategoriesDataDAL(string FromDate, string ToDate, int LoginUserId)
        {

            List<SalesPopularCategoriesChart> result = new List<SalesPopularCategoriesChart>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    var SearchParametersVendor = PetaPoco.Sql.Builder.Append(" ");




                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)<=@0", ToDate);
                    }

                    if (LoginUserId > 0)
                    {
                        SearchParametersVendor.Append("AND CTG.VendorID = @0", LoginUserId);
                    }


                    var ppSql = PetaPoco.Sql.Builder.Append(@";WITH CTE_Main AS (
                    SELECT   OI.OrderItemID , OI.ProductID,PRD.VendorID, PCM.CategoryID FROM OrderItems OI 
                    INNER JOIN Products PRD ON OI.ProductID = PRD.ProductID 
                    INNER JOIN ORDERS ORD ON ORD.OrderId = OI.OrderID
                    OUTER APPLY( 
                    SELECT TOP 1 PCM.CategoryID FROM ProductsCategoriesMapping PCM 
                    WHERE PCM.ProductID =  PRD.ProductID 
                    )PCM 
                    where ord.OrderID is not null")
                    .Append(SearchParameters)
                    .Append(@" ) , 
                    CTE_Categories AS ( 
                    SELECT CTM.* , CT.Name As CategoryName, CT.ParentCategoryID FROM CTE_Main CTM 
                    INNER JOIN Categories CT ON CT.CategoryID=  CTM.CategoryID 
                    ) 
                    SELECT  distinct TOP 15 CTG.CategoryID, CTG.ParentCategoryID, ctg.CategoryName , PRNT.Name AS ParentCategoryName 
                    , COUNT(*) OVER (PARTITION BY CTG.CategoryID) AS TotalSale 
                    FROM CTE_Categories CTG 
                    INNER JOIN Categories PRNT ON PRNT.CategoryID = CTG.ParentCategoryID ")
                    .Where("CTG.CategoryID IS NOT NULL")
                     .Append(SearchParametersVendor)
                    .OrderBy("TotalSale DESC");


                    result = context.Fetch<SalesPopularCategoriesChart>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<List<TwoDimensionChart>> GetChartCustomersLocationWiseDataDAL(string FromDate, string ToDate, int LoginUserId)
        {

            List<TwoDimensionChart> result = new List<TwoDimensionChart>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");


                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParameters.Append("AND Cast(USR.CreatedOn AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParameters.Append("AND Cast(USR.CreatedOn AS Date)<=@0", ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@"  DISTINCT TOP 12  CTRN.CountryName AS ChartLabel , COUNT(*) OVER(PARTITION BY MONTH(CTRN.CountryID)) as ChartValue")
                      .From("Users USR")
                      .InnerJoin("Countries CTRN").On("USR.CountryID = CTRN.CountryID")
                      .Where("USR.UserID is not null")
                      .Append(SearchParameters)
                     .OrderBy("ChartValue DESC");


                    result = context.Fetch<TwoDimensionChart>(ppSql);

                    //--If vendor id is greater than zero
                    if (LoginUserId > 0)
                    {
                        result = new List<TwoDimensionChart>();
                    }

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<List<TwoDimensionChart>> GetChartPopularProductsDataDAL(string FromDate, string ToDate, int LoginUserId)
        {

            List<TwoDimensionChart> result = new List<TwoDimensionChart>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");


                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)>=@0", FromDate);
                    }
                    if (LoginUserId > 0)
                    {
                        SearchParameters.Append("AND PRD.VendorID =@0", LoginUserId);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParameters.Append("AND Cast(ORD.OrderDateUTC AS Date)<=@0", ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" DISTINCT TOP 10 OI.ProductID, PRD.ProductName AS ChartLabel , COUNT(OI.ProductID) OVER(PARTITION BY OI.ProductID) AS ChartValue")
                      .From("OrderItems OI")
                      .InnerJoin("Orders ORD").On("ORD.OrderID = OI.OrderID")
                      .InnerJoin("Products PRD").On("OI.ProductID=  PRD.ProductID")
                      .Where("OI.OrderItemId is not null")
                      .Append(SearchParameters);



                    result = context.Fetch<TwoDimensionChart>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }

        public async Task<DashboardLifeTimeStatistics?> GetDashboardLifetimeStatisticsDAL(string FromDate, string ToDate, int LoginUserId)
        {

            DashboardLifeTimeStatistics? result = new DashboardLifeTimeStatistics();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    #region search parameters of products
                    var SearchParametersProduct = PetaPoco.Sql.Builder.Append(" ");


                    if (LoginUserId > 0)
                    {
                        SearchParametersProduct.Append("AND PRD.VendorID = @0", LoginUserId);
                    }

                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParametersProduct.Append("AND Cast(PRD.CreatedOn AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParametersProduct.Append("AND Cast(PRD.CreatedOn AS Date)<=@0", ToDate);
                    }
                    #endregion

                    #region search parameters of orders
                    var SearchParametersOrder = PetaPoco.Sql.Builder.Append(" ");


                    if (LoginUserId > 0)
                    {
                        SearchParametersOrder.Append("AND PRD.VendorID = @0", LoginUserId);
                    }

                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParametersOrder.Append("AND Cast(ORD.OrderDateUTC AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParametersOrder.Append("AND Cast(ORD.OrderDateUTC AS Date)<=@0", ToDate);
                    }
                    #endregion

                    #region search parameters of users
                    var SearchParametersProductUser = PetaPoco.Sql.Builder.Append(" ");


                   

                    if (!String.IsNullOrEmpty(FromDate))
                    {
                        SearchParametersProductUser.Append("AND Cast(USR.CreatedOn AS Date)>=@0", FromDate);
                    }

                    if (!String.IsNullOrEmpty(ToDate))
                    {
                        SearchParametersProductUser.Append("AND Cast(USR.CreatedOn AS Date)<=@0", ToDate);
                    }
                    #endregion




                    //var ppSql = PetaPoco.Sql.Builder.Append(@" ;WITH CTE_Products AS (
                    //    SELECT COUNT(*) AS TotalProducts
                    //    FROM Products PRD where prd.ProductId is not null ")
                    //    .Append(SearchParametersProduct)
                    //    .Append(") ,")
                    //    .Append(@" CTE_Orders AS (
                    //    SELECT COUNT(*) AS TotalOrders, SUM(ORD.OrderTotal) AS TotalIncome
                    //    FROM Orders ORD where ord.OrderId is not null")
                    //    .Append(SearchParametersOrder)
                    //    .Append(") ,")
                    //    .Append(@" CTE_Users AS (
                    //    SELECT COUNT(*) AS TotalUsers
                    //    FROM Users USR where usr.UserId is not null")
                    //    .Append(SearchParametersProductUser)
                    //    .Append(" ) ")
                    //    .Append(@" SELECT PRD.TotalProducts 
                    //    , (SELECT ORD.TotalOrders FROM CTE_Orders ORD) AS TotalOrders
                    //    , (SELECT ORD.TotalIncome FROM CTE_Orders ORD) AS TotalIncome
                    //    , (SELECT USR.TotalUsers FROM CTE_Users USR) AS TotalUsers

                    //    FROM
                    //    CTE_Products PRD ");


                    var ppSql = PetaPoco.Sql.Builder.Append(@";WITH CTE_Products AS (
                    SELECT COUNT(*) AS TotalProducts 
                    FROM Products PRD 
                    where prd.ProductId is not null")
                    .Append(SearchParametersProduct)
                    .Append("),")    
                    .Append(@"CTE_Orders AS (
	                SELECT COUNT(ORD.OrderID) AS TotalOrders, SUM(ORD.OrderTotal) AS TotalIncome  FROM (
		            SELECT DISTINCT ORD.OrderID, ORD.OrderTotal FROM Orders ORD
		            INNER JOIN OrderItems OI ON ORD.OrderID =  OI.OrderID
		            INNER JOIN PRODUCTS PRD ON PRD.ProductID = OI.ProductID
		            WHERE ord.OrderId is not null")
                    .Append(SearchParametersOrder)
                    .Append(@"	) AS D
	                JOIN Orders ORD ON D.OrderID = ORD.OrderID")
                    .Append("),")
                    .Append(@" CTE_Users AS (
                    SELECT COUNT(*) AS TotalUsers
                    FROM Users USR where usr.UserId is not null")
                    .Append(SearchParametersProductUser)
                    .Append(" ) ")
                    .Append(@" SELECT PRD.TotalProducts 
                    , (SELECT ORD.TotalOrders FROM CTE_Orders ORD) AS TotalOrders
                    , (SELECT ORD.TotalIncome FROM CTE_Orders ORD) AS TotalIncome
                    , (SELECT USR.TotalUsers FROM CTE_Users USR) AS TotalUsers

                    FROM
                    CTE_Products PRD ");


                    result = context.Fetch<DashboardLifeTimeStatistics>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;

                    throw;
                }

            }

        }
    }
}
