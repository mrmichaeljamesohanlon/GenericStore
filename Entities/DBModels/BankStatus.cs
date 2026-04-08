using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class BankStatus
    {
        public BankStatus()
        {
            BankMasters = new HashSet<BankMaster>();
        }

        public int BankStatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModfiedBy { get; set; }

        public virtual ICollection<BankMaster> BankMasters { get; set; }
    }
}
