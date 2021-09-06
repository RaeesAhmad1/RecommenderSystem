using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace InventoryManagement.Models.DBModels
{

    [Schema("Products")]
    [Table("Product")]
    public class Product
    {
        [DontInsert][DontUpdate]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Remote("IsProductNameAvailable",  "Validation", AdditionalFields = "ID", HttpMethod = "POST", ErrorMessage = "Product Name already exists")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitID { get; set; }
        public int CategoryID { get; set; }
        [DisplayName("MU")]
        public string MU { get; set; }
        [DisplayName("SKU")]
        public string SKU { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public bool Featured { get; set; }
        public string ImagePath { get; set; }
    }

    [Table(nameof(WishList))]
    [Schema("Products")]
    public class WishList
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
