using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using log4net;
using Common.Utilities;

namespace WebSetting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureLogging();
        }

        private void ConfigureLogging()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();

                // Set logfile name and application name variables
                log4net.GlobalContext.Properties["ApplicationCode"] = "ERP-Setting";
                log4net.GlobalContext.Properties["ServerName"] = System.Environment.MachineName;

                // Record application startup
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (Context.Handler is IRequiresSessionState)
            {
                log4net.ThreadContext.Properties["RemoteAddress"] = ApplicationHelpers.GetClientIP();
                //if (HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    log4net.ThreadContext.Properties["UserID"] = HttpContext.Current.User.Identity.Name;
                //}

                if (System.Web.HttpContext.Current.Session["UserName"] != null)
                {
                    log4net.ThreadContext.Properties["UserName"] = System.Web.HttpContext.Current.Session["UserName"];
                }
            }
        }
    }
}
