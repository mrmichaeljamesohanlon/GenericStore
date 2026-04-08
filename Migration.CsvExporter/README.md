# Migration CSV Exporter

This console app exports products from the `SparkDevelopments` database into an XLSX file with the headers required by the Noor eCommerce product import.

## What it does

- Reads from `dbo.Product`.
- Outputs `product_migration.xlsx` (default) with these headers:
  `ProductName`, `ShortDescription`, `FullDescription`, `ManufacturerId`, `VendorId`, `IsActive`, `ShowOnHomePage`, `MarkAsNew`, `AllowCustomerReviews`, `SellStartDatetimeUTC`, `SellEndDatetimeUTC`, `Sku`, `Price`, `IsDiscountAllowed`, `IsShippingFree`, `ShippingCharges`, `CategoriesIdsCommaSeperated`, `TagsIdsCommaSeperated`, `ShippingMethodsIdsCommaSeperated`, `ColorsIdsCommaSeperated`, `SizeIdsCommaSeperated`, `ImagesIdsCommaSeperated`, `EstimatedShippingDays`, `IsReturnAble`, `InventoryMethodId`, `WarehouseId`, `StockQuantity`, `IsBoundToStockQuantity`, `DisplayStockQuantity`, `OrderMinimumQuantity`, `OrderMaximumQuantity`, `MetaTitle`, `MetaKeywords`, `MetaDescription`.

## Configuration

Set your connection string in one of the following places (priority order):

1. Second CLI argument.
2. `DBConnection` key in `appsettings.json`.
3. `DB_CONNECTION` environment variable.

The app writes boolean-like fields as `1` or `0`.

### Required default IDs

To satisfy import validation rules, each row must contain at least one category, tag, and image ID. Configure defaults in `appsettings.json`:

```json
{
  "DefaultCategoryId": "1",
  "DefaultTagId": "1",
  "DefaultImageId": "1"
}
```

If mapping tables exist, the exporter uses them; otherwise, it falls back to these defaults.

## Usage

- Default output path is `product_migration.xlsx` in the current directory.
- Optional arguments: `[outputPath] [connectionString]`.

Examples (PowerShell):

```powershell
# Uses appsettings.json
dotnet run --project .\Migration.CsvExporter\Migration.CsvExporter.csproj

# Custom output + connection string
dotnet run --project .\Migration.CsvExporter\Migration.CsvExporter.csproj -- .\exports\products.csv "Server=...;Database=...;User Id=...;Password=...;"
```

## Notes

- Category/tag/image IDs are populated from mapping tables when available:
  - `ProductsTagsMapping` (TagID)
  - `ProductPicturesMapping` (PictureID)
  - Category mappings (tries `ProductsCategoriesMapping`, `ProductCategoriesMapping`, `ProductCategoryMapping`)
- If no mapping exists for a product, the exporter uses the default IDs.
