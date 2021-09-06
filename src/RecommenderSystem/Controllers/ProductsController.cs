using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System
{

}


namespace InventoryManagement.ViewModels
{
    public class Products_VM
    {
        public List<Product_LV> Products { get; set; }
        public List<Category_LV> Categories { get; set; }
        public Packaging Package { get; set; }
        public Products_VM(List<Product_LV> Products, List<Category_LV> categories, Packaging package)
        {
            this.Products = Products;
            this.Categories = categories;
            this.Package = package;
        }
    }
}

namespace InventoryManagement.Controllers
{
    public class WishListProduct_VM
    {
        public  List<Product_LV> Products{get; set; }
    }

    public class ProductsController : PanelController
    {
        public ProductRepository ProductRepo { get; set; }
        public UnitRepository UnitRepo { get; set; }
        public ProductPackagingRepository ProductPackagingRepo { get; set; }
        public ProductsController()
        {
            ProductRepo = new ProductRepository();
            CategoryRepo = new CategoryRepository();
            this.UnitRepo = new UnitRepository();
            this.PackagingRepo = new PackagingRepository();
            this.ProductPackagingRepo = new ProductPackagingRepository();
        }
        // GET: Products

        public ActionResult Products(int CategoryID=0, int UnitID=0, int PackingID = 0)
        {
            List<Product_LV> products = ProductRepo.GetList(CategoryID, UnitID, PackingID);
            if (products != null && products.Count != 0)
            {
                return View(new Products_VM(products, CategoryRepo.GetList(), PackagingRepo.Get(products.First().PackagingID)));
            }
            return View(new Products_VM(new List<Product_LV>(), new List<Category_LV>(), new Packaging()));
        }


        public ActionResult WishListProducts()
        {
            List<Product_LV> products = ProductRepo.GetList(new ProductFilter
            {
                UserId = user.Id
            });

            var vm = new WishListProduct_VM
            {
                Products = products
            };
            return View(vm);
        }


