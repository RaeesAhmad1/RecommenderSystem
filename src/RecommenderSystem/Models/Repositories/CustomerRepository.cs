using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Models.Repositories
{
    public class CustomerRepository : DBRepository
    {

        public bool Insert(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.DisplayName))
            {
                customer.DisplayName = customer.FirstName +" "+  customer.LastName;
            }
            return DBHelper.Insert(customer);
        }
        public bool Update(Customer customer)
        {
            db.values.Clear();
            db.values.Add("@CustomerID", customer.ID.ToString());
            return DBHelper.Update(customer, "Where ID = @CustomerID", db.values);
        }

        public List<Dictionary<string,string>> GetUnClearedInvoices(int customerID)
        {
            string query = $@"
SELECT * FROM
(
Select Invoice.ID, Invoice.AmountPayable, Invoice.CustomerID, ISNULL(SUM(Payment.Amount),0) as PaidAmount 
from Sales.Invoice 
LEFT JOIN Payments.Payment ON Payment.InvoiceID = Invoice.ID
Group by Invoice.ID , Invoice.AmountPayable, Invoice.CustomerID
) 
InvoiceList
Where (PaidAmount IS NULL OR PaidAmount < AmountPayable) AND CustomerID = {customerID}
";
            return DBHelper.QueryList(query);
        }

        public bool Delete(int CustomerID)
        {
            string query = $@"
DELETE SIP FROM Sales.Invoice SI INNER JOIN Sales.Invoice_Product_Packaging SIP ON SIP.ID = SI.ID
Where SI.CustomerID = @CustomerID;
DELETE P FROM Sales.Invoice SI INNER JOIN Payments.Payment P ON P.InvoiceID = SI.ID Where SI.CustomerID = @CustomerID;
DELETE PP FROM Payments.Cheque C INNER JOIN Payments.Payment_Cheque PP ON PP.ChequeID = C.ID Where C.CustomerID = @CustomerID;
DELETE FROM Payments.Cheque Where CustomerID =  @CustomerID;
DELETE FROM Sales.Customer Where ID = @CustomerID;
";

            db.values.Clear();
            db.values.Add("@CustomerID", CustomerID.ToString());
            return DBHelper.ExecuteQuery(query, db.values);
        }

        public Customer Get(int iD)
        {
            db.values.Clear();
            db.values.Add("@ID", iD.ToString());
            return DBHelper.Get<Customer>("","Where ID = @ID",db.values);
        }

        public List<Customer> GetList()
        {
            return DBHelper.GetList<Customer>()    ;
        }

        public List<Dictionary<string,string>> GetFilteredHistory(CustomerHistory_Filter filter)
        {

            string query = $@"
SELECT I.TimeStamp, I.AmountPayable, C.DisplayName as CustomerName, 'Credit' as [Transaction], 'Invoice' as Type FROM Sales.Invoice I
INNER JOIN Sales.Customer C ON C.ID = I.CustomerID
UNION
SELECT P.TimeStamp, P.Amount as AmountPayble, C.DisplayName as CustomerName, 'Debit' as [Transaction], PaymentMethod as Type FROM Payments.Payment P
INNER JOIN Sales.Customer C ON P.CustomerID = C.ID
Where C.ID = {filter.CustomerID}
 ";

            if (filter.OrderDateStart != null)
            {
                query += $@"
AND SI.OrderDate >= '{((DateTime)filter.OrderDateStart).ToString("yyyy-MM-dd")}'
";
            }

            if (filter.OrderDateEnd != null)
            {
                query += $@"
AND SI.OrderDate <= '{((DateTime)filter.OrderDateEnd).ToString("yyyy-MM-dd")} 23:59:59'
";
            }
            if (filter.InvoiceDateStart != null)
            {
                query += $@"
AND SI.TimeStamp >= '{((DateTime)filter.InvoiceDateStart).ToString("yyyy-MM-dd")}'
";
            }
            if (filter.InvoiceDateEnd != null)
            {
                query += $@"
AND SI.TimeStamp <= '{((DateTime)filter.InvoiceDateEnd).ToString("yyyy-MM-dd")} 23:59:59'
";
            }
            if (filter.PaymentDateStart != null)
            {
                query += $@"
AND PP.TimeStamp >= '{((DateTime)filter.PaymentDateStart).ToString("yyyy-MM-dd")}'
";
            }
            if (filter.PaymentDateEnd != null)
            {
                query += $@"
AND PP.TimeStamp <= '{((DateTime)filter.PaymentDateEnd).ToString("yyyy-MM-dd")} 23:59:59'
";
            }
          
            var CustomerInvoices = DBHelper.QueryList(query);
            return CustomerInvoices;
        }
    }
}