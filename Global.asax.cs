using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace twd_project
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            // Maps the custom user table with the tables added in by the 'SimpleMembershipProvider'.
            // Use 'DefaultConnection' for SimpleMembershipProvider services.
            // Use 'Contact' table for custom users.
            // Use the 'UserID' column of the 'Contact' table for the user's ID.
            // Use the 'UserName' column of the 'Contact' table for user's name which is the loginName.
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "TWD_User",
                                                     "UserID", "UserName",
                                                      autoCreateTables: true);
        }
    }
}