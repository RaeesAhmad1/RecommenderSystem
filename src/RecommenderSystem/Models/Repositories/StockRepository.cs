using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class StockRepository : DBRepository
    {
        public double StockBalance()
        {
            return double.Parse(DBHelper.QueryColumn("SELECT ISNULL(SUM(Quantity* PricePerPiece), 0) StockBalance From Products.Product_Packaging", null).First());
        }

        public double StockSales()
        {
            return double.Parse(DBHelper.QueryColumn("Select ISNULL(SUM(Quantity),0) StockQuantity from Sales.Invoice_Product_Packaging", null).First());
        }

        public int PendingOrders()
        {
            return int.Parse(DBHelper.QueryColumn($@"
SELECT COUNT(*) FROM Sales.Invoice Where PaidDate IS NULL

", null).First());
        }

        public string CashRecovery()
        {
            return DBHelper.QueryColumn($@"
SELECT (
SELECT ISNULL(SUM(AmountPayable),0) FROM Sales.Invoice
) - (
SELECT ISNULL(SUM(Amount),0) FROM Payments.Payment
) as CashRecovery

", null).First();
        }


        public List<Dictionary<string, string>> RevenueGraph()
        {
            return DBHelper.QueryList(
                @"
SELECT
DateName( Month , DateAdd( month , E.Month , 0 ) - 1 ) as Month
, E.Expense, ISNULL(P.Earning,0) as Earning, (ISNULL(Earning, 0)-Expense) as Revenue FROM
(
Select MONTH(Expense.ExpenseDate) as Month,
SUM(Expense.Amount) as Expense
from Payments.Expense
Where ExpenseDate > DATEADD(MONTH, -6, GETDATE())
Group by MONTH(ExpenseDate)
) E
LEFT JOIN (
Select SUM(P.Amount) as Earning, MONTH(P.TimesTamp) as Month from Payments.Payment P
LEFT JOIN Payments.Payment_Cheque PC ON PC.PaymentID = P.ID 
LEFT JOIN Payments.Cheque C ON C.ID = PC.ChequeID
Where P.TimeStamp > DATEADD(MONTH, -6, GETDATE()) AND C.Cleared = 1 OR C.Cleared IS NULL
GROUP BY MONTH(P.TimeStamp)
) P
ON E.Month = P.Month

");
        }

        public List<Dictionary<string, string>> DiscountGraph()
        {
            return DBHelper.QueryList($@"
SELECT D.Discount, S.Quantity, D.Month, D.DiscountMoney FROM (
Select AVG(Discount) as Discount,
SUM(SubTotal-AmountPayable) as DiscountMoney,
Month(TimeStamp) as Month from Sales.Invoice
Where  TimeStamp > DATEADD(MONTH, -6, GETDATE())
GROUP BY MONTH(TimeStamp)
) D INNER JOIN (
Select SUM(Quantity) as Quantity , MONTH(TimeStamp) as Month from Sales.Invoice SI INNER JOIN Sales.Invoice_Product_Packaging SIP ON SI.ID = SIP.InvoiceID
Group By Month(SI.TimeStamp)
) S ON S.Month = D.Month
");
        }
        public List<Dictionary<string, string>> SalesGraph()
        {
            return DBHelper.QueryList($@"
Select SUM(Quantity) as Quantity ,  DateName( Month , DateAdd( month , MONTH(TimeStamp) , 0 ) - 1 ) as Month from Sales.Invoice SI INNER JOIN Sales.Invoice_Product_Packaging SIP ON SI.ID = SIP.InvoiceID
Group By Month(SI.TimeStamp)
");
        }
        public List<Dictionary<string, string>> LowStock()
        {
            return DBHelper.QueryList($@"
Select P.Name, P.SKU, P.MU, PP.Quantity,PP.RetailPrice FROM Products.Product P INNER JOIN Products.Product_Packaging PP ON P.ID = PP.ProductID Where PP.Quantity < PP.NotifyQuantity
");
        }

        public string StockCount()
        {
            return DBHelper.QueryColumn($@"
Select SUM(Quantity) as StockCount from Products.Product_Packaging
", null).First();
        }


    }
}