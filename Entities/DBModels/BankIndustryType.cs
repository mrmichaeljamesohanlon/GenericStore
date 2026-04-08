using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankIndustryType
    {
        public BankIndustryType()
        {
            BankMasters = new HashSet<BankMaster>();
        }

        public int IndustryTypeId { get; set; }
        public string IndustryName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<BankMaster> BankMasters { get; set; }
    }
}
