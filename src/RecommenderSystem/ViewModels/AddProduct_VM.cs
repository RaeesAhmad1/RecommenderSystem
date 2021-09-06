using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class AddProduct_VM
    {
        public Product product { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public List<Unit> Units { get; set; }
        public List<Packaging_LV> packaging { get; set; }
        public List<Category_LV> categoryList { get; set; }
        public List<ProductPackaging> ProductPacking { get; set; }
        public int CategoryID { get; set; }
    }
}