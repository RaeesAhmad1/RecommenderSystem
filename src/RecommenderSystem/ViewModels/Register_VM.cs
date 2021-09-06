using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.ViewModels
{
    public class Register_VM : User
    {
        public string ReturnUrl { get; set; }


        
    }
}