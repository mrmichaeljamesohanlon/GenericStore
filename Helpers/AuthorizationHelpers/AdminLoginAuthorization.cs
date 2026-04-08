
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.AuthorizationHelpers
{
    public class AdminLoginAuthorization : ActionFilterAttribute
    {
        private readonly ISessionManager _sessionManag;
        public AdminLoginAuthorization(ISessionManager sessionManag)
        {
            _sessionManag = sessionManag;
        }

       

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserEntity? usr = new UserEntity();

            usr = _sessionManag.GetLoginUserFromSession();


            // --if user session null, then redirect to  login page
            if (usr == null || usr.UserId < 1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Authentication",
                    action = "Login",
                    area = ""
                }));

            }
            else
            {
                // Ensure menu and role-rights are available in session for sidebar rendering
                var mainMenu = filterContext.HttpContext.Session.GetString("MainMenu");
                if (string.IsNullOrEmpty(mainMenu))
                {
                    _sessionManag.SetMenusInSession();
                }

                var userRoleRights = filterContext.HttpContext.Session.GetString("UserRoleRights");
                if (string.IsNullOrEmpty(userRoleRights))
                {
                    _sessionManag.SetUserRightsInSession(usr.UserId);
                }
            }


            base.OnActionExecuting(filterContext);
        }
    }
}
