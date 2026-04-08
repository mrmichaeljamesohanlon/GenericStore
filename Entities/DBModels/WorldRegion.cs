using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class WorldRegion
    {
        public WorldRegion()
        {
            Countries = new HashSet<Country>();
        }

        public int WorldRegionId { get; set; }
        public string RegionName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public decimal? DisplaySeqNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
