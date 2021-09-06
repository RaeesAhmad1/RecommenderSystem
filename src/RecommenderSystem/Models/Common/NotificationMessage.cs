using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models.Common
{

    [Schema("General")]
    [Table("NotificationMessage")]
    public class NotificationMessage
    {
        [DontInsert]
        [DontUpdate]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MessageType { get; set; }
        public string NotificationType { get; set; }
        public string Icon { get; set; }
        public bool IsAjaxMessage { get; set; }
        public bool IsViewMessage { get; set; }
        public bool IsRedirectMessage { get; set; }
        public string Code { get; set; }
        public string URL { get; set; }
        public bool Viewed { get; set; }
        [DontInsert]
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }

        public NotificationMessage()
        {
            IsAjaxMessage = true;
            IsViewMessage = true;
            IsRedirectMessage = true;
        }

        public NotificationMessage(SqlDataReader reader)
        {
            ID = (int)reader["ID"];
            Title = reader["Title"].ToString();
            Description = reader["Description"].ToString();
            MessageType = reader["MessageType"].ToString();
            NotificationType = reader["NotificationType"].ToString();
            Icon = reader["Icon"].ToString();
            IsAjaxMessage = (bool)reader["IsAjaxMessage"];
            IsViewMessage = (bool)reader["IsViewMessage"];
            IsRedirectMessage = (bool)reader["IsRedirectMessage"];
            URL = reader["URL"].ToString();
           // DateTime.TryParse(reader["TimeStamp"].ToString(), out DateTime tempTimeStamp);
           // TimeStamp = tempTimeStamp;
        }
    }
}