using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Filters
{
    public class RequireSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (Controller)filterContext.Controller;
            SessionUser _user = controller.Session["User"] as SessionUser;
            Role role = controller.Session["Role"] as Role;
            if (_user != null)
            {
                if(role == null)
                controller.Session["Role"] = DBHelper.Get<Models.DBModels.Role>("Select * from [Users].[Role] ",$"Where ID = {_user.RoleID}", null);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action", "Login" }, { "controller", "Account" } });
            }
        }
    }
}