using Entities.CommonModels;
using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class NotificationsModel
    {
        public List<AdminPanelNotificationEntity>? AdminPanelNotificationsList { get; set; }
        public List<LanguageEntity>? LanguagesList { get; set; }

        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