        [HttpGet]
        public ActionResult AddProduct(int ID = 0)
        {
            AddProduct_VM vm = new AddProduct_VM
            {
                product = (ID != 0) ? ProductRepo.Get(ID) : new Product(),
                ProductPacking = ProductPackagingRepo.GetList(ID),
                Units = UnitRepo.GetList(),
                packaging = PackagingRepo.GetList(),
                categoryList = CategoryRepo.GetList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddProduct(AddProduct_VM vm)
        {

            SessionUser user = Session["User"] as SessionUser;
            if (vm.ProductPacking != null)
            {
                vm.ProductPacking = vm.ProductPacking.Where(x => x.PackagingID != 0).ToList();
            }
            if (vm.ImageFile != null)
            {
                string fn = Path.GetFileName(vm.ImageFile.FileName);
                string fileExtension = fn.Remove(0, fn.IndexOf('.') + 1);
                fn = vm.product.Name + "." + fileExtension;
                string SaveLocation = "~/Images/Products/";
                vm.product.ImagePath = Path.Combine(SaveLocation, fn);
                string FilePath = Server.MapPath(SaveLocation);
                vm.ImageFile.SaveAs(Path.Combine(FilePath, fn));
            }

            if (vm.product.ID == 0)
            {
                if (ProductRepo.Insert(vm, user.Id))
                {
                    Notify("Success", "Successfully added", "Product Added Successfully", false, false, true);
                }
                else
                {
                    Notify("Error", "Technical Error", "Unable to add product due to technical issues", false, false, true);
                }
            }
            else
            {
                if (ProductRepo.Update(vm, user.Id))
                {
                    Notify("Success", "Successfully Updated", "Product Added Successfully", false, false, true);
                }
                else
                {
                    Notify("Error", "Technical Error", "Unable to add product due to technical issues", false, false, true);
                }
            }
           
            return RedirectToAction("AddProduct", "Products");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductID)
        {
            if (ProductRepo.Delete(ProductID))
            {
                Notify("Success", "Successfully Deleted", "Product Deleted Successfully", false, true, true);
            }
            else
            {
                Notify("Error", "Technical Error", "Unable to delete Product due to technical issues", false, true, true);
            }
            return RedirectToAction("Products", "Products");

        }


        [HttpGet]
        public JsonResult GetPackaging(int ProductID)
        {
            return Json(PackagingRepo.GetProductPackaging(ProductID), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddStocK() //CategoryID
        {
            List<AddStock_LV> StockList = new List<AddStock_LV>();
            List<Product_LV> products = new List<Product_LV>();
            products = ProductRepo.GetList();
            if (products != null && products.Count > 0)
            {
                products = products.GroupBy(p => p.ID).Select(g => g.First()).ToList();
            }

            foreach (var element in products)
            {
                AddStock_LV stockitem = new AddStock_LV();
                stockitem.Product = element;
                stockitem.Packaging = ProductPackagingRepo.GetPackaginListView(element.ID);
                StockList.Add(stockitem);
            }

            return View(new AddStock_VM(StockList));
        }


        [HttpPost]
        public ActionResult AddStock( List<ProductPackaging> PackagingList)
        {
            SessionUser user = Session["User"] as SessionUser;
            ProductPackagingRepo.UpdateStock(PackagingList, user.Id);
            Notify("Success", "Successfully Added", "Stock has been added successfully", false,false,true);
            return RedirectToAction("AddStock", "Products");
        }


        [HttpPost]
        public ActionResult ReturnStock(List<ProductPackaging> PackagingList)
        {
            SessionUser user = Session["User"] as SessionUser;
            ProductPackagingRepo.ReturnStock(PackagingList, user.Id);
            Notify("Success", "Successfully Returned", "Stock has been returned successfully", false, false, true);
            return RedirectToAction("ReturnStock", "Products");
        }



        [HttpGet]
        public ActionResult Stock()
        {
            return View();
        }


        [HttpGet]
        public ActionResult UseStock() //CategoryID
        {
            List<AddStock_LV> StockList = new List<AddStock_LV>();
            List<Product_LV> products = new List<Product_LV>();
            products = ProductRepo.GetList(RawMeterial : true);
            if (products != null && products.Count > 0)
            {
                products = products.GroupBy(p => p.ID).Select(g => g.First()).ToList();
            }

            foreach (var element in products)
            {
                AddStock_LV stockitem = new AddStock_LV();
                stockitem.Product = element;
                stockitem.Packaging = ProductPackagingRepo.GetPackaginListView(element.ID);
                StockList.Add(stockitem);
            }

            return View(new AddStock_VM(StockList));
        }


        //[HttpPost]
        //public ActionResult UseStock(List<ProductPackaging> PackagingList, int CategoryID)
        //{
        //    ProductPackagingRepo.UseStock(PackagingList);
        //    Notify("Success", "Successfully Added", "Stock has been added successfully", false, false, true);
        //    return RedirectToAction("UseStock", "Products", new { ID = CategoryID });
        //}


        [HttpGet]
        public ActionResult ReturnStock()
        {
            List<AddStock_LV> StockList = new List<AddStock_LV>();
            List<Product_LV> products = new List<Product_LV>();
            products = ProductRepo.GetList();
            if (products != null && products.Count > 0)
            {
                products = products.GroupBy(p => p.ID).Select(g => g.First()).ToList();
            }

            foreach (var element in products)
            {
                AddStock_LV stockitem = new AddStock_LV();
                stockitem.Product = element;
                stockitem.Packaging = ProductPackagingRepo.GetPackaginListView(element.ID);
                StockList.Add(stockitem);
            }
            return View(new AddStock_VM(StockList));
        }


        public JsonResult DeleteProductPackaging(int ProductID, int PackagingID)
        {
            return Json(ProductPackagingRepo.Delete(ProductID, PackagingID));
        }
        
  


        public ActionResult Packaging()
        {
            return View(PackagingRepo.GetList());
        }

        public ActionResult AddPackaging(Packaging Packaging)
        {
            if (Packaging.ID == 0)
            {
                PackagingRepo.Insert(Packaging);
                Notify("Success", "Successfully added", "Packaging added successfully", false, false, true);
            }
            else
            {
                PackagingRepo.Update(Packaging);
                Notify("Success", "Successfully Updated", "Packaging Updated successfully", false, false, true);

            }
            return RedirectToAction("Packaging", "Products");
        }


        public JsonResult GetPackagingDetails(int PackagingId)
        {
            return Json(PackagingRepo.Get(PackagingId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePackaging(int PackagingId)
        {
            PackagingRepo.Delete(PackagingId);
            Notify("Success", "Successfully Deleted", "Packaging Deleted successfully", false, false, true);
            return RedirectToAction("Packaging", "Products");
        }
    }
}