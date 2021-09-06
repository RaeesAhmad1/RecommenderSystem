using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class ProductPackaging_LV : ProductPackaging
    {
        public string PackagingName { get; set; }
        public string Unit { get; set; }

    }


    public class AddStock_LV
    {
        public Product Product { get; set; }
        public List<ProductPackaging_LV> Packaging {get;set;}

        public AddStock_LV()
        {
            this.Product =  new Product();
            this.Packaging = new List<ProductPackaging_LV>();
        }

        public AddStock_LV(Product Products, List<ProductPackaging_LV> Packaging)
        {
            this.Packaging = Packaging;
            this.Product = Product;
        }

    }

    public class AddStock_VM
    {
        public List<AddStock_LV> ProductList { get; set; }
        public AddStock_VM( List<AddStock_LV> ProductList)
        {
               this.ProductList = ProductList;
        }
    }


}