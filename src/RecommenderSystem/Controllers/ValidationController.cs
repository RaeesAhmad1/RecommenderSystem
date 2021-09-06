using InventoryManagement.Models.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult IsAvailableMobileNo(string mobileNo, int Id=0)
        {
            if (!IsMobileNoRegistered(mobileNo, Id))
                return Json(true, JsonRequestBehavior.AllowGet);
            var suggestedUid = string.Format(CultureInfo.InvariantCulture, "{0} is already registered with another account and it's blocked. Please contact support center to un-block it.", mobileNo);
            return Json(suggestedUid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAvailableEmail(string email, int Id=0)
        {
            if (!IsEmailRegistered(email, Id))
                return Json(true, JsonRequestBehavior.AllowGet);
            var suggestedUid = string.Format(CultureInfo.InvariantCulture, "{0} is already registered. Try {1}",
                email, "<a href='" + Url.Action("Login", "User") + "'>Login</a><br />");
            return Json(suggestedUid, JsonRequestBehavior.AllowGet);
        }



        public JsonResult IsNotAvailableEmail(string email)
        {
            if (IsEmailRegistered(email, 0))
                return Json(true, JsonRequestBehavior.AllowGet);
            var suggestedUid = string.Format(CultureInfo.InvariantCulture, "{0} is not registered. Try {1}", email, "<a href='" + Url.Action("Register", "Home") + ">Register</a><br />");
            return Json(suggestedUid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPasswordInCorrect(string email, string password)
        {
            if (IsPasswordIncorrect(email, password))
                return Json(true, JsonRequestBehavior.AllowGet);
            var suggestedUid = string.Format(CultureInfo.InvariantCulture,
                "Your account or password is incorrect. If you don't remember your password, {0}",
                "<a href='/Home/ForgotPassword?Type=User'>reset it now.</a><br />");
            return Json(suggestedUid, JsonRequestBehavior.AllowGet);
        }

     
        [NonAction]
        public bool IsEmailRegistered(string email, int Id)
        {
            DBHelper db = new DBHelper();
            db.values.Clear();
            db.values.Add("@Email", email);
            string query = "Select count(*) from[Users].[User] where Email = @Email";
            if (Id != 0)
            {
                db.values.Add("@ID", Id.ToString());
                query += " AND Id != @ID";
            }
            return DBHelper.GetScaler(query, db.values) >0 ;
        }

        [NonAction]
        public bool IsMobileNoRegistered(string mobileNo, int Id)
        {
            DBHelper db = new DBHelper();
            db.values.Add("@MobileNo", mobileNo);
            string query = "Select count(*) from Users.[User] where MobileNo = @MobileNo";
            if (Id != 0)
            {
                db.values.Add("@ID", Id.ToString());
                query += " AND Id != @ID";
            }
            return DBHelper.GetScaler(query, db.values ) > 0;
        }

        [NonAction]
        public bool IsPasswordIncorrect(string email, string password)
        {
            DBHelper db = new DBHelper();
            db.values.Clear();
            db.values.Add("@Email", email);
            db.values.Add("@Password", Security.Encrypt( password));
            return DBHelper.GetScaler("Select count(*) from Users.[User] where Email = @Email and Password = @Password", db.values) > 0;
        }


        
        [AllowAnonymous]
        [HttpPost]
        public JsonResult IsProductNameAvailable([Bind(Prefix = "Product.Name")]string Name, [Bind(Prefix = "Product.ID")]int ID =0)
        {
           DBHelper db = new DBHelper();
            db.values.Add("@ProductName", Name);
            string query = "SELECT COUNT(*) FROM Products.Product Where Name = @ProductName ";
            if (ID != 0)
            {
                query += " AND ID != @ID";
                db.values.Add("@ID", ID.ToString());
            }
            return Json(!(DBHelper.GetScaler(query, db.values) > 0));
        }


        public JsonResult AvailableProductSKU(string SKU, int ID)
        {
            DBHelper db = new DBHelper();
            db.values.Add("@SKU", SKU);
            string query = "SELECT COUNT(*) FROM Products.Product Where SKU = @SKU ";
            if (ID != 0)
            {
                query += " AND ID != @ID";
                db.values.Add("@ID", ID.ToString());
            }
            return Json(!(DBHelper.GetScaler(query, db.values) > 0));
        }

    }
}