using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Users")]
    [Table("Role")]
    public class Role
    {
        [DontInsert]
        [DontUpdate]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool CanAddProducts { get; set; }
        public bool CanAddInvoice { get; set; }
        public bool CanViewInvoice { get; set; }
        public bool CanUpdateInvoice { get; set; }
        public bool CanDeleteInvoice { get; set; }
        public bool CanViewProduct { get; set; }
        public bool CanUpdateProduct { get; set; }
        public bool CanDeleteProduct { get; set; }
        public bool CanViewStock { get; set; }
        public bool CanAddStock { get; set; }
        public bool CanUpdateStock { get; set; }
        public bool CanDeleteStock { get; set; }
        public bool CanViewDashboard { get; set; }
        public bool CanChangeProductSettings { get; set; }
        public bool CanChangeBranding { get; set; }
        public bool CanViewUsers { get; set; }
        public bool CanAddUsers { get; set; }
        public bool CanDeleteUsers { get; set; }
        public bool CanUpdateUsers { get; set; }
        public bool CanViewRoles { get; set; }
        public bool CanAddRoles { get; set; }
        public bool CanDeleteRoles { get; set; }
        public bool CanUpdateRoles { get; set; }
        public bool CanViewCustomer { get; set; }
        public bool CanAddCustomer { get; set; }
        public bool CanUpdateCustomer { get; set; }
        public bool CanDeleteCustomer { get; set; }
        public bool CanReturnInvoice { get; set; }
        public bool CanViewDispute { get; set; }
        public bool CanResolveDispute { get; set; }
        public bool CanViewCheque { get; set; }
        public bool CanViewExpense { get; set; }
        public bool CanAddExpense { get; set; }
        public bool CanUpdateExpense { get; set; }
        public bool CanDeleteExpense { get; set; }
        public bool IsDeveloper { get; set; }

        public bool CanAddFormulas { get; set; }
        public bool CanViewFormulas { get; set; }
        public bool CanDeleteFormulas { get; set; }
        public bool CanUpdateFormulas { get; set; }
        public bool CanHardDeleteFormulas { get; set; }

        public bool CanAddLabor { get; set; }
        public bool CanViewLabor { get; set; }
        public bool CanUpdateLabor { get; set; }
        public bool CanDeleteLabor { get; set; }
        public bool CanHardDeleteLabor { get; set; }
        public bool CanAddWorker { get; set; }
        public bool CanViewWorker { get; set; }
        public bool CanDeleteWorker { get; set; }
        public bool CanHardDeleteWorker { get; set; }
        public bool CanUpdateWorker { get; set; }
        public Role()
        {

        }

        public Role(string Role = "")
        {
            if (Role == "Developer")
            {
                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.GetType() == typeof(bool))
                    {
                        property.SetValue(this, true);
                    }
                }
            }


            List<Role> roles = new List<Role>(); //<-
      
            List<Role> filteredRoles = new List<Role>();


            foreach (var k in roles)
            {
                if (k.CanAddExpense == true)
                {
                    filteredRoles.Add(k);
                }
            }
        }
    }
}