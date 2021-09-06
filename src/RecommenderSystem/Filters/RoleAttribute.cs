using InventoryManagement.Controllers;
using InventoryManagement.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Filters
{
    public class RoleAttribute : ActionFilterAttribute
    {
        public List<string> type { get; set; }
        public RoleAttribute(string type)
        {
            this.type = new List<string>
            {
                type
            };
        }
        public RoleAttribute(string[] type)
        {
            this.type = type.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (PanelController)filterContext.Controller;
            SessionUser user = controller.Session["User"] as SessionUser;
            if (user != null)
            {
                if (!this.type.Contains(user.Type))
                {
                    controller.Session.Clear();
                    if (controller.Request.Cookies["KMGQQMK"] != null)
                    {
                        var c = controller.Request.Cookies["KMGQQMK"];
                        c.Expires = DateTime.Now.AddDays(-1);
                        controller.Response.Cookies.Add(c);
                    }
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action", "FacilitySignin" }, { "controller", "Members" } });
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

    }
}