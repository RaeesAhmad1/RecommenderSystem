using InventoryManagement.Models;
using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class AccountController : BaseController
    {
        public UserRepository UserRepo { get; set; }
        public RoleRepository RoleRepo { get; set; }
        public AccountController() : base()
        {
            this.RoleRepo = new RoleRepository();
            this.UserRepo = new UserRepository();
        }
        

        // GET: Account
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            RoleRepo.CreateDefaultRole();
           // InfoLog("Trying to login with IP : " + Request.UserHostAddress);
           var vm = new Login_VM {ReturnUrl = ReturnUrl};
           return View(vm);
        }

        [HttpPost]
        public ActionResult Login(Login_VM login)
        {
            SessionUser user = LoginHandler.GetUser(login.Email, login.Password, Request.UserHostAddress);
            if (user == null) return View();

            Session["User"] = user;
            if (!string.IsNullOrWhiteSpace(login.ReturnUrl))
                return Redirect(login.ReturnUrl);
            
            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public ActionResult Register(string ReturnUrl)
        {
            var vm = new Register_VM
            {
                ReturnUrl = ReturnUrl
            };
            return View(vm);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var roles = DBHelper.GetList<Role>();
                var role = roles.FirstOrDefault(x => x.Name != "Admin");
                user.RoleID = role.ID;
                UserRepo.Insert(user);
                if (!string.IsNullOrWhiteSpace(ReturnUrl)) 
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }


        public ActionResult LockScreen()
        {
            return View();
        }

        public ActionResult Logout(string ReturnUrl)
        {
            Session.Abandon();
            if (!string.IsNullOrWhiteSpace(ReturnUrl)) 
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}