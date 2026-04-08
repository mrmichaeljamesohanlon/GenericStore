using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.CommonHelpers.Enums
{
    public enum EntitiesEnum : int
    {
        ProductsList = 1,
        UsersList = 2,
        CountriesList = 3,
        StatesList = 4,
        AddressTypesList = 5,
        ColorsList = 6,
        CategoriesList = 7,
        TagsList = 8,
        SizesList = 9,
        ManufacturersList = 10,
        CurrenciesList = 11,
        AttachmentTypesList = 12,
        PaymentMethodsList = 13,
        DiscountsList = 14,
        ContactUsList = 15,
        SubscribersList = 16,
        SiteHomeBannersList = 17,
        Dashboard = 18,
        ProductsCatalog = 19,
        UsersManagement = 20,
        BasicData = 21,
        Promotions = 22,
        CititesList = 23,
        Configuration = 24,
        RolesRightsSetting = 25,
        Sales = 26,
        OrdersList = 27,
        OrderDetail = 28,
        Notifications = 29,
        Admin_Notifications = 30,
        DiscountCampaigns = 31,
        Accounts = 32,
        Banks = 33,
        UsersBankAccounts = 34,
        VendorPayments = 35,
        VendorsCommissionSetup = 36,
        VendorAccountsTransaction = 37,
        SitesLogo = 38,
        ProductsReviews = 39,
        ProductsBulkUpload = 40,
        ImagesUpload = 41,
        ScreensLocalization = 42,
        MenuLocalization = 43,

        [Description("Create New Product")]
        CreateNewProduct = 44,
        [Description("Update Product")]
        UpdateProduct = 45,
        [Description("Product Review Detail")]
        ProductReviewDetail = 46,
        [Description("Languages")]
        Languages = 47,
        [Description("Create New User")]
        CreateNewUser = 48,
        [Description("Update User")]
        UpdateUser = 49,
        [Description("Create New User Bank Account")]
        CreateNewUserBankAccount = 50,
        [Description("Update User Bank Account")]
        UpdateUserBankAccount = 51,
        [Description("Create New Discount")]
        CreateNewDiscount = 52,
        [Description("Update Discount")]
        UpdateDiscount = 53,

        [Description("Menu Localization Detail")]
        MenuLocalizationDetail = 54,
        [Description("Product Variants")]
        ProductVariants = 55,
        [Description("Product Variant Detail")]
        ProductVariantDetail = 56, 
        [Description("Dynamic languages Localization Detail")]
        DynamicLocalizationDetail = 93,

        [Description("Tasks Management")]
        TasksManagement = 95,
        [Description("Requests Queue")]
        RequestsQueue = 96,






    }
}
