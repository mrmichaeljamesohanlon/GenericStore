using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Address
    {
        public Address()
        {
            UserAddressesMappings = new HashSet<UserAddressesMapping>();
        }

        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressLineOne { get; set; } = null!;
        public string? AddressLineTwo { get; set; }
        public int CountryId { get; set; }
        public int? StateProvinceId { get; set; }
        public int? CityId { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual AddressType AddressType { get; set; } = null!;
        public virtual City? City { get; set; }
        public virtual Country Country { get; set; } = null!;
        public virtual StateProvince? StateProvince { get; set; }
        public virtual ICollection<UserAddressesMapping> UserAddressesMappings { get; set; }
    }
}
