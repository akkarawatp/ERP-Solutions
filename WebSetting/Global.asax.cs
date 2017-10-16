using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

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
    }
}
