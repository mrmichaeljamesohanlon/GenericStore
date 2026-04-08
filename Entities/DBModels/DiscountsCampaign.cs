using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class DiscountsCampaign
    {
        public DiscountsCampaign()
        {
            DiscountUsageHistories = new HashSet<DiscountUsageHistory>();
        }

        public int CampaignId { get; set; }
        public string MainTitle { get; set; } = null!;
        public string DiscountTitle { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime DisplayStartDate { get; set; }
        public DateTime DisplayEndDate { get; set; }
        public int? CoverPictureId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }
    }
}
