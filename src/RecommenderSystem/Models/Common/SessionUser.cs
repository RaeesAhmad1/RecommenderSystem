using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Common
{
    public class SessionUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string AuthToken { get; set; }
        public string Type { get; set; }
        public int RoleID { get; set; }


        public SessionUser(User user)
        {

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Type = "User";
            MobileNo = user.MobileNo;
            Birthday = user.Birthday;
            Gender = user.Gender;
            Username = user.Username;
            RoleID = user.RoleID;
        }

        public SessionUser()
        {

        }



    }
}