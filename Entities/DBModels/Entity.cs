using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Entity
    {
        public Entity()
        {
            ScrnsLocalizations = new HashSet<ScrnsLocalization>();
        }

        public int EntityId { get; set; }
        public string EntityName { get; set; } = null!;
        public decimal DataVersion { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int AppModuleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual AppModule AppModule { get; set; } = null!;
        public virtual EntityType EntityType { get; set; } = null!;
        public virtual ICollection<ScrnsLocalization> ScrnsLocalizations { get; set; }
    }
}
