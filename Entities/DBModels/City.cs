using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class City
    {
        public City()
        {
            DiscountCitiesMappings = new HashSet<DiscountCitiesMapping>();
            UserAddresses = new HashSet<UserAddress>();
            VendorsAccountRequestAddressOneCities = new HashSet<VendorsAccountRequest>();
            VendorsAccountRequestAddressTwoCities = new HashSet<VendorsAccountRequest>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int CountryId { get; set; }
        public int? StateProvinceId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual StateProvince? StateProvince { get; set; }
        public virtual ICollection<DiscountCitiesMapping> DiscountCitiesMappings { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressOneCities { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressTwoCities { get; set; }
    }
}
