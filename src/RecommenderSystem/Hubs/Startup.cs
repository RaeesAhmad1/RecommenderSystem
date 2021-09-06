using InventoryManagement.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

[assembly: OwinStartup("Startup", typeof(Startup))]
namespace InventoryManagement.Hubs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
            //var properties = new AppProperties(app.Properties);
            //CancellationToken token = properties.OnAppDisposing;
            //if (token != CancellationToken.None)
            //{
            //    token.Register(() =>
            //    {
            //        SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
            //    });
            //}
        }
    }
}