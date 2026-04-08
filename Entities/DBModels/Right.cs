using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Right
    {
        public Right()
        {
            RoleRights = new HashSet<RoleRight>();
        }

        public int RightId { get; set; }
        public string RightName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<RoleRight> RoleRights { get; set; }
    }
}
