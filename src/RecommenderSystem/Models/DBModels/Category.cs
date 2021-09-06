using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Products")]
    [Table("Category")]
    public class Category
    {
        [DontInsert]
        [DontUpdate]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool Sellable { get; set; }
        public string Description { get; set; }
        public bool LaborMeterial { get; set; }
        public bool RawMeterial { get; set; }
    }
}
