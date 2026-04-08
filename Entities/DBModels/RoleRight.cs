using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class RoleRight
    {
        public int RoleRightId { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public int EntityId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Right Right { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
