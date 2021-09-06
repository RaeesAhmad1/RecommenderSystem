using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Sales")]
    [Table("Customer")]
    public class Customer
    {
        [DontInsert]
        [DontUpdate]
        public int ID { get; set; }

        [Column("Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CreditLimit { get; set; }



    

    }

}