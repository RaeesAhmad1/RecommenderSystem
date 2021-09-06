using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Products")]
    [Table("Product_Packaging")]
    public class ProductPackaging
    {
        [DontUpdate]
        [DontInsert]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int PackagingID { get; set; }
        public int PackageSize { get; set; }
        public int Quantity { get; set; }
        public double PricePerPiece { get; set; }
        public double RetailPrice { get; set; }
        [DontLoad]
        [DontUpdate]
        [DontInsert]
        public string Reason { get; set; }


    }
}