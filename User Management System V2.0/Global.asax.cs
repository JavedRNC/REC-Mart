using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace User_Management_System_V2._0
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeAuthenticationProcess();
        }
        public class Global : HttpApplication
        {
            protected void Application_BeginRequest(object sender, EventArgs e)
            {
                
            }
        }

        private void InitializeAuthenticationProcess()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("dbx", "UsersTable", "UserId", "UserName", true);
                if (WebSecurity.Initialized)
                {
                    //WebSecurity.CreateUserAndAccount("admin", "admin");
                    //Roles.CreateRole("Administrator");
                    //Roles.CreateRole("Manager");
                    //Roles.CreateRole("User");
                    //Roles.AddUserToRole("admin", "Administrator");

                }


            }
        }
    }




