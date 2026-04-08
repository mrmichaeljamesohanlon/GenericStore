using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class WeightUnit
    {
        public WeightUnit()
        {
            AttrWeights = new HashSet<AttrWeight>();
        }

        public int WeightUnitId { get; set; }
        public string WeightUnitName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<AttrWeight> AttrWeights { get; set; }
    }
}
