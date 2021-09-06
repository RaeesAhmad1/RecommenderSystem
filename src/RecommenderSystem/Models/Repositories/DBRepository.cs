using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    [ClearDictionary]
    public class DBRepository
    {
        public DBHelper db { get; set; }
        public DBRepository()
        {
            this.db = new DBHelper();
        }
    }

    public class ClearDictionaryAttribute : Attribute
    {
        public ClearDictionaryAttribute()
        {
            
        }
    }

}