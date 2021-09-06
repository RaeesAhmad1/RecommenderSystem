using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class CustomerController : PanelController
    {
        public CustomerRepository CustomerRepo { get; set; }
        public CustomerController()
        {
            this.CustomerRepo = new CustomerRepository();
        }


        [HttpGet]
        public ActionResult CustomerList()
        {
            return View(CustomerRepo.GetList());
        }


        [HttpPost]
        public JsonResult GetUnClearedInvoices(int CustomerID)
        {
            return Json(CustomerRepo.GetUnClearedInvoices(CustomerID));
        }

        [HttpGet]
        public ActionResult AddCustomer(int? ID)
        {
            if (ID == null)
            {
                return View(new Models.DBModels.Customer());
            }
            else
            {
                return View(CustomerRepo.Get((int)ID));
            }

        }

        [HttpGet]
        public ActionResult CustomerHistory(int ID)
        {
            return View();
        }



        [HttpPost]
        public ActionResult AddCustomer(Customer Customer)
        {
            if (Customer.ID == 0)
            {
                if (CustomerRepo.Insert(Customer))
                {
                    Notify("Success", "Sucessfully Added", "Customer Added Successfully", false, false, true);
                }
                else
                {
                    Notify("Error", "Technical Error", "Difficulties in Adding Customer. Please contact support", false, false, true);
                }
            }
            else
            {
                if (CustomerRepo.Update(Customer))
                {
                    Notify("Success", "Sucessfully Updated", "Customer Updated Successfully", false, false, true);
                }
                else
                {
                    Notify("Error", "Technical Error", "Difficulties in Adding Customer. Please contact support", false, false, true);
                }
            }
            return RedirectToAction("AddCustomer", "Customer");
        }

        [HttpPost]
        public JsonResult DeleteCustomer(int ID)
        {
            if (CustomerRepo.Delete(ID))
            {
                Notify("Sucess", "Successfully Deleted", "Customer Deleted Successfully", true, true, true);
                return Json(true);
            }
            else
            {
                Notify("Error", "Technical Error", "Unable to delete Customer. Please contact Support");
                return Json(false);
            }
          
        }


        [HttpPost]
        public JsonResult GetCustomerHistory(CustomerHistory_Filter filter)
        {
            return Json(CustomerRepo.GetFilteredHistory(filter));
        }

    }



}
