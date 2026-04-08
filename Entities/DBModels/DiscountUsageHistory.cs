using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountUsageHistory
    {
        public int UsageId { get; set; }
        public int DiscountId { get; set; }
        public int? CampaignId { get; set; }
        public int? UsedBy { get; set; }
        public DateTime? UsageDate { get; set; }

        public virtual DiscountsCampaign? Campaign { get; set; }
        public virtual Discount Discount { get; set; } = null!;
        public virtual User? UsedByNavigation { get; set; }
    }
}
