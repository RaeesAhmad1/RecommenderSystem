using System.Web.Mvc;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;

namespace InventoryManagement.Controllers
{
    public class CategoryController : PanelController
    {
        public CategoryController() : base()
        {
            this.CategoryRepo = new CategoryRepository();
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return View(CategoryRepo.GetList());
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (category.ID != 0)
            {
                CategoryRepo.Update(category);
                Notify("Success", "Successfully Updated", "Category Updated successfully", false,false,true);
            }
            else
            {
                CategoryRepo.Insert(category);
                Notify("Success", "Successfully Added", "Category Added successfully", false,false,true);
            }
            return RedirectToAction("Categories", "Category"); //PartialView("~/Views/Category/CategoryList.cshtml", CategoryRepo.GetList());
        }

        [HttpPost]
        public JsonResult GetCategory(int ID)
        {
            return Json(CategoryRepo.Get(ID));
        }


        [HttpPost]
        public ActionResult DeleteCategory(int CategoryID)
        {
            CategoryRepo.Delete(CategoryID);
            Notify("Success", "Successfully Deleted", "Category Deleted successfully");
            return PartialView("~/Views/Category/CategoryList.cshtml", CategoryRepo.GetList());
        }
        
    }
}