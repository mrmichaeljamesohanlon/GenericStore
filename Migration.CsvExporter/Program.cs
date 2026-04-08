using System.Globalization;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Migration.CsvExporter;

internal static class Program
{
    private const string DefaultOutputFileName = "product_migration.xlsx";
    private static readonly string[] Headers =
    {
        "ProductName",
        "ShortDescription",
        "FullDescription",
        "ManufacturerId",
        "VendorId",
        "IsActive",
        "ShowOnHomePage",
        "MarkAsNew",
        "AllowCustomerReviews",
        "SellStartDatetimeUTC",
        "SellEndDatetimeUTC",
        "Sku",
        "Price",
        "IsDiscountAllowed",
        "IsShippingFree",
        "ShippingCharges",
        "CategoriesIdsCommaSeperated",
        "TagsIdsCommaSeperated",
        "ShippingMethodsIdsCommaSeperated",
        "ColorsIdsCommaSeperated",
        "SizeIdsCommaSeperated",
        "ImagesIdsCommaSeperated",
        "EstimatedShippingDays",
        "IsReturnAble",
        "InventoryMethodId",
        "WarehouseId",
        "StockQuantity",
        "IsBoundToStockQuantity",
        "DisplayStockQuantity",
        "OrderMinimumQuantity",
        "OrderMaximumQuantity",
        "MetaTitle",
        "MetaKeywords",
        "MetaDescription"
    };

    private static async Task<int> Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var outputPath = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : Path.Combine(Environment.CurrentDirectory, DefaultOutputFileName);
        outputPath = EnsureXlsxExtension(outputPath);

        var connectionString = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : configuration["DBConnection"] ?? configuration["ConnectionStrings:Default"] ?? Environment.GetEnvironmentVariable("DB_CONNECTION");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.Error.WriteLine("Missing connection string. Provide DBConnection in appsettings.json, DB_CONNECTION env var, or pass as the second argument.");
            return 1;
        }

        var products = new List<ProductExportRow>();

        await using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var defaultCategoryId = await ResolveDefaultIdAsync(
                connection,
                configuration,
                "DefaultCategoryId",
                new[]
                {
                    new IdLookup("Category", "CategoryID"),
                    new IdLookup("Categories", "CategoryID")
                });
            var defaultTagId = await ResolveDefaultIdAsync(
                connection,
                configuration,
                "DefaultTagId",
                new[] { new IdLookup("Tags", "TagID") });
            var defaultImageId = await ResolveDefaultIdAsync(
                connection,
                configuration,
                "DefaultImageId",
                new[] { new IdLookup("Attachments", "AttachmentID") });

            if (defaultCategoryId is null || defaultTagId is null || defaultImageId is null)
            {
                Console.Error.WriteLine("Missing default IDs. Set DefaultCategoryId, DefaultTagId, and DefaultImageId in appsettings.json (or ensure those tables have data). ");
                return 1;
            }

            var tagMappings = await LoadMappingAsync(
                connection,
                new[] { new MappingLookup("ProductsTagsMapping", "ProductID", "TagID") });
            var imageMappings = await LoadMappingAsync(
                connection,
                new[] { new MappingLookup("ProductPicturesMapping", "ProductID", "PictureID") });
            var categoryMappings = await LoadMappingAsync(
                connection,
                new[]
                {
                    new MappingLookup("ProductsCategoriesMapping", "ProductID", "CategoryID"),
                    new MappingLookup("ProductCategoriesMapping", "ProductID", "CategoryID"),
                    new MappingLookup("ProductCategoryMapping", "ProductID", "CategoryID")
                });

            const string sql = @"
SELECT
    ProductID,
    Name,
    Description,
    Price,
    SalePrice,
    Code,
    Discount,
    MetaKeywords,
    MetaDescription,
    PromoFront,
    PromoDept
