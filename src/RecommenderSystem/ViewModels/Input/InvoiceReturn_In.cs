using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels.Input
{
    public class ReturnQuantity
    {
        public int PackagingID { get; set; }
        public int Quantity { get; set; }
    }
    public class InvoiceReturn_In : VM_In
    {
        public List<ReturnQuantity> ReturnQuantity { get; set; }
        public int InvoiceID { get; set; }
        public int Discount { get; set; }
    }
}