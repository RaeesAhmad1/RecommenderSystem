using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using InventoryManagement.Controllers;

namespace InventoryManagement.Models.Repositories
{
    public class ProductRepository : DBRepository
    {
        private string productQuery = $@"
Select p.*, 
c.Name as CategoryName, 
PU.Name as Unit, 
PPack.Name as Packaging,
PPack.ID as PackagingID, 
PP.PackageSize,
PP.Quantity,
PP.PricePerPiece,
PP.RetailPrice,
ISNULL(AVG(PR.Rating), 0) as Rating,
ISNULL(COUNT(PR.Rating), 0) as Reviews,

PP.ID as ProductPackagingID
from Products.Product p 
INNER JOIN Products.Category c on p.CategoryID = c.ID 
INNER JOIN Products.Unit PU ON PU.ID = P.UnitID 
INNER JOIN Products.Product_Packaging  PP ON PP.ProductID = p.ID
INNER JOIN Products.Packaging PPack ON PPack.ID = PP.PackagingID
LEFT JOIN Products.WishList W ON W.ProductId = p.ID
LEFT JOIN ProductReview PR ON PR.ProductId = P.ID
where 1=1
";

        private string GroupByStatement = $@"
GROUP BY 
P.Id, P.CategoryID,P.Color,P.Description,P.Featured,P.ImagePath,P.MU, P.Name,P.Size, P.SKU, P.Type, P.UnitID,P.Vendor,
P.ScreenSize, P.memory,C.Name,
PU.Name, PPack.Name, PPack.ID, PP.PackageSize, PP.Quantity, PP.PricePerPiece, PP.RetailPrice, PP.Id
";
        public bool Insert(Product p)
        {
            return DBHelper.Insert(p);
        }

        public bool Insert(AddProduct_VM p, int UserId)
        {
            string query = QueryMaker.InsertQuery(p.product);
            query += "DECLARE @ProductID INT  =( SELECT SCOPE_IDENTITY() );";
            foreach (var element in p.ProductPacking)
            { 
                query += $@"INSERT INTO Products.[Product_Packaging] (ProductID, PackagingID, PackageSize, Quantity, PricePerPiece, RetailPrice) VALUES (@ProductID, {element.PackagingID}, {element.PackageSize}, {element.Quantity} , {element.PricePerPiece}, {element.RetailPrice});";
                query += $@"INSERT INTO Stocks.Stock (ProductID, PackagingID, PackageSize, Quantity, Reason,  UserId , Type) VALUES (@ProductID, {element.PackagingID},{element.PackageSize}, {element.Quantity},'{"Initial Product Stock."}', {UserId} , '{StockCategory.DeliveredStock}');";
            }
            return DBHelper.ExecuteQuery(query);
        }

        public List<Product> GetUsableProducts()
        {
            string query = $@"
SELECT P.* from Products.Product P INNER JOIN Products.Category ON Category.ID = P.CategoryID
Where Category.RawMeterial = 1
";
            return DBHelper.GetList<Product>(query);
        }

        public List<Product> GetCreatableProducts()
        {
            string query = $@"
SELECT P.* from Products.Product P INNER JOIN Products.Category ON Category.ID = P.CategoryID
Where Category.Sellable = 1
";
            return DBHelper.GetList<Product>(query);
        }

        public List<Product> GetLaborProducts()
        {
            string query = @"
SELECT P.* from Products.Product P INNER JOIN Products.Category ON Category.ID = P.CategoryID
Where Category.LaborMeterial =1
";
            return DBHelper.GetList<Product>(query);
        }

        public List<Product_Submit> GetInoviceList(int iD)
        {
            db.values.Clear();
            db.values.Add("@InvoiceID", iD.ToString());
            string query = @"
Select p.*, 
c.Name as CategoryName, 
PU.Name as Unit, 
PPack.Name as Packaging,
PPack.ID as PackagingID, 
PP.PackageSize,
PP.Quantity,
SIPP.AmountPayable,
SIPP.Quantity as SoldQuantity,
PP.PricePerPiece,
PP.RetailPrice,
PP.ID as ProductPackagingID
from Products.Product p 
INNER JOIN Products.Category c on p.CategoryID = c.ID 
INNER JOIN Products.Unit PU ON PU.ID = P.UnitID 
INNER JOIN Products.Product_Packaging  PP ON PP.ProductID = p.ID
INNER JOIN Products.Packaging PPack ON PPack.ID = PP.PackagingID
INNER JOIN Sales.Invoice_Product_Packaging SIPP ON SIPP.Product_Packaging_ID = PP.ID
Where SIPP.InvoiceID = @InvoiceID
";
            return DBHelper.GetList<Product_Submit>(query :query, Params: db.values);
        }

