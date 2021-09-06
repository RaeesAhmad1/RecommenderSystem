using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Products")]
    [Table("Unit")]
    public class Unit
    {
        [DontInsert][DontUpdate]
        public int ID { get; set; }
        public string Name { get; set; }
        
    }
}