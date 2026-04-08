using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankMaster
    {
        public int BankMasterId { get; set; }
        public string BankName { get; set; } = null!;
        public int? IndustryTypeId { get; set; }
        public string? BankCode { get; set; }
        public string? SwiftCode { get; set; }
        public int BankStatusId { get; set; }
        public int CountryId { get; set; }
        public int? AccountNoMinLength { get; set; }
        public int? AccountNoMaxLength { get; set; }
        public string? BankRegistNo { get; set; }
        public DateTime? RegistDate { get; set; }
        public string? WebUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual BankStatus BankStatus { get; set; } = null!;
        public virtual BankIndustryType? IndustryType { get; set; }
    }
}
