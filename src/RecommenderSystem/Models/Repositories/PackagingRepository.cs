using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Models.Repositories
{
    public class PackagingRepository : DBRepository
    {
        public bool Insert(Packaging u)
        {
            return DBHelper.Insert(u);
        }
        public bool Update(Packaging u)
        {
            return DBHelper.Update(u, "Where ID = @PackagingID", new Dictionary<string, string> { { "@PackagingID", u.ID.ToString() } });
        }
        public bool Delete(int PackagingID)
        {
            db.values.Clear();
            db.values.Add("@PackagingID", PackagingID.ToString());
            string query = @"
Delete PP from Products.Product_Packaging PP Where PackagingID = @PackagingID
DELETE S from Stocks.Stock S INNER JOIN Products.Packaging P ON P.ID = S.PackagingID Where PackagingID = @PackagingID
Delete from Products.Packaging Where ID = @PackagingID
";
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }

        public List<Packaging_LV> GetList()
        {
            return DBHelper.GetList<Packaging_LV>();
        }

        //public List<Packaging> GetAllSizes(int productID)
        //{
        //    return DBHelper.GetList<PackingSize>("SELECT Size, QTY from Products.Product_Packaging",
        //        " Where ProductID = @ProductID",
        //        new Dictionary<string, string>() { { "@ProductID", productID.ToString() } }).ToList();
        //}

        public int GetSelectedPackingID(int iD)
        {
            return DBHelper.GetScaler("Select top 1 PackagingID from [Products].Product_Packaging Where ProductId = @ProductID", new Dictionary<string, string>() {
                { "@ProductID", iD.ToString()}
            });
        }

        public Packaging Get(int packagingID)
        {
            db.values.Add("@PackingID", packagingID.ToString());
            return DBHelper.Get<Packaging>("","Where ID =@PackingID", db.values);
        }

        public List<ProductPackaging_LV> GetProductPackaging(int productID)
        {
            string query = $@"
Select  PPk.*, PK.Name as PackagingName, U.Name as Unit
 From Products.Packaging PK 
 INNER JOIN Products.Product_Packaging PPK ON PK.ID = PPK.PackagingID
 INNER JOIN Products.Product P ON P.ID = PPK.ProductID
 INNER JOIN Products.Unit U ON U.ID = P.UnitID
Where PPK.ProductID = {productID}
";
            db.values.Add("@ProductID", productID.ToString());
            return DBHelper.GetList<ProductPackaging_LV>(query,"", db.values);
        }
    }

}