        //public List<Product_LV> GetInoviceList(int iD)
        //{

        //}

        public bool Update(AddProduct_VM vm, int UserID)
        {

            //if (vm.ImageFile != null)
            //{
            //    string fn = Path.GetFileName(vm.ImageFile.FileName);
            //    string fileExtension = fn.Remove(0, fn.IndexOf('.') + 1);
            //    fn = vm.product.Name + "." + fileExtension;
            //    string SaveLocation = "~/Images/Products/" + fn;
            //    vm.product.ImagePath = SaveLocation;
            //    if (System.IO.File.Exists(SaveLocation))
            //    {
            //        System.IO.File.Delete(SaveLocation);
            //    }
            //    vm.ImageFile.SaveAs(Path.Combine(SaveLocation, fn));
            //}


            string query = string.Empty;
            string Condition = string.Empty;
            if (vm.ProductPacking != null)
            {
                foreach (var element in vm.ProductPacking)
                {
                    Condition += $" AND PP.ID != {element.ID} ";
                }
            }

            db.values.Add("@ProductID", vm.product.ID.ToString());
            query += QueryMaker.UpdateQuery(vm.product, "Where ID = @ProductID;");

            foreach (var element in vm.ProductPacking)
            {
                if (element.ID == 0)
                {
                    element.ProductID = vm.product.ID;
                    query += QueryMaker.InsertQuery(element) + ";";
                }
                else
                {
                    element.ProductID = vm.product.ID;
                    
                    int OldQuantity = DBHelper.GetScaler($"SELECT Quantity from Products.Product_Packaging Where ID = {element.ID}");
                    int NewQuantity = element.Quantity;

                    string StoreQuantity = string.Empty;
                    if (OldQuantity - NewQuantity > 0)
                    {
                        StoreQuantity = "-" + (OldQuantity - NewQuantity).ToString();
                    }
                    else
                    {
                        StoreQuantity = "+" + (NewQuantity - OldQuantity).ToString();
                    }

                    query += $@"
INSERT INTO Stocks.Stock (ProductID, PackagingID, PackageSize, Quantity, Reason, UserID, Type ) 
VALUES ({element.ProductID}, {element.PackagingID}, {element.PackageSize}, {StoreQuantity}, 'Stock Modified', {UserID}, '{StockCategory.DeliveredStock}' )  
";
                    query += QueryMaker.UpdateQuery(element, $"Where ID = {element.ID}") + ";";
                }
            }
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }

        public int InsertAndGetID(Product p)
        {
            return DBHelper.GetScaler(QueryMaker.InsertQuery(p) + ";SELECT SCOPE_IDENTITY();");
        }



        public List<Product_LV> GetSellableList(int CategoryID = 0)
        {
            string query = $@"{productQuery}";
            string WhereClause = string.Empty;
            var queryParameters = new Dictionary<string, string>();
            query += " AND c.Sellable = 1";
            if (CategoryID != 0)
            {
                queryParameters = new Dictionary<string, string> { { "@CategoryID", CategoryID.ToString() } };
                query += " AND c.ID = @CategoryID ";
            }
            //query += "Group by p.CategoryID,p.Description,p.ID,p.ID,p.Name,p.QTY,p.UnitID,c.Name,pu.Name, pp.Size, pp.Qty,pp.PackagingID";
            return DBHelper.GetList<Product_LV>(query, "", queryParameters);
        }


        public List<Product> GetProductList()
        {
            string query = $@"
Select DISTINCT p.*, 
c.Name as CategoryName, 
PU.Name as Unit
from Products.Product p 
INNER JOIN Products.Category c on p.CategoryID = c.ID 
INNER JOIN Products.Unit PU ON PU.ID = P.UnitID 
INNER JOIN Products.Product_Packaging PP ON PP.ProductID = P.ID
";
            return DBHelper.GetList<Product>(query);
        }

