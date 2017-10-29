using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using log4net;
using Common.Utilities;
using Entity;
using WebSetting.Models;
using BusinessLogic;

namespace WebSetting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private int _cacheTimeoutInMinute = 20;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_BeginRequest()
        {
            System.Web.HttpContext.Current.Response.AddHeader("P3P", "CP=\"NOI CURa ADMa DEVa TAIa OUR BUS IND UNI COM NAV INT\"");
        }

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

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //Logger.Debug("--START--:--Get User--"); 

            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!string.IsNullOrEmpty(encTicket))
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket);
                    var id = new UserIdentity(ticket);
                    UserFacade userFacade = null;

                    try
                    {
                        UserEntity userInfo = null;
                        var cacheKey = string.Format(CultureInfo.InvariantCulture, "{0}_user_info", id.Name);

                        if (HttpRuntime.Cache[cacheKey] != null)
                        {
                            userInfo = (UserEntity)HttpRuntime.Cache[cacheKey];
                        }
                        else
                        {
                            userFacade = new UserFacade();
                            userInfo = userFacade.GetUserByUsername(id.Name);
                            HttpRuntime.Cache.Insert(cacheKey, userInfo, null,
                                DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);
                        }

                        id.UserInfo = userInfo;
                        var userRoles = new string[] { id.UserInfo.RoleIdValue.ConvertToString() };
                        var prin = new GenericPrincipal(id, userRoles);
                        HttpContext.Current.User = prin;
                        //Logger.DebugFormat("--SUCCESS--:--Get User--:Username/{0}", id.UserInfo.Username); 
                    }
                    catch (CustomException cex)
                    {
                        Logger.Error("CustomException occur:\n", cex);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Exception occur:\n", ex);
                    }
                    finally
                    {
                        if (userFacade != null)
                        {
                            userFacade = null;
                        }
                    }
                }
            }
        }
    }
}
