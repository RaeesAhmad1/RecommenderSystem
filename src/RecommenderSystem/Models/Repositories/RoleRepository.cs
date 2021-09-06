using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class RoleRepository : DBRepository
    {
        public List<Role> GetList()
        {
            return DBHelper.GetList<Role>("","Where IsDeveloper = 0");
        }
        public bool Insert(Role  role)
        {
            return DBHelper.Insert(role);
        }

        public bool Update(Role role)
        {
            db.values.Add("@ID", role.ID.ToString());
            return DBHelper.Update(role, "Where ID = @ID", db.values);
        }

        public bool Delete(int ID)
        {
            db.values.Add("@ID", ID.ToString());
            string query = @"
DELETE FROM Users.[User] Where RoleID = @ID;
DELETE FROM Users.[Role] Where ID = @ID;
";
            return DBHelper.ExecuteQuery(query, db.values);
        }

        public Role Get(int iD)
        {
            db.values.Clear();
            db.values.Add("@ID", iD.ToString());
            return DBHelper.Get<Role>("","Where ID = @ID", db.values);
        }



        public void CreateDefaultRole()
        {
            string query = "SELECT COUNT(*) FROM Users.[Role] Where IsDeveloper = 1";
            int count = DBHelper.GetScaler(query);
            int RoleID = 0;
            if (count == 0)
            {
                Role role = new Role("Developer");
                RoleID = DBHelper.GetScaler(QueryMaker.InsertQuery(role) + "; SELECT SCOPE_IDENTITY();");
            }
            else
            {
                RoleID = DBHelper.GetScaler("SELECT ID FROM Users.[Role] where IsDeveloper = 1");
            }

            query = $"SELECT COUNT(*) FROM Users.[User] Where RoleID = {RoleID} AND Email = 'zetawars@hotmail.com'";
            int DevCount = DBHelper.GetScaler(query);

            if (DevCount == 0)
            {
                User user = new Models.DBModels.User
                {
                    FirstName = "Zawar",
                    LastName = "Mahmood",
                    Password = Security.Encrypt("Zawar123"),
                    MobileNo = "03074655233",
                    RoleID = RoleID,
                    Email = "zetawars@hotmail.com",
                    Gender = "Male",
                    Username = "zetawars",
                    Birthday = DateTime.Parse("1994-1-4")
                };
                DBHelper.Insert(user);
            }

            query = $"SELECT COUNT(*) FROM Users.[User] Where RoleID = {RoleID} AND Email = 'hamzaharis13@gmail.com'";
            DevCount = DBHelper.GetScaler(query);

            if (DevCount == 0)
            {
                User user = new Models.DBModels.User
                {
                    FirstName = "Hamza",
                    LastName = "Haris",
                    Password = Security.Encrypt("Hhamza13"),
                    MobileNo = "03236774896",
                    RoleID = RoleID,
                    Birthday = DateTime.Parse("1992-6-28"),
                    Email = "hamzaharis13@gmail.com",
                    Gender = "Male",
                    Username = "haris"
                };
                DBHelper.Insert(user);
            }
        }
    }
}