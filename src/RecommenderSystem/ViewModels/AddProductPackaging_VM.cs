using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class AddProductPackaging_VM
    {
        public List<Product_LV> products { get; set; }
        public List<Packaging_LV> packaging { get; set; }
        public List<Packaging> PackagingSize { get; set; }
        public int SelectedProduct { get; set; }
        public int SelectedPacking { get; set; }
        public int ProductID { get; set; }
    }
}