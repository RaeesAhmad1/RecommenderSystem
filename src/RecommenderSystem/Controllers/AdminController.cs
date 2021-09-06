using InventoryManagement.Models.Common;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    public class AdminController : PanelController
    {
        public ProductRepository ProductRepo { get; set; }
        public StockRepository StockRepo { get; set; }

        public AdminController()
        {
            this.ProductRepo = new ProductRepository();
            this.StockRepo = new StockRepository();
        }

        public ActionResult Dashboard()
        {
            return View(GetDashboardVM());
        }


        [NonAction]
        public Dashboard_Out_VM GetDashboardVM()
        {
            return new Dashboard_Out_VM
            {
                Top10SoldProducts = ProductRepo.GetTop10SoldProducts(),
                CashRecovery = StockRepo.CashRecovery(),
                DiscountGraph = StockRepo.DiscountGraph(),
                LowStock = StockRepo.LowStock(),
                PendingOrders = StockRepo.PendingOrders(),
                RevenueGraph = StockRepo.RevenueGraph(),
                SalesGraph = StockRepo.SalesGraph(),
                StockBalance = StockRepo.StockBalance(),
                StockCount = StockRepo.StockCount(),
                StockSales = StockRepo.StockSales()
            };
        }
    }
}