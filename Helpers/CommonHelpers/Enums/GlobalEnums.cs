using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.CommonHelpers.Enums
{

    public enum DataOperationType : short
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        ViewAll = 5
    }

    public enum HTMLFieldTypeEnums : short
    {
        Search = 1,
        Date = 2,
        Text = 3,
        Dropdown = 4,
        Number = 5,
        Hidden = 6,
        Color = 7,
        Email = 8,
        Password = 9,
        File = 10
    }

    public enum HtmlBootsrapModalTypes : short
    {
        Large = 1,
        Medium = 2,
        Small = 3
    }

    public enum HtmlDropdownSelectType : short
    {
        Simple = 1,
        Select2 = 2,  //--In this type, there is search option in the drop down data

    }

    public enum SqlDeleteTypes : short
    {

        PlainTableDelete = 1,
        ForeignKeyDelete = 2,

    }

    public enum UserTypesEnum : short
    {
        Admin = 1,
        Customer = 2,
        Vendor = 3,
        Shipper = 4,

    }

    public enum DiscountValueTypeEnum : short
    {
        FixedAmount = 1,
        PercentageAmount = 2,

    }

    public enum DiscountTypesEnum : short
    {
        AppliedOnOrderTotal = 1,
        AppliedOnProducts = 3,
        AppliedOCategories = 4,
        AppliedOnManufacturers = 5,
        AppliedOnCities = 6,
        AppliedOnShipping = 7


    }

    public enum DataExportTypeEnum : short
    {
        Excel = 1,
        PDF = 2,
        CSV = 3
    }

    public enum UserRightsEnum : short
    {
        Add = 1,
        Update = 2,
        Delete = 3,
        View_Self = 4,
        View_All = 5

    }

    public enum IsActiveTypeEnum : short
    {
        Active = 1,
        InActive = 0,
        NoValue = 3  //--use this type if you do not want to apply IsActive check in where conditions etc


    }

    public enum ApiStatusCodes : short
    {
        OK = 200,
    }

    public enum PaymentMethodsEnum : short
    {
        PayPal = 2,
        Visa = 3,
        BankTransfer = 4,
        Stripe = 5,
        CashOnDelivery = 6,
    }

    public enum AppModulesEnum : short
    {
        CustomerPortal = 1,
        AdminPanel = 2,

    }

    public enum LanguagesEnum : short
    {
        [Description("en")]
        English = 1,
        [Description("ar")]
        Arabic = 2,
        [Description("spn")]
        Spanish = 3,
        [Description("frn")]
        French = 4,
        [Description("rus")]
        Russian = 5,
        [Description("turk")]
        Turkish = 6,
    }

    public enum EntityTypesEnum : short
    {
        [Description("Screen")]
        Screen = 1,
        [Description("Parent Nav Menu Toggle")]
        ParentNavMenuToggle = 2,
        [Description("Form")]
        Form = 3,
        [Description("Button")]
        Button = 4,
        [Description("React Component")]
        ReactComponent = 5,

    }

    public enum PriceAdjustmentTypeEnum : short
    {
        [Description("Fixed Value")]
        FixedValue = 1,
        [Description("Percentage")]
        Percentage = 2,
    }

    public enum ProductAttributesEnum : short
    {
        [Description("Color")]
        Color = 1,
        [Description("Size")]
        Size = 2,
        [Description("AttrProcessor")]
        AttrProcessor = 3,
        [Description("AttrRAM")]
        AttrRAM = 4,
        [Description("AttrHDD")]
        AttrHDD = 5,
        [Description("AttrWeight")]
        AttrWeight = 6,
        [Description("AttrFlavor")]
        AttrFlavor = 7,

    }

    public enum LocalizationTablesEnum : short
    {
        [Description("Categories")]
        Categories = 1,
        [Description("Tags")]
        Tags = 2,
        [Description("Sizes")]
        Sizes = 3,
        [Description("Manufacturers")]
        Manufacturers = 4,

    }

    public enum RequestTypesEnum : short
    {
        [Description("Vendor Request")]
        VendorRequest = 1,
        [Description("Order Refund Request")]
        OrderRefundRequest = 2

    }
    public enum RequestStatusEnum : short
    {
        [Description("In Process")]
        InProcess = 1,
        [Description("New")]
        New = 2,
        [Description("Approved")]
        Approved = 3,
        [Description("Declined")]
        Declined = 4

    }

    public enum OrderStatusesEnum : short
    {
        [Description("Active")]
        Active = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Returned")]
        Returned = 4,
        [Description("Refunded")]
        Refunded = 5

    }

}
