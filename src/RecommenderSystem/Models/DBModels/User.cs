using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.ViewModels;

namespace InventoryManagement.Models.DBModels
{
    [Table("User")]
    [Schema("Users")]
    public class User
    {
        [DontInsert]
        [DontUpdate]
        public int Id { get; set; }
        public string Username { get; set; }

        [MaxLength(length: 50, ErrorMessage = "Only 50 characters are allowed<br />")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Letters(Alphabets) are allowed in Name<br />")]
        public string FirstName { get; set; }

        [MaxLength(length: 50, ErrorMessage = "Only 50 characters are allowed<br />")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Letters(Alphabets) are allowed in Name<br />")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,200})", ErrorMessage = "Password must contain atleat one uppercase letter, one lowercase and one number and Length must be greater than 8<br />")]
        [MaxLength(250, ErrorMessage = "You cannot have password with more than 250 strength <br />")]
        public string Password { get; set; }


        [Remote("IsAvailableEmail", "Validation", AdditionalFields = "Id")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "The Email is Invalid")]
        [MaxLength(250, ErrorMessage = "Longer emails than 250 characters are not accepted <br />")]
        public string Email { get; set; }

        [Remote("IsAvailableMobileNo", "Validation", AdditionalFields = "Id")]
        [Required(ErrorMessage = "Mobile No is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a number")]
        public string MobileNo { get; set; }

        [RegularExpression(@"((Male)|(Female))", ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }

        public DateTime Birthday { get; set; }
        public bool BlockedStatus { get; set; }
        public string BlockedReason { get; set; }
        [DontInsert][DontUpdate]
        public DateTime LastLogin { get; set; }
        public string LastLoginIpAddress { get; set; }
        [DontInsert]
        [DontUpdate]
        public DateTime LastModified { get; set; }
        [DontInsert]
        [DontUpdate]
        public DateTime AccountCreation { get; set; }
        public int RoleID { get; set; }
        public string Address { get; set; }


        [Required]
        [DontInsert][DontUpdate][DontLoad]
        [DisplayName("ReType Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,200})", ErrorMessage = "Password must contain atleat one uppercase letter, one lowercase and one number and Length must be greater than 8<br />")]
        public string ConfirmPassword { get; set; }
        

    }
}