FROM dbo.Product";

            await using var command = new SqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
                var nameOrdinal = reader.GetOrdinal("Name");
                var name = reader.IsDBNull(nameOrdinal) ? string.Empty : reader.GetString(nameOrdinal);
                var descriptionOrdinal = reader.GetOrdinal("Description");
                var description = reader.IsDBNull(descriptionOrdinal) ? string.Empty : reader.GetString(descriptionOrdinal);
                var price = reader.GetDecimal(reader.GetOrdinal("Price"));
                var salePriceOrdinal = reader.GetOrdinal("SalePrice");
                var salePrice = reader.IsDBNull(salePriceOrdinal) ? (decimal?)null : reader.GetDecimal(salePriceOrdinal);
                var codeOrdinal = reader.GetOrdinal("Code");
                var code = reader.IsDBNull(codeOrdinal) ? string.Empty : reader.GetString(codeOrdinal);
                var discountOrdinal = reader.GetOrdinal("Discount");
                var discount = reader.IsDBNull(discountOrdinal) ? string.Empty : reader.GetString(discountOrdinal);
                var metaKeywordsOrdinal = reader.GetOrdinal("MetaKeywords");
                var metaKeywords = reader.IsDBNull(metaKeywordsOrdinal) ? string.Empty : reader.GetString(metaKeywordsOrdinal);
                var metaDescriptionOrdinal = reader.GetOrdinal("MetaDescription");
                var metaDescription = reader.IsDBNull(metaDescriptionOrdinal) ? string.Empty : reader.GetString(metaDescriptionOrdinal);
                var promoFront = reader.GetBoolean(reader.GetOrdinal("PromoFront"));
                var promoDept = reader.GetBoolean(reader.GetOrdinal("PromoDept"));

                var safeName = string.IsNullOrWhiteSpace(name) ? $"Product {productId}" : name.Trim();
                var safeDescription = string.IsNullOrWhiteSpace(description) ? $"{safeName} description" : description.Trim();
                var shortDescription = CreateShortDescription(safeDescription);
                var exportRow = new ProductExportRow
                {
                    ProductName = safeName,
                    ShortDescription = shortDescription,
                    FullDescription = safeDescription,
                    ManufacturerId = "0",
                    VendorId = "0",
                    IsActive = "1",
                    ShowOnHomePage = promoFront ? "1" : "0",
                    MarkAsNew = promoDept ? "1" : "0",
                    AllowCustomerReviews = "1",
                    SellStartDatetimeUTC = string.Empty,
                    SellEndDatetimeUTC = string.Empty,
                    Sku = code,
                    Price = (salePrice ?? price).ToString("0.##", CultureInfo.InvariantCulture),
                    IsDiscountAllowed = (!string.IsNullOrWhiteSpace(discount) || salePrice.HasValue) ? "1" : "0",
                    IsShippingFree = "0",
                    ShippingCharges = "0",
                    CategoriesIdsCommaSeperated = GetMappingOrDefault(categoryMappings, productId, defaultCategoryId),
                    TagsIdsCommaSeperated = GetMappingOrDefault(tagMappings, productId, defaultTagId),
                    ShippingMethodsIdsCommaSeperated = string.Empty,
                    ColorsIdsCommaSeperated = string.Empty,
                    SizeIdsCommaSeperated = string.Empty,
                    ImagesIdsCommaSeperated = GetMappingOrDefault(imageMappings, productId, defaultImageId),
                    EstimatedShippingDays = string.Empty,
                    IsReturnAble = "0",
                    InventoryMethodId = "1",
                    WarehouseId = "0",
                    StockQuantity = "0",
                    IsBoundToStockQuantity = "0",
                    DisplayStockQuantity = "0",
                    OrderMinimumQuantity = "1",
                    OrderMaximumQuantity = "0",
                    MetaTitle = safeName,
                    MetaKeywords = metaKeywords,
                    MetaDescription = metaDescription
                };

                products.Add(exportRow);
            }
        }

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Products");

        for (var col = 0; col < Headers.Length; col++)
        {
            worksheet.Cell(1, col + 1).Value = Headers[col];
        }

        for (var rowIndex = 0; rowIndex < products.Count; rowIndex++)
        {
            var values = GetRowValues(products[rowIndex]);
            for (var col = 0; col < values.Length; col++)
            {
                worksheet.Cell(rowIndex + 2, col + 1).Value = values[col];
            }
        }

        worksheet.Columns().AdjustToContents();
        workbook.SaveAs(outputPath);

        Console.WriteLine($"Exported {products.Count} products to {outputPath}.");
        return 0;
    }

    private static string CreateShortDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return "No description";
        }

        var normalized = description.ReplaceLineEndings(" ").Trim();
        return normalized.Length <= 250 ? normalized : normalized[..250];
    }

    private static string GetMappingOrDefault(Dictionary<int, List<int>> mapping, int productId, string defaultId)
    {
        if (mapping.TryGetValue(productId, out var ids) && ids.Count > 0)
        {
            return string.Join(',', ids.Distinct());
        }

        return defaultId;
    }

    private static async Task<string?> ResolveDefaultIdAsync(
        SqlConnection connection,
        IConfiguration configuration,
        string configKey,
        IEnumerable<IdLookup> lookups)
    {
        var configured = configuration[configKey];
        if (!string.IsNullOrWhiteSpace(configured))
        {
            return configured.Trim();
        }

        foreach (var lookup in lookups)
        {
            if (!await TableExistsAsync(connection, lookup.TableName))
            {
                continue;
            }

            var sql = $"SELECT TOP 1 {lookup.IdColumn} FROM dbo.{lookup.TableName} ORDER BY {lookup.IdColumn};";
            await using var command = new SqlCommand(sql, connection);
            var result = await command.ExecuteScalarAsync();
            if (result is not null && result != DBNull.Value)
            {
                return Convert.ToString(result, CultureInfo.InvariantCulture);
            }
        }

        return null;
    }

    private static async Task<Dictionary<int, List<int>>> LoadMappingAsync(
        SqlConnection connection,
        IEnumerable<MappingLookup> lookups)
    {
        foreach (var lookup in lookups)
        {
            if (!await TableExistsAsync(connection, lookup.TableName))
            {
                continue;
            }

            var sql = $@"
SELECT {lookup.ProductIdColumn}, {lookup.MappedIdColumn}
FROM dbo.{lookup.TableName}";

            var mapping = new Dictionary<int, List<int>>();
            await using var command = new SqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var productId = reader.GetInt32(0);
                var mappedId = reader.GetInt32(1);
                if (!mapping.TryGetValue(productId, out var list))
                {
                    list = new List<int>();
                    mapping[productId] = list;
                }
                list.Add(mappedId);
            }

            return mapping;
        }

        return new Dictionary<int, List<int>>();
    }

    private static async Task<bool> TableExistsAsync(SqlConnection connection, string tableName)
    {
        const string sql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = @TableName;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@TableName", tableName);
        var result = await command.ExecuteScalarAsync();
        return result is not null && Convert.ToInt32(result, CultureInfo.InvariantCulture) > 0;
    }

    private static string EnsureXlsxExtension(string outputPath)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            return DefaultOutputFileName;
        }

        var extension = Path.GetExtension(outputPath);
        if (string.IsNullOrWhiteSpace(extension))
        {
            return outputPath + ".xlsx";
        }

        if (!extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            return Path.ChangeExtension(outputPath, ".xlsx");
        }

        return outputPath;
    }

    private static string[] GetRowValues(ProductExportRow row)
        =>
        [
            row.ProductName,
            row.ShortDescription,
            row.FullDescription,
            row.ManufacturerId,
            row.VendorId,
            row.IsActive,
            row.ShowOnHomePage,
            row.MarkAsNew,
            row.AllowCustomerReviews,
            row.SellStartDatetimeUTC,
            row.SellEndDatetimeUTC,
            row.Sku,
            row.Price,
            row.IsDiscountAllowed,
            row.IsShippingFree,
            row.ShippingCharges,
            row.CategoriesIdsCommaSeperated,
            row.TagsIdsCommaSeperated,
            row.ShippingMethodsIdsCommaSeperated,
            row.ColorsIdsCommaSeperated,
            row.SizeIdsCommaSeperated,
            row.ImagesIdsCommaSeperated,
            row.EstimatedShippingDays,
            row.IsReturnAble,
            row.InventoryMethodId,
            row.WarehouseId,
            row.StockQuantity,
            row.IsBoundToStockQuantity,
            row.DisplayStockQuantity,
            row.OrderMinimumQuantity,
            row.OrderMaximumQuantity,
            row.MetaTitle,
            row.MetaKeywords,
            row.MetaDescription
        ];
}

