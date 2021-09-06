using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class UserRepository : DBRepository
    {
        public User Get(int userId)
        {
            db.values.Add("@UserID", userId.ToString());
            return DBHelper.Get<User>("","Where ID = @UserID",db.values);
        }




        public List<User> GetList()
        {
            DBHelper db = new DBHelper();
            return DBHelper.GetList<User>();
        }

        public bool Update(User user)
        {
            DBHelper db = new DBHelper();
            user.Password = Security.Encrypt(user.Password);
            db.values.Add("@ID", user.Id.ToString());
            return DBHelper.Update(user, "Where ID = @ID", db.values);
        }

        public bool Insert(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                user.Username = user.FirstName + "." + Models.Common.Common.RandomString(4);
            }
            user.Password = Security.Encrypt(user.Password);
            return DBHelper.Insert(user);
        }

        public bool Delete(int userID)
        {
            db.values.Clear();
            db.values.Add("@ID", userID.ToString());
            return DBHelper.Delete<User>("Where ID = @ID", db.values);
        }

        public List<User> GetUsers()
        {
            db.values.Clear();
            return DBHelper.GetList<User>("Select *  FROM Users.[User] U INNER JOIN Users.Role R ON U.RoleID = R.ID Where R.IsDeveloper = 0");
        }
    }
}