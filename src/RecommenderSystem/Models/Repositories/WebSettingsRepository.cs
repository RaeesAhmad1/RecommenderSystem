using InventoryManagement.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Repositories
{
    public class WebSettingsRepository : DBRepository
    {

        public bool Update(WebSettings wb)
        {
            string query = "EXECUTE [General].[UpdateProjectSettings] @ProjectTitle, @ProjectImagePath";
            return DBHelper.ExecuteQuery(query, new Dictionary<string, string> {
                { "@ProjectTitle", wb.ProjectTitle },
                { "@ProjectImagePath", wb.ProjectImagePath }
            });
        }

        public WebSettings Get()
        {
            string query = "SELECT TOP 1 * FROM [General].[WebSettings]";
            return DBHelper.Get<WebSettings>(query,"", null);
        }

        public bool DateExpired()
        {
            string query = "SELECT Record FROM General.WebSettings";
            DateTime ExpirationDate =  DateTime.Parse(DBHelper.QueryColumn(query, null).First());
            return (ExpirationDate < DateTime.Now);
        }

        
        public void DefaultSettings()
        {
            string query = $@"
IF()
";
        }
    }
}