using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class SellProduct_VM
    {
        public List<Product_LV> ProductList { get; set; }
        public int CategoryID { get; set; }
        public SellProduct_VM(List<Product_LV> ProductList, int CategoryID)
        {
            this.ProductList = ProductList;
            this.CategoryID = CategoryID;
        }
    }
}