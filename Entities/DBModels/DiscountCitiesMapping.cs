using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountCitiesMapping
    {
        public int DiscountCityMappingId { get; set; }
        public int DiscountId { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Discount Discount { get; set; } = null!;
    }
}