        public List<Product> GetProductsWithStock()
        {
            string query = $@"
Select DISTINCT p.*, 
c.Name as CategoryName, 
PU.Name as Unit
from Products.Product p 
INNER JOIN Products.Category c on p.CategoryID = c.ID 
INNER JOIN Products.Unit PU ON PU.ID = P.UnitID 
INNER JOIN Products.Product_Packaging PP ON PP.ProductID = P.ID
Where PP.Quantity > 0
";
            return DBHelper.GetList<Product>(query);
        }

        public List<Product_LV> GetList(int CategoryID = 0, int UnitID = 0, int PackingID = 0, bool RawMeterial = false)
        {
            string query = $@"{productQuery}";
            string WhereClause = string.Empty;
            if (CategoryID != 0)
            {
                db.values.Add("@CategoryID", CategoryID.ToString());
                WhereClause += " AND c.ID = @CategoryID";
            }
            if (UnitID != 0)
            {
                db.values.Add("@UnitID", UnitID.ToString());
                WhereClause += " AND UnitID = @UnitID";
            }
            if (PackingID != 0)
            {
                db.values.Add("@PackagingID", PackingID.ToString());
                WhereClause += " AND PackagingID = @PackagingID";
            }
            if (RawMeterial)
            {
                WhereClause += " AND c.RawMeterial = 1";
            }

            WhereClause += GroupByStatement;
            return DBHelper.GetList<Product_LV>(query, WhereClause, db.values);
        }

        public List<Product_LV> GetProductsWithLowerPrices(int CategoryId)
        {
            string query = $@"{productQuery}";
            query += $@"
AND P.CategoryID = {CategoryId}
";
            query += GroupByStatement + " Order By PP.RetailPrice asc";
            return DBHelper.GetList<Product_LV>(query);
        }

        public List<Product_LV> GetList(ProductFilter filter)
        {
            string query = $@"{productQuery}";
            string WhereClause = string.Empty;
          
            
            if (filter.CategoryId != 0)
            {
                db.values.Add("@CategoryID", filter.CategoryId.ToString());
                WhereClause += " AND c.ID = @CategoryID";
            }
            if (filter.PriceFrom != 0)
            {
                Product p = new Product();
                
                db.values.Add("@PriceFrom", filter.PriceFrom.ToString());
                WhereClause += " AND PP.RetailPrice >= @PriceFrom";
            }
            if (filter.PriceTo != 0)
            {
                db.values.Add("@PriceTo", filter.PriceTo.ToString());
                WhereClause += " AND PP.RetailPrice <= @PriceTo";
            }

            if (filter.ProductId != 0)
            {
                WhereClause += $" AND P.Id ={filter.ProductId}";
            }

            if (!string.IsNullOrWhiteSpace( filter.Name))
            {
                WhereClause += $" AND p.Name Like '%{filter.Name}%'";
            }

            if (!string.IsNullOrWhiteSpace(filter.Vendor))
            {
                WhereClause += $" AND p.Vendor Like '%{filter.Vendor}%'";
            }

            if (!string.IsNullOrWhiteSpace(filter.ScreenSize))
            {
                WhereClause += $" AND p.Vendor Like '%{filter.ScreenSize}%'";
            }

            if (!string.IsNullOrWhiteSpace(filter.Memory))
            {
                WhereClause += $" AND p.Vendor Like '%{filter.Memory}%'";
            }

            if (filter.Featured != null)
            {
                WhereClause += $" AND p.Featured = {((bool)filter.Featured ? "1" : "0")}";
            }

            if (filter.UserId != 0)
            {
                WhereClause += $" AND W.UserId = {filter.UserId}";
            }

            WhereClause += GroupByStatement;
            return DBHelper.GetList<Product_LV>(query, WhereClause, db.values);
        }



