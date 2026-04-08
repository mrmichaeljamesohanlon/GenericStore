using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class AdminPanelNotification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int NotificationTypeId { get; set; }
        public bool IsRead { get; set; }
        public int? ReadBy { get; set; }
        public DateTime? ReadByDate { get; set; }
        public string? ClickUrl { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual NotificationType NotificationType { get; set; } = null!;
    }
}
