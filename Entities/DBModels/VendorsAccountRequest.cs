using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class VendorsAccountRequest
    {
        public int ReqtVendorId { get; set; }
        public int? TaskId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public bool? IsTermAccepted { get; set; }
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsNewsLetterAccepted { get; set; }
        public int HomeCountryId { get; set; }
        public string? AddressOne { get; set; }
        public int? AddressOneCountryId { get; set; }
        public int? AddressOneStateId { get; set; }
        public int? AddressOneCityId { get; set; }
        public int? AddressOneTypeId { get; set; }
        public string? AddressOnePostalCode { get; set; }
        public string? AddressTwo { get; set; }
        public int? AddressTwoCountryId { get; set; }
        public int? AddressTwoStateId { get; set; }
        public int? AddressTwoCityId { get; set; }
        public int? AddressTwoTypeId { get; set; }
        public string? AddressTwoPostalCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual City? AddressOneCity { get; set; }
        public virtual Country? AddressOneCountry { get; set; }
        public virtual StateProvince? AddressOneState { get; set; }
        public virtual AddressType? AddressOneType { get; set; }
        public virtual City? AddressTwoCity { get; set; }
        public virtual Country? AddressTwoCountry { get; set; }
        public virtual StateProvince? AddressTwoState { get; set; }
        public virtual AddressType? AddressTwoType { get; set; }
        public virtual Country HomeCountry { get; set; } = null!;
        public virtual RequestsQueue? Task { get; set; }
    }
}
