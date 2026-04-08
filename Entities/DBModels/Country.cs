using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            StateProvinces = new HashSet<StateProvince>();
            UserAddresses = new HashSet<UserAddress>();
            VendorsAccountRequestAddressOneCountries = new HashSet<VendorsAccountRequest>();
            VendorsAccountRequestAddressTwoCountries = new HashSet<VendorsAccountRequest>();
            VendorsAccountRequestHomeCountries = new HashSet<VendorsAccountRequest>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public int? FlagUrlid { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public string? CountryCode { get; set; }
        public int? CurrencyId { get; set; }
        public int? WorldRegionId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual Attachment? FlagUrl { get; set; }
        public virtual WorldRegion? WorldRegion { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressOneCountries { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressTwoCountries { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestHomeCountries { get; set; }
    }
}
