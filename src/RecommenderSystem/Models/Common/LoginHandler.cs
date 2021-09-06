using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Common
{
    public class LoginHandler
    {

        public static SessionUser GetUser(string CookieData, string IPAddress)
        {
            string AuthCookie = CookieData;
            string[] temp = AuthCookie.Split('|');
            string selector = temp[0];
            string token = temp[1];
            string username = string.Empty;
            string type = string.Empty;
            User _user = new User();
            DBHelper db = new DBHelper();
            string query = "Select Username from[Others].[AuthToken] oa where oa.Token = @Token and oa.Selector = @Selector";
            db.values.Add("@Selector", selector);
            db.values.Add("@Token", token);
            List<string> UsernameList = DBHelper.QueryColumn(query, db.values);
            if (UsernameList.Count > 0)
            {
                db.values.Add("@Username", UsernameList.First());
                DBHelper.Get(_user, where_clause: "where Username = @Username", Params: db.values);
            }
            SessionUser sessionUser = new SessionUser(_user);
            //CreateTokens(sessionUser, IPAddress);
            return sessionUser;
        }


        public static SessionUser GetUser(string Email, string password, string IPAddress)
        {
            DBHelper db = new DBHelper();
            SessionUser _user = new SessionUser();
            User user = new User();
            db.values.Add("@Email", Email);
            db.values.Add("@Password", Security.Encrypt(password));
            DBHelper.Get(user, where_clause: "where Email = @Email and password = @Password COLLATE SQL_Latin1_General_CP1_CS_AS", Params: db.values);
            _user = new SessionUser(user);
            //if (!string.IsNullOrWhiteSpace(user.Username))
            //{
            //    CreateTokens(_user, IPAddress);
            //}
            return _user;
        }

        //private static void CreateTokens(SessionUser _user, string IPAddress)
        //{
        //    _user.SessionToken = SessionToken.CreateToken(_user.Username, IPAddress);
        //    _user.AuthToken = AuthToken.CreateToken(_user.Username, IPAddress);
        //}



    }
}