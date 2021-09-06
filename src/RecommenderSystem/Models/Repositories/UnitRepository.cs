using InventoryManagement.Models.DBModels;
using InventoryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class UnitRepository : DBRepository
    {
        public bool Insert(Unit u)
        {
            return DBHelper.Insert(u);
        }
        public bool Update(Unit u)
        {
            return DBHelper.Update(u, "Where ID = @UnitID", new Dictionary<string, string> { { "@UnitID", u.ID.ToString() } });
        }
        public bool Delete(int UnitID)
        {
            db.values.Clear();
            db.values.Add("@UnitID", UnitID.ToString());
            string query = @"
Delete SIP From Sales.Invoice_Product_Packaging SIP INNER JOIN Products.Product_Packaging PP ON SIP.Product_Packaging_ID = PP.ID
INNER JOIN Products.Product P ON P.ID = PP.ProductID INNER JOIN Products.Unit U ON U.ID = P.UnitID
Where U.ID = @UnitID

Delete S from Sales.Invoice S Where ID IN(
Select S.ID from Sales.Invoice S LEFT JOIN Sales.Invoice_Product_Packaging SIP ON SIP.InvoiceID = S.ID
Group by S.ID Having COUNT(SIP.InvoiceID) = 0)

Delete PP from Products.Product_Packaging PP INNER JOIN Products.Product P ON P.ID = PP.ProductID
INNER JOIN Products.Unit U ON U.ID = P.UnitID
Where U.ID = @UnitID

Delete ST from Stocks.Stock ST INNER JOIN Products.Product P ON P.ID = ST.ProductID
INNER JOIN Products.Unit U ON U.ID = P.UnitID
Where U.ID = @UnitID

Delete P from Products.Product P INNER JOIN Products.Unit U ON U.ID = P.UnitID 
Where U.ID = @UnitID

Delete from Products.Unit Where ID = @UnitID

";
            return DBHelper.ExecuteTransactionQuery(query, db.values);
        }

        public List<Unit> GetList()
        {
            return DBHelper.GetList<Unit>();
        }
        public List<Unit_LV> GetListWithCount()
        {
            string query = "SELECT COUNT(P.ID) as TotalProducts, U.* FROM Products.Unit U LEFT JOIN Products.Product P ON P.UnitID = U.ID Group by U.ID, U.Name";
            return DBHelper.GetList<Unit_LV>(query);
        }


        public Unit Get(int unitID)
        {
            db.values.Add("@UnitID", unitID.ToString());
            return DBHelper.Get<Unit>("","Where ID = @UnitID", db.values);
        }
    }
}