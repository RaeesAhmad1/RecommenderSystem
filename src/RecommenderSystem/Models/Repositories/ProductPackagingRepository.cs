using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class ProductPackagingRepository : DBRepository
    {
        public bool Insert(ProductPackaging p)
        {
            return DBHelper.Insert(p);
        }
        public bool Update(ProductPackaging u)
        {
            return DBHelper.Update(u,
                "Where ProductID = @ProductID AND PackagingID = @PackagingID", new Dictionary<string, string> {
                { "@PackagingID", u.PackagingID.ToString() } ,
                { "@ProductID", u.ProductID.ToString() } }
            );
        }
        public bool Delete(int ProductID, int PackagingID)
        {
            db.values.Add("@PackagingID", PackagingID.ToString());
            db.values.Add("@ProductID", ProductID.ToString());
            string query = "";
            return DBHelper.ExecuteQuery(query, db.values);
        }
        public ProductPackaging Get(int ProductID, int PackagingID)
        {
            db.values.Add("@PackagingID", PackagingID.ToString());
            db.values.Add("@ProductID", ProductID.ToString());
            return DBHelper.Get<ProductPackaging>("", "Where ProductID = @ProductID AND PackagingID = @PackagingID", db.values);
        }
        public List<ProductPackaging> GetList(int iD)
        {
            return DBHelper.GetList<ProductPackaging>(whereClause: "Where ProductID = @ProductID ", Params: new Dictionary<string, string>() { { "@ProductID", iD.ToString() } });

        }
        public List<ProductPackaging_LV> GetPackaginListView(int iD)
        {
            db.values.Clear();
            db.values.Add("@ID", iD.ToString());
            string query = @"
Select Product_Packaging.*,packaging.Name as PackagingName , PU.Name as Unit
from Products.Product_Packaging 
INNER JOIN Products.Packaging packaging
ON packaging.ID = Product_Packaging.PackagingID
INNER JOIN Products.Product ON Product.ID = Products.Product_Packaging.ProductID
LEFT JOIN Products.Unit PU ON PU.ID = Product.UnitID
Where Product_Packaging.ProductID = @ID
";
            return DBHelper.GetList<ProductPackaging_LV>(query, Params: db.values);
        }

        public bool UpdateStock(List<ProductPackaging> packagingList, int UserID)
        {
            packagingList = packagingList.Where(x => x.Quantity != 0).ToList();
            string query = string.Empty;
            int i = 0;
            db.values.Add("@UserID", UserID.ToString());
            foreach (var element in packagingList)
            {
                db.values.Add($"@PackageSize_{i}", element.PackageSize.ToString());
                db.values.Add($"@PackagingID_{i}", element.PackagingID.ToString());
                db.values.Add($"@ProductID_{i}", element.ProductID.ToString());
                db.values.Add($"@Quantity_{i}", element.Quantity.ToString());
                query += $"UPDATE Products.Product_Packaging SET Quantity += @Quantity_{i} Where PackagingID = @PackagingID_{i} and ProductID = @ProductID_{i} AND PackageSize = @PackageSize_{i};";
                query += $@"
INSERT INTO Stocks.Stock (ProductID, PackagingID, PackageSize, Quantity, Reason, UserID, Type ) 
VALUES (@ProductID_{i}, @PackagingID_{i}, @PackageSize_{i}, @Quantity_{i}, '{element.Reason}', @UserID, '{StockCategory.DeliveredStock}' )  
";
                i++;  
            }
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }

        public bool ReturnStock(List<ProductPackaging> packagingList, int UserID)
        {
            packagingList = packagingList.Where(x => x.Quantity != 0).ToList();
            string query = string.Empty;
            int i = 0;
            db.values.Add("@UserID", UserID.ToString());
            foreach (var element in packagingList)
            {
                db.values.Add($"@PackageSize_{i}", element.PackageSize.ToString());
                db.values.Add($"@PackagingID_{i}", element.PackagingID.ToString());
                db.values.Add($"@ProductID_{i}", element.ProductID.ToString());
                db.values.Add($"@Quantity_{i}",  element.Quantity.ToString());
                query += $"UPDATE Products.Product_Packaging SET Quantity -= @Quantity_{i} Where PackagingID = @PackagingID_{i} and ProductID = @ProductID_{i} AND PackageSize = @PackageSize_{i};";
                query += $@"
INSERT INTO Stocks.Stock (ProductID, PackagingID, PackageSize, Quantity, Reason, UserID, Type ) 
VALUES (@ProductID_{i}, @PackagingID_{i}, @PackageSize_{i}, @Quantity_{i}, '{element.Reason}', @UserID, '{StockCategory.ReturnStock}' )  
";
                i++;
            }
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }
    }


}