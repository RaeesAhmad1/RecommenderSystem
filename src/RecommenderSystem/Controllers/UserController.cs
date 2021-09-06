using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class UserController : PanelController
    {
        public RoleRepository RoleRepo { get; set; }
        public UserController()
        {
            this.RoleRepo = new RoleRepository();
        }
      
        [HttpGet]
        public ActionResult UserList()
        {
            return View(UserRepo.GetUsers());
        }

        [HttpGet]
        public ActionResult AddUser(int? ID)
        {
            ViewBag.Roles = RoleRepo.GetList();
            if (ID == null)
            {
                return View(new User());
            }
            else
            {
                User user = UserRepo.Get((int)ID);
                user.Password = Security.Decrypt(user.Password);
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (user.Id == 0)
            {
                UserRepo.Insert(user);
            }
            else
            {
                UserRepo.Update(user);
            }
            return RedirectToAction("AddUser", "User");
        }

        public JsonResult DeleteUser(int UserID)
        {
            return Json(UserRepo.Delete(UserID));
        }
    }
}