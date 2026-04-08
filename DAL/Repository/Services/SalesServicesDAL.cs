using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.CommonModels;
using Entities.CommonModels.ProductsCatalogModule;
using Entities.CommonModels.SalesModule;
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
    public class SalesServicesDAL : ISalesServicesDAL
    {

        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;


        //--Constructor of the class
        public SalesServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;


        }

        public async Task<List<OrderEntity>> GetOrdersListDAL(OrderEntity FormData)
        {

            List<OrderEntity> result = new List<OrderEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.OrderId > 0)
                    {
                        SearchParameters.Append("AND CTM.OrderId =  @0 ", FormData.OrderId);
                    }


                    if (!String.IsNullOrEmpty(FormData.CustomerName))
                    {
                        SearchParameters.Append("AND CTM.CustomerName LIKE  @0", "%" + FormData.CustomerName + "%");
                    }

                    if (FormData.ProductId != null && FormData.ProductId > 0)
                    {
                        SearchParameters.Append("AND CTM.ProductId =  @0 ", FormData.ProductId);
                    }

                    if (FormData.VendorId > 0)
                    {
                        SearchParameters.Append("AND CTM.VendorId =  @0 ", FormData.VendorId);
                    }


                    if (FormData.LatestStatusId > 0)
                    {
                        SearchParameters.Append("AND CTM.LatestStatusId =  @0 ", FormData.LatestStatusId);
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(CTM.OrderDateUTC AS Date)>=@0", FormData.FromDate);
                    }


                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(CTM.OrderDateUTC AS Date)<=@0", FormData.ToDate);
                    }


                    var ppSql = PetaPoco.Sql.Builder.Append(@" ;WITH CTE_MAIN AS (")
                    .Select("DISTINCT ORD.OrderID, ORD.OrderNumber  ,ORD.CustomerID, ORD.OrderDateUTC, ORD.LatestStatusID, ORD.OrderTotal , ORS.StatusName AS LatestStatusName")
                    .Append(", (ISNULL(USR.FirstName,'')+ SPACE(1) + ISNULL(USR.LastName,'')) AS CustomerName , PRDC.VendorId , PRDC.ProductId")
                    .From("Orders ORD")
                    .InnerJoin("OrderItems OITM").On("ORD.OrderID=OITM.OrderID")
                    .InnerJoin("Products PRDC").On("PRDC.ProductId=OITM.ProductId")
                    .InnerJoin("Users USR").On("USR.UserID= ORD.CustomerID")
                    .LeftJoin("OrderStatuses ORS").On("ORD.LatestStatusID=ORS.StatusID")
                    .Append("),")
                    .Append("CTE_FINAL AS (")
                    .Append("SELECT  DISTINCT CTM.OrderID, CTM.OrderNumber  ,CTM.CustomerID, CTM.OrderDateUTC, CTM.LatestStatusID, CTM.OrderTotal , CTM.LatestStatusName , CTM.CustomerName")
                    .From("CTE_MAIN CTM")
                    .Where("CTM.OrderId is not null")
                    .Append(SearchParameters)
                    .Append(")")
                    .Select(" CF.* ,  COUNT(*) OVER () as TotalRecords")
                    .From(" CTE_FINAL CF")
                    .OrderBy("CF.OrderId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
                    FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);




                    result = context.Fetch<OrderEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<OrderEntity?> GetOrderDetailByIdDAL(OrderEntity FormData)
        {

            OrderEntity? result = new OrderEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {


                    context.EnableAutoSelect = false;

                    result = context.Fetch<OrderEntity>(@";EXEC [dbo].[SP_AdmPanel_GetOrderDetailById] @OrderId",
                          new
                          {
                              OrderId = FormData.OrderId

                          }).FirstOrDefault();


                    await Task.FromResult(result);
                    return result;




                }
                catch (Exception)
                {

                    throw;
                }

            }

        }


        public async Task<List<OrderStatusesEntity>> GetOrderStatusesList(OrderStatusesEntity FormData)
        {

            List<OrderStatusesEntity> result = new List<OrderStatusesEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.StatusId > 0)
                    {
                        SearchParameters.Append("AND MTBL.StatusId =  @0 ", FormData.StatusId);
                    }


                    if (!String.IsNullOrEmpty(FormData.StatusName))
                    {
                        SearchParameters.Append("AND MTBL.StatusName LIKE  @0", "%" + FormData.StatusName + "%");
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
                      .From(" OrderStatuses MTBL")
                      .Where("MTBL.StatusId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.StatusName ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<OrderStatusesEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<string> UpdateOrderShippingItemsDetailDAL(OrderShippingDetailEntity FormData)
        {
            string result = "";


            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute("Sp_AdmPanel_UpdateOrderShippingItems",
                        new
                        {
                            OrderId = FormData.OrderId,
                            OrderShippingDetailItemsJson = FormData.OrderShippingDetailItemsJson,
                            UserId = FormData.UserId,
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


        public async Task<List<OrderNoteEntity>> GetOrderNotesListDAL(OrderNoteEntity FormData)
        {

            List<OrderNoteEntity> result = new List<OrderNoteEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(ORDN.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(ORDN.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords, ORDN.* , USR.FirstName AS CreatedByFirstName, USR.LastName AS CreatedByLastName")
                      .From(" OrderNotes ORDN ")
                      .InnerJoin("Users USR").On("ORDN.CreatedBy =  USR.UserID")
                      .Where("ORDN.OrderID=@0", FormData.OrderId)
                      .Append(SearchParameters)
                     .OrderBy("ORDN.OrderNoteID ASC") //-- to read order note from old at the top and new at the bottom
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<OrderNoteEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<string> SaveOrderNoteReplyDAL(OrderNoteEntity FormData)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {


                    if (FormData.OrderNoteId > 0)
                    {
                        context.Execute(@"Insert into OrderNotes(OrderID , Message , ParentOrderNoteID , CreatedOn , CreatedBy)
                        VALUES(@OrderId , @Message , @OrderNoteId , GETDATE() , @UserId)",
                     new
                     {
                         OrderId = FormData.OrderId,
                         Message = FormData.Message,
                         OrderNoteId = FormData.OrderNoteId,
                         UserId = FormData.UserId,

                     });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        context.Execute(@"Insert into OrderNotes(OrderID , Message  , CreatedOn , CreatedBy)
                        VALUES(@OrderId , @Message  , GETDATE() , @UserId)",
                     new
                     {
                         OrderId = FormData.OrderId,
                         Message = FormData.Message,
                         UserId = FormData.UserId,

                     });
                        result = "Saved Successfully!";
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


        public async Task<string> UpdateOrderStatusDAL(int OrderId, int LatestStatusId, int UserId)
        {
            string result = "";


            try
            {

                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute(@"DECLARE @OrderStatusMappingID INT;
                    INSERT INTO OrderStatusesMapping(OrderID , StatusID , IsActive ,CreatedOn , CreatedBy)
                    VALUES(@OrderId , @LatestStatusId , 1 , GETDATE() , @UserId);
                    SET @OrderStatusMappingID = SCOPE_IDENTITY();

                    UPDATE OrderStatusesMapping SET IsActive=0 WHERE OrderID =@OrderId AND OrderStatusMappingID!=@OrderStatusMappingID

                    UPDATE Orders SET LatestStatusID= @LatestStatusId WHERE OrderID =@OrderId",
                        new
                        {
                            OrderId = OrderId,
                            LatestStatusId = LatestStatusId,
                            UserId = UserId,
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

        public async Task<List<OrderVariantDetail>> GetOrderVariantsDetailByIdDAL(int OrderId)
        {
            List<OrderVariantDetail> result = new List<OrderVariantDetail>();

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    var resultIEnumerable = await dbConnection.QueryAsync<OrderVariantDetail>(@"DROP TABLE IF EXISTS  #AttributesTempTable
                    CREATE TABLE #AttributesTempTable
	                    (	
	                    OrderAttributeMappingID INT,
	                    ProductAttributeID INT,
	                    OrderItemID INT,
	                    PrimaryKeyValue INT,
	                    PrimaryKeyDisplayText NVARCHAR(max),
	                    AttributeDisplayName NVARCHAR(300),
	                    AttributeSqlTableName NVARCHAR(300),
	                    IsPrimaryKeyDisplayTextSet BIT
                    )

                    DECLARE @resultResponse TABLE (Response NVARCHAR(MAX));

                    INSERT INTO #AttributesTempTable(OrderAttributeMappingID, ProductAttributeID , OrderItemID, PrimaryKeyValue, PrimaryKeyDisplayText, AttributeDisplayName, AttributeSqlTableName,
                    IsPrimaryKeyDisplayTextSet)
                    SELECT OPAM.OrderAttributeMappingID , OPAM.ProductAttributeID, OPAM.OrderItemID, OPAM.AttributeValue AS PrimaryKeyValue,'' as PrimaryKeyDisplayText,
                    PA.DisplayName AS AttributeDisplayName, PA.AttributeSqlTableName , 0
                    FROM OrderProductAttributeMapping OPAM
                    INNER JOIN OrderItems OI ON OI.OrderItemID = OPAM.OrderItemID
                    INNER JOIN ProductAttributes PA ON PA.ProductAttributeID =  OPAM.ProductAttributeID
                    WHERE OI.OrderID= @OrderID


                    DECLARE @limit INT;
                    DECLARE @count INT = 1;
                    SET @limit = (SELECT COUNT(*) FROM #AttributesTempTable);

                    WHILE @count <= @limit
                    BEGIN
	                    DECLARE @OrderAttributeMappingID INT, @ProductAttributeID INT ,@AttributeSqlTableName NVARCHAR(300), @PrimaryKeyValue INT;
		
	                    SELECT TOP 1 @OrderAttributeMappingID = OrderAttributeMappingID,   @ProductAttributeID =ProductAttributeID  , @AttributeSqlTableName = AttributeSqlTableName,
	                    @PrimaryKeyValue=PrimaryKeyValue 
	                    FROM #AttributesTempTable WHERE IsPrimaryKeyDisplayTextSet = 0 ;

			

	                    DECLARE @DisplayValueColumn NVARCHAR(100);
	                    DECLARE @DisplayTextColumn NVARCHAR(100);
	                    SET @DisplayValueColumn=(SELECT TOP 1 PAC.ColumnName FROM ProductAttributeColumns PAC WHERE PAC.ProductAttributeID=@ProductAttributeId AND PAC.ColumnType='PrimaryKey');
	                    SET @DisplayTextColumn=(SELECT TOP 1 PAC.ColumnName FROM ProductAttributeColumns PAC WHERE PAC.ProductAttributeID=@ProductAttributeId AND PAC.ColumnType='DisplayText');
			
	                    DECLARE @sourceQuery NVARCHAR(MAX);
	                    SET @sourceQuery=('SELECT TOP 1 ' +  @DisplayTextColumn + SPACE(1) + 'AS DisplayText' + SPACE(1)+'FROM ' + @AttributeSqlTableName + SPACE(1) +
					                    'WHERE ' + @DisplayValueColumn + ' = ' + CAST(@PrimaryKeyValue AS NVARCHAR(MAX)));

	                    INSERT INTO @resultResponse EXECUTE sp_executesql @sourceQuery
	                    UPDATE TOP(1) #AttributesTempTable SET PrimaryKeyDisplayText = (SELECT TOP(1) Response AS Data FROM @resultResponse), 
	                    IsPrimaryKeyDisplayTextSet = 1  
	                    WHERE OrderAttributeMappingID = @OrderAttributeMappingID;

	                    DELETE FROM @resultResponse

	                    SET @count += 1;
                    END

                    SELECT * FROM #AttributesTempTable
                    DROP TABLE IF EXISTS  #AttributesTempTable",
                            new
                            {
                                OrderId = OrderId

                            }
                          , commandType: CommandType.Text);
                    dbConnection.Close();


                    result = resultIEnumerable.ToList();

                    await Task.FromResult(result);
                    return result;

                }




            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<List<OrderRefundReasonType>> GetOrderRefundReasonTypeListDAL(OrderRefundReasonType FormData)
        {

            List<OrderRefundReasonType> result = new List<OrderRefundReasonType>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {

                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");



                    if (FormData.RefundReasonTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.RefundReasonTypeId =  @0 ", FormData.RefundReasonTypeId);
                    }




                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" OrderRefundReasonType MTBL")
                      .Where("MTBL.RefundReasonTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ReasonName ASC");
                   

                    result = context.Fetch<OrderRefundReasonType>(ppSql);

                    await Task.FromResult(result);
                    return result;


                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        public async Task<DigitalOrderInfoEntity?> GetDigitalOrderInfoForCustomerByIdDAL(int order_item_id, int user_id)
        {

            DigitalOrderInfoEntity? result = new DigitalOrderInfoEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {



                    var ppSql = PetaPoco.Sql.Builder.Select(@" OI.OrderID, OI.OrderItemID, PRD.ProductID,PRD.ProductName, PRD.IsDigitalProduct, DigitalFileMapping.DigitalFileDownloadUrl")
                      .From(" OrderItems OI")
                      .InnerJoin("Orders ORD").On("ORD.OrderID = OI.OrderID")
                      .InnerJoin("Products PRD").On("PRD.ProductID = OI.ProductID")
                      .Append(@"OUTER APPLY (
					        SELECT TOP 1 PDFM.ProductDigitalFileMappingID , ATC.AttachmentURL AS DigitalFileDownloadUrl
					        FROM ProductDigitalFileMapping PDFM
					        INNER JOIN Attachments ATC ON ATC.AttachmentID=PDFM.AttachmentID
					        WHERE PDFM.ProductID=PRD.ProductID
                            )DigitalFileMapping")
                      .Where("OI.OrderItemID = @0 AND ORD.CustomerID = @1", order_item_id, user_id);


                    result = context.Fetch<DigitalOrderInfoEntity>(ppSql).FirstOrDefault();

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
