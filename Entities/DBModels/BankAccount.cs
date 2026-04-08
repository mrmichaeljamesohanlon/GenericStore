using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankAccount
    {
        public int BankAccountId { get; set; }
        public int BankMasterId { get; set; }
        public string BankBranchName { get; set; } = null!;
        public int UserId { get; set; }
        public string BankBranchCode { get; set; } = null!;
        public int AccountTypeId { get; set; }
        public string AccountNo { get; set; } = null!;
        public string AcountTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string? Iban { get; set; }
        public int? StateProvinceId { get; set; }
        public int? CityId { get; set; }
        public string? BranchAddress { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
