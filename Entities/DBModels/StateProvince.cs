using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class StateProvince
    {
        public StateProvince()
        {
            Cities = new HashSet<City>();
            UserAddresses = new HashSet<UserAddress>();
            VendorsAccountRequestAddressOneStates = new HashSet<VendorsAccountRequest>();
            VendorsAccountRequestAddressTwoStates = new HashSet<VendorsAccountRequest>();
        }

        public int StateProvinceId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressOneStates { get; set; }
        public virtual ICollection<VendorsAccountRequest> VendorsAccountRequestAddressTwoStates { get; set; }
    }
}