        public List<Product_Submit> GetInvoiceProducts(int invoiceID)
        {
            string query = $@"
Select p.*, 
c.Name as CategoryName, 
PU.Name as Unit, 
PPack.Name as Packaging,
PPack.ID as PackagingID, 
PP.PackageSize,
PP.Quantity,
PP.PricePerPiece,
SIP.AmountPayable,
PP.RetailPrice,
PP.ID as ProductPackagingID, SIP.Quantity as SoldQuantity
from Products.Product p 
INNER JOIN Products.Category c on p.CategoryID = c.ID 
INNER JOIN Products.Unit PU ON PU.ID = P.UnitID 
INNER JOIN Products.Product_Packaging  PP ON PP.ProductID = p.ID
INNER JOIN Products.Packaging PPack ON PPack.ID = PP.PackagingID
INNER JOIN Sales.Invoice_Product_Packaging SIP ON SIP.Product_Packaging_ID = PP.ID
Where SIP.InvoiceID = {invoiceID}
";
            return DBHelper.GetList<Product_Submit>(query);
            
        }




        public Product Get(int iD)
        {
            db.values.Add("@ProductID", iD.ToString());
            return DBHelper.Get<Product>("", "Where ID = @ProductID", db.values);
        }

        public bool Update(Product product)
        {
            return DBHelper.Update(product, "Where ID = @ProductID", new Dictionary<string, string> {
                { "@ProductID", product.ID.ToString()}
            });
        }

        public bool Delete(int productID)
        {
            db.values.Add("@ProductID", productID.ToString());
            string query = @"
DELETE FROM  SIP from Sales.Invoice_Product_Packaging SIP 
INNER JOIN [Products].Product_Packaging PP ON SIP.Product_Packaging_ID = PP.ID 
Where PP.ProductID =@ProductID;
Delete from [Products].[Product_Packaging] Where ProductID = @ProductID;
DELETE PF FROM Products.Formula_Product PF
INNER JOIN Products.Product_Packaging PP ON PF.ProductPackagingID = PF.ProductPackagingID
where PP.ProductID = @ProductID;
DELETE FROM Stocks.Stock Where ProductID = @ProductID;
Delete from [Products].[Product] Where ID = @ProductID;
";
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }

        internal void AddToWishList(int productId, int userId)
        {
            var query = $@"
IF not exists(SELECT TOP 1 * FROM Products.WishList where ProductId = {productId} AND UserId = {userId})
INSERT INTO Products.WishList (ProductId, UserId) VALUES ({productId}, {userId});

";
            DBHelper.ExecuteQuery(query);
        }

        public bool DeletePackaging(int PackagingID)
        {
            db.values.Add("@PackagingID", PackagingID.ToString());
            string query = @"
DELETE FROM  SIP from Sales.Invoice_Product_Packaging SIP INNER JOIN [Products].Product_Packaging PP ON SIP.Product_Packaging_ID = PP.ID Where PP.ProductID = @ProductID;
Delete from [Products].[Product_Packaging] Where ID = @PackagingID;
DELETE FROM Stocks.Stock Where ProductID = @PackagingID;
Delete from [Products].[Product] Where ID = @PackagingID;
";
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }


        public List<Dictionary<string, string>> GetTop10SoldProducts()
        {
            return DBHelper.QueryList(@"
Select Top 10 P.Name, U.Name as Unit, PK.Name as Packaging, PP.PackageSize, PP.PricePerPiece,
PC.Name as Category,
SUM(SIP.Quantity) TotalSold from Sales.Invoice_Product_Packaging SIP
INNER JOIN Products.Product_Packaging PP
ON PP.ID = SIP.Product_Packaging_ID
INNER JOIN Products.Product P ON P.ID = PP.ProductID
INNER JOIN Products.Unit U ON U.ID = P.UnitID
INNER JOIN Products.Packaging PK ON PK.ID = PP.PackagingID
INNER JOIN Products.Category PC ON PC.ID = P.CategoryID
GROUP BY SIP.Product_Packaging_ID, PP.PackageSize, PP.PricePerPiece, P.Name, U.Name, PK.Name, PC.Name
Order By TotalSold
");
        }

        public List<int> GetWishList(int userId)
        {
           return DBHelper.GetList<WishList>("", $"where UserId = {userId}").Select(x=>x.ProductId).ToList();
        }
    }
}