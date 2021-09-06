using InventoryManagement.Models.DBModels;
using System;

namespace InventoryManagement.ViewModels
{
    public class Product_LV : Product
    {
        public string CategoryName { get; set; }
        public string Unit { get; set; }
        public int PackagingID { get; set; }
        public int ProductPackagingID { get; set; }
        public int PackageSize { get; set; }
        public int Quantity { get; set; }
        public double PricePerPiece { get; set; }
        public double RetailPrice { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }

    }

    public class Product_Submit : Product_LV
    {
        public Product_Submit() : base()
        {
        }
        
        public int SoldQuantity { get; set; }
        public double AmountPayable { get; set; }
        public double RetailPrice { get; set; }
    }

}