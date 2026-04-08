using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class EntityType
    {
        public EntityType()
        {
            Entities = new HashSet<Entity>();
        }

        public int EntityTypeId { get; set; }
        public string EntityTypeName { get; set; } = null!;

        public virtual ICollection<Entity> Entities { get; set; }
    }
}
