using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repository.IServices
{
    public interface INotificationsServicesDAL
    {
        Task<List<AdminPanelNotificationEntity>> GetAdminPanelNotificationsListDAL(AdminPanelNotificationEntity FormData);
        Task<string> MarkAllAdminNotificationsAsReadDAL(int UserID);
        Task<string> MarkOnlySelectedAdminNotificationsAsReadDAL(AdminPanelNotificationEntity FormData);
        Task<int> GetHeaderAdminUnreadNotificationsCountDAL();
    }
}
