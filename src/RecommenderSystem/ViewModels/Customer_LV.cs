using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class Customer_LV : Customer
    {

    }

    public class Customer_VM
    {
        public Dictionary<string, string> InvoicesAndTheirAmount { get; set; }
    }

}