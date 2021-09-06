using InventoryManagement.Filters;
using InventoryManagement.Hubs;
using InventoryManagement.Models.Common;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace InventoryManagement.Controllers
{
    //[ErrorHandler]
    public class BaseController : Controller
    {
        protected IHubContext Context { get; set; }
        public WebSettingsRepository WebSettingsRepo { get; set; }
        public CategoryRepository CategoryRepo { get; set; }
        public UnitRepository unitRepo { get; set; }
        public PackagingRepository PackagingRepo { get; set; }
        private readonly log4net.ILog Logger;

    public BaseController()
        {
            this.WebSettingsRepo = new WebSettingsRepository();
            this.CategoryRepo = new CategoryRepository();
            this.unitRepo = new UnitRepository();
            this.PackagingRepo = new PackagingRepository();
            this.Context = GlobalHost.ConnectionManager.GetHubContext<Notification>();
            this.Logger = log4net.LogManager.GetLogger("BaseController");
            ViewBag.Layout = new Layout_VM(WebSettingsRepo.Get(), CategoryRepo.GetList(), PackagingRepo.GetList());
        }

        [NonAction]
        public virtual void ErrorLog(string text)
        {
            Logger.Error(Request.UserHostAddress + " " + text);

        }

        [NonAction]
        public void DebugLog(string text)
        {
           Logger.Debug(Request.UserHostAddress + " " + text);
        }

        [NonAction]
        public void InfoLog(string text)
        {
            Logger.Info(Request.UserHostAddress + " " + text);
        }

        [NonAction]
        public void WarningLog(string text)
        {
            Logger.Warn(Request.UserHostAddress + " " + text);
        }


        public ActionResult DefaultSettings()
        {
            new RoleRepository().CreateDefaultRole();
            new WebSettingsRepository().DefaultSettings();
            return RedirectToAction("Login", "Account");
        }


        public virtual void Notify(NotificationMessage message)
        {
            if (message.IsAjaxMessage)
            {
                Context.Clients.Group(Request.UserHostAddress).Notify(message);
            }
            if (message.IsRedirectMessage)
            {
                TempData["NotificationMessage"] = message;
            }
            if (message.IsViewMessage)
            {
                ViewBag.NotificationMessage = message;
            }
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }
}