using InventoryManagement.Filters;
using InventoryManagement.Hubs;
using InventoryManagement.Models.Common;
using InventoryManagement.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [RequireSession]
    [Validator]
    public class PanelController : BaseController
    {
        public SessionUser user { get { return Session["User"] as SessionUser; } set { } }
        public RoleAttribute role { get { return Session["User"] as RoleAttribute; } set { } }

        public UserRepository UserRepo { get; set; }
        public PanelController() : base()
        {
            this.UserRepo = new UserRepository();
        }

        public void TestingBellNotification()
        {
            BellNotifyAdmin(new NotificationMessage
            {
                Code = "200",
                Description = "This is test db message",
                Icon = "something",
                IsAjaxMessage = true,
                IsViewMessage = true,
                IsRedirectMessage = false,
                MessageType = "Success",
                NotificationType = "PanelNotification",
                Title = "Test Notification",
                URL = "",
                UserId = 1
            });
        }

        public void BellNotifyAdmin(NotificationMessage message)
        {
            message.UserType = "Admin";
            MessagesRepository.Insert(message);
            string Username = UserRepo.Get(message.UserId).Username;
            Notify(Username, message);
        }


        public void Notify(string Type, string Title, string Description, bool IsAjaxMessage = true, bool IsViewMessage = true, bool IsRedirectMessage = false)
        {
            Notify(new NotificationMessage {
                MessageType = Type,
                Title = Title,
                Description = Description,
                IsAjaxMessage = IsAjaxMessage,
                IsViewMessage = IsViewMessage,
                IsRedirectMessage = IsRedirectMessage
            });
        }
        public override void Notify(NotificationMessage message)
        {
            Notify(user.Username, message);
        }

        public void Notify(string Username, NotificationMessage message)
        {
            if (message.IsAjaxMessage)
            {
                Context.Clients.Group(Username).Notify(message);
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


        public JsonResult BellNotificationListener()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return Json(_messageRepository.BellNotificationListner(user.Id, "Admin"));
        }

        public JsonResult GetBellNotifications()
        {
            return Json(MessagesRepository.GetBellUnReadNotificationMessages(user.Id, "Admin"));
        }

        //public ActionResult Notifications()
        //{
        //    return View(new NotificationViewModel(user.Id, "Admin"));
        //}


        public JsonResult MarkAllAsRead()
        {
            MessagesRepository.MarkAllRead(user.Id, "Admin");
            return Json(true);
        }
        public JsonResult MarkRead(string MessageID)
        {
            MessagesRepository.MarkRead(MessageID);
            return Json(true);
        }

    }
}