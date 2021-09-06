using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class CategoryRepository : DBRepository
    {
        public List<Category_LV> GetList()
        {
            string query = "Select c.*, COUNT(p.ID) as TotalProducts from [Products].Category c LEFT JOIN Products.Product p ON c.ID = p.CategoryID GROUP BY c.Description,c.ID,c.Name, c.Sellable, c.Packed, c.LaborMeterial, C.RawMeterial";
            return DBHelper.GetList<Category_LV>(query);
        }

        public bool Update(Category category)
        {
            return DBHelper.Update(category, "Where ID = @CategoryID", new Dictionary<string, string> {
                { "@CategoryID", category.ID.ToString()}
            });
        }

        public bool Insert(Category category)
        {
            return DBHelper.Insert(category);
        }

        public bool Delete(int categoryID)
        {
            db.values.Clear();
            db.values.Add("@CategoryID", categoryID.ToString());
            string query = $@"
 DELETE S from Stocks.Stock S INNER JOIN Products.Product P ON P.ID = S.ProductID
 INNER JOIN Products.Category PC ON PC.ID = P.CategoryID Where PC.ID = @CategoryID

 DELETE SI from  Sales.Invoice_Product_Packaging SI
 INNER JOIN Products.Product_Packaging PP ON PP.ID = SI.Product_Packaging_ID
 INNER JOIN Products.Product P ON P.ID = PP.ProductID
 INNER JOIN Products.Category PC ON PC.ID = P.CategoryID
 Where PC.ID = @CategoryID 

 DELETE I from Sales.Invoice I 
 INNER JOIN Sales.Invoice_Product_Packaging SI ON I.ID = SI.InvoiceID
 INNER JOIN Products.Product_Packaging PP ON PP.ID = SI.Product_Packaging_ID
 INNER JOIN Products.Product P ON P.ID = PP.ProductID
 INNER JOIN Products.Category PC ON PC.ID = P.CategoryID
 Where PC.ID = @CategoryID 

 DELETE PP from  Products.Product_Packaging PP 
 INNER JOIN Products.Product P ON P.ID = PP.ProductID
 INNER JOIN Products.Category PC ON PC.ID = P.CategoryID
 Where PC.ID = @CategoryID

 DELETE P from   Products.Product P
 INNER JOIN Products.Category PC ON PC.ID = P.CategoryID
 Where PC.ID = @CategoryID

 DELETE PC from  Products.Category PC Where PC.ID = @CategoryID 
";
            return DBHelper.ExecuteTransactionQuery(query, db.values);

           // return DBHelper.Delete<Category>("Where ID = @CategoryID", new Dictionary<string, string> { { "@CategoryID", categoryID.ToString() } });
        }

        public Category Get(int iD)
        {
            string query = $@"
Select top 1  c.*, COUNT(p.ID) as TotalProducts from [Products].Category c LEFT JOIN Products.Product p ON c.ID = p.CategoryID  Where c.ID = {iD} GROUP BY c.Description,c.ID,c.Name, c.Sellable, c.Packed, c.LaborMeterial, C.RawMeterial
";
            return DBHelper.Get<Category_LV>(query, null, null);
        }
    }
}