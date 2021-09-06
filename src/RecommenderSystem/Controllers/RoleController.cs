using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class RoleController : PanelController
    {
        public RoleRepository RoleRepo { get; set; }
        public RoleController()
        {
            this.RoleRepo = new RoleRepository();
        }


        [HttpGet]
        public ActionResult RoleList()
        {
            return View(RoleRepo.GetList());
        }

        [HttpGet]
        public ActionResult AddRole(int? ID)
        {
            if (ID == null)
            {
                return View(new Models.DBModels.Role());
            }
            else
            {
                return View(RoleRepo.Get((int)ID));
            }

        }

        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            if (role.ID == 0)
            {
                RoleRepo.Insert(role);
            }
            else
            {
                RoleRepo.Update(role);
            }
            return RedirectToAction("AddRole", "Role");
        }

        [HttpPost]
        public JsonResult DeleteRole(int ID)
        {
            return Json(RoleRepo.Delete(ID));
        }
    }
}