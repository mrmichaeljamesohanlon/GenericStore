using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Role
    {
        public Role()
        {
            RoleRights = new HashSet<RoleRight>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<RoleRight> RoleRights { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
