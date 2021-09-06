using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.DBModels
{
    [Schema("Products")]
    [Table("Packaging")]
    public class Packaging
    {
        [DontInsert]
        [DontUpdate]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}