using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class Packaging_LV : Packaging
    {
        [DontLoad]
        public int TotalProducts { get; set; }
    }
}