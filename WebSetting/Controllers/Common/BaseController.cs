using System.Globalization;
using System.Web;
using System.Web.Mvc;
//using BusinessLogic;
using Common.Resources;
//using Common.Utilities;
using Entity;
using WebSetting.Models;
using log4net;
using System;
using System.Collections.Generic;

namespace WebSetting.Controllers.Common
{
    public class BaseController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseController));

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    ViewBag.CallId = !string.IsNullOrEmpty(this.CallId) ? this.CallId : Constants.NotKnown;
        //    ViewBag.PhoneNo = !string.IsNullOrEmpty(this.PhoneNo) ? this.PhoneNo : Constants.NotKnown;
        //    base.OnActionExecuting(filterContext);
        //}

        protected UserEntity UserInfo
        {
            get
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    UserIdentity id = HttpContext.User.Identity as UserIdentity;
                    return id.UserInfo;
                }

                return null;
            }
        }

        public Dictionary<string, object> GetModelValidationErrors()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                    errors[key] = ModelState[key].Errors[0].ErrorMessage;
            }

            return errors;
        }

        public ActionResult Error(HandleErrorInfo handleError)
        {
            if (handleError != null)
            {
                var controllerName = handleError.ControllerName;
                var actionName = handleError.ActionName;
                Logger.Error("Exception occur: Controller Name-" + controllerName + " Action Name-" + actionName + "\n", handleError.Exception);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    Valid = false,
                    Error = Resources.Error_System
                });
            }

            return View("~/Views/Shared/Error.cshtml", handleError);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                if (filterContext.ExceptionHandled)
                {
                    return;
                }

                filterContext.ExceptionHandled = true;
                Exception exception = filterContext.Exception;

                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                if (Request.IsAjaxRequest())
                {
                    filterContext.Result = Json(new
                    {
                        Valid = false,
                        Error = Resources.Error_System
                    });
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(model),
                        TempData = filterContext.Controller.TempData
                    };
                }

                Logger.Error("Exception occur:\n", exception);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
            }
        }

        protected void ClearSession()
        {
            // Clear cache
            var username = System.Web.HttpContext.Current.Session["UserName"] != null ? System.Web.HttpContext.Current.Session["UserName"].ToString() : string.Empty;
            if (!string.IsNullOrWhiteSpace(username))
            {
                var cacheKey = string.Format(CultureInfo.InvariantCulture, "{0}_user_info", username);
                if (HttpRuntime.Cache[cacheKey] != null) HttpRuntime.Cache.Remove(cacheKey);
            }

            System.Web.HttpContext.Current.Session["sessionid"] = null;
            System.Web.HttpContext.Current.Session["user_id"] = null;
            System.Web.HttpContext.Current.Session["UserName"] = null;
            System.Web.HttpContext.Current.Session["login_his_id"] = null;

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
        }
    }
}