internal sealed record IdLookup(string TableName, string IdColumn);

internal sealed record MappingLookup(string TableName, string ProductIdColumn, string MappedIdColumn);

internal sealed class ProductExportRow
{
    public string ProductName { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    public string ManufacturerId { get; set; } = string.Empty;

    public string VendorId { get; set; } = string.Empty;

    public string IsActive { get; set; } = string.Empty;

    public string ShowOnHomePage { get; set; } = string.Empty;

    public string MarkAsNew { get; set; } = string.Empty;

    public string AllowCustomerReviews { get; set; } = string.Empty;

    public string SellStartDatetimeUTC { get; set; } = string.Empty;

    public string SellEndDatetimeUTC { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public string Price { get; set; } = string.Empty;

    public string IsDiscountAllowed { get; set; } = string.Empty;

    public string IsShippingFree { get; set; } = string.Empty;

    public string ShippingCharges { get; set; } = string.Empty;

    public string CategoriesIdsCommaSeperated { get; set; } = string.Empty;

    public string TagsIdsCommaSeperated { get; set; } = string.Empty;

    public string ShippingMethodsIdsCommaSeperated { get; set; } = string.Empty;

    public string ColorsIdsCommaSeperated { get; set; } = string.Empty;

    public string SizeIdsCommaSeperated { get; set; } = string.Empty;

    public string ImagesIdsCommaSeperated { get; set; } = string.Empty;

    public string EstimatedShippingDays { get; set; } = string.Empty;

    public string IsReturnAble { get; set; } = string.Empty;

    public string InventoryMethodId { get; set; } = string.Empty;

    public string WarehouseId { get; set; } = string.Empty;

    public string StockQuantity { get; set; } = string.Empty;

    public string IsBoundToStockQuantity { get; set; } = string.Empty;

    public string DisplayStockQuantity { get; set; } = string.Empty;

    public string OrderMinimumQuantity { get; set; } = string.Empty;

    public string OrderMaximumQuantity { get; set; } = string.Empty;

    public string MetaTitle { get; set; } = string.Empty;

    public string MetaKeywords { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;
}
