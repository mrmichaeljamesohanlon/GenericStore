using System;
using System.Collections.Generic;

namespace Entities.DBModels
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            AdminPanelNotifications = new HashSet<AdminPanelNotification>();
        }

        public int NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<AdminPanelNotification> AdminPanelNotifications { get; set; }
    }
}
