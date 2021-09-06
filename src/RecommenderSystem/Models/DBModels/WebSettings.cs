using System;
using System.Collections.Generic;

namespace InventoryManagement.Models.DBModels
{
    [Schema("General")]
    [Table("WebSettings")]
    public class WebSettings
    {
        public string ProjectTitle { get; set; }
        public string ProjectImagePath { get; set; }
        public string Address { get; set; }
    }

    

}