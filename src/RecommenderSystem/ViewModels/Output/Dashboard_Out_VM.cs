using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels.Output
{
    public class Dashboard_Out_VM : VM_Out
    {
        public List<Dictionary<string, string>> Top10SoldProducts { get; set; }
        public double StockBalance { get; set; }
        public double StockSales { get; set; }
        public int PendingOrders { get; set; }
        public string CashRecovery { get; set; }
        public List<Dictionary<string, string>> RevenueGraph { get; set; }
        public List<Dictionary<string, string>> DiscountGraph { get; set; }
        public List<Dictionary<string, string>> SalesGraph { get;  set; }
        public List<Dictionary<string, string>> LowStock { get;  set; }
        public string StockCount { get;  set; }
    }
}

