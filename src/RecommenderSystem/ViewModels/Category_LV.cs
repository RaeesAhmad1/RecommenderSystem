using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class Category_LV : Category
    {
        public int TotalProducts { get; set; }
    }
}