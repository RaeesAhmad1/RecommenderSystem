using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class Layout_VM
    {
        public WebSettings wb { get; set; }
        public List<Category_LV> categories { get; set; }
        public List<Packaging_LV> packaging { get; set; }
        public Layout_VM(WebSettings wb, List<Category_LV> categories, List<Packaging_LV> packaging)
        {
            this.wb = wb;
            this.categories = categories;
            this.packaging = packaging;
        }
    }
}