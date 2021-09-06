using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.ViewModels
{
    public class Login_VM
    {
        [Remote("IsNotAvailableEmail", "Validation")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email")]
        [MaxLength(250, ErrorMessage = "Longer emails than 250 characters are not accepted <br />")]
        public string Email { get; set; }

        [Remote("IsPasswordInCorrect", "Validation", AdditionalFields = "Email")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public Login_VM()
        {
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.ReturnUrl = string.Empty;
        }

    }
}