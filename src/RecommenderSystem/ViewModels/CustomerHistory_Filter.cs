using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class CustomerHistory_Filter
    {
        public int CustomerID { get; set; }
        public DateTime? PaymentDateStart { get; set; }
        public DateTime? PaymentDateEnd { get; set; }
        public DateTime? InvoiceDateStart { get; set; }
        public DateTime? InvoiceDateEnd { get; set; }
        public DateTime? OrderDateStart { get; set; }
        public DateTime? OrderDateEnd { get; set; }
    }
}