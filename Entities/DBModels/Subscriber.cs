using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class Subscriber
    {
        public int SubscriptionId { get; set; }
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
