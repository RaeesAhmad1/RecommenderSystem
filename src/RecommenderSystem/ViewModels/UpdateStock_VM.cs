using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class UpdateStock_VM
    {
        public List<Product_LV> products { get; set; }
        public int CategoryID { get; set; }
        public UpdateStock_VM(List<Product_LV> products, int CategoryID)
        {
            this.CategoryID = CategoryID;
            this.products = products;
        }
    }
}