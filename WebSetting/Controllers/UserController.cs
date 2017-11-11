using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net;
using Common.Utilities;
using Common.Resources;
using Common.Securities;
using WebSetting.Models;
using WebSetting.Controllers.Common;
using Entity;
using BusinessLogic;
using BusinessLogic.Config;
using Filters;


namespace WebSetting.Controllers
{
    public class UserController : BaseController
    {
        private UserFacade userFacade;
        private CommonFacade _CommonFacade;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserController));

        #region "User Master Data"
        public ActionResult SearchUser()
        {
            _CommonFacade = new CommonFacade();
            SearchUserViewModel model = new SearchUserViewModel();
            var activeStatusList = _CommonFacade.GetStatusSelectList(Resources.Ddl_Status_All, Constants.ApplicationStatus.All);
            model.ActiveStatusList = new SelectList((IEnumerable)activeStatusList, "Key", "Value", string.Empty);
            model.SearchFilter = new UserSearchFilter
            {
                Username = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                OrganizeName = string.Empty,
                DepartmentName = string.Empty,
                PositionName = string.Empty,
                ActiveStatus = string.Empty,
                PageNo = 1,
                PageSize = 15,
                SortField = "FullName",
                SortOrder = "ASC"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        ///[CheckUserRole(ScreenCode.SearchCustomer)]
        public ActionResult SearchUser(UserSearchFilter SearchFilter)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Search User").Add("Username", SearchFilter.Username)
                .Add("FirstName",SearchFilter.FirstName).Add("LastName",SearchFilter.LastName)
                .Add("OrganizeName", SearchFilter.OrganizeName).Add("DepartmentName",SearchFilter.DepartmentName)
                .Add("PositionName", SearchFilter.PositionName).ToInputLogString());
            try
            {
                if (ModelState.IsValid)
                {
                    _CommonFacade = new CommonFacade();
                    userFacade = new UserFacade();
                    var model = new SearchUserViewModel();
                    var activeStatusList = _CommonFacade.GetStatusSelectList(Resources.Ddl_Status_All, Constants.ApplicationStatus.All);
                    model.ActiveStatusList = new SelectList((IEnumerable)activeStatusList, "Key", "Value", string.Empty);
                    model.UserList = userFacade.searchUserList(SearchFilter);
                    model.SearchFilter = SearchFilter;

                    return PartialView("~/Views/User/_UserList.cshtml", model);
                }

                return Json(new
                {
                    Valid = false,
                    Error = string.Empty,
                    Errors = GetModelValidationErrors()
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Search Role").Add("Error Message", ex.Message).ToFailLogString());
                return Error(new HandleErrorInfo(ex, this.ControllerContext.RouteData.Values["controller"].ToString(),
                    this.ControllerContext.RouteData.Values["action"].ToString()));
            }
        }
        #endregion;


            #region "User Login Function"
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // So that the user can be referred back to where they were when they click logon
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = HttpUtility.UrlDecode(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel userModel, string returnUrl)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("UserName", userModel.UserName).ToInputLogString());

            try
            {
                // Validate the input data
                IDictionary<string, object> errorList = null;

                if (ModelState.IsValid)
                {
                    userFacade = new UserFacade();
                    DateTime loginTime = DateTime.Now;
                    UserEntity user = userModel.IsValid(userModel.UserName, StringCipher.EncryptTxt(userModel.Password), loginTime);
                    if (user != null && user.UserId > 0)
                    {
                        if (userFacade.UpdateLastLoginTime(user.UserId, loginTime) == true)
                        {
                            string ClientIP = ApplicationHelpers.GetClientIP();
                            string ClientBrowser = ApplicationHelpers.GetClientBrowser();
                            string ServerUrl = ApplicationHelpers.GetServerUrl();
                            string sid = System.Web.HttpContext.Current.Session.SessionID;

                            System.Web.HttpContext.Current.Session["sessionid"] = sid;
                            System.Web.HttpContext.Current.Session["user_id"] = user.UserId;
                            System.Web.HttpContext.Current.Session["UserName"] = user.Username;
                            System.Web.HttpContext.Current.Session["login_his_id"] = userFacade.SaveLoginHistory(user, sid, WebConfig.GetSystemCode(), loginTime, ClientIP, ClientBrowser, ServerUrl);
                        }

                        if (user.ForceChangePsswd == "N")
                        {
                            //returnURL needs to be decoded
                            string decodedUrl = string.Empty;
                            if (!string.IsNullOrEmpty(returnUrl))
                                decodedUrl = Server.UrlDecode(returnUrl);

                            // Login logic
                            SetupFormsAuthTicket(userModel.UserName, user.UserId);
                            Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("UserId", user.UserId).ToSuccessLogString());

                            if (Url.IsLocalUrl(decodedUrl))
                            {
                                return Redirect(decodedUrl);
                            }

                            return RedirectToAction("Index", "Welcome");
                        }
                        else
                        {
                            return ForceChangePassword(user.Username);
                        }
                    }
                    else
                    {
                        userModel.ErrorMessage = Resources.Msg_LoginFailed;
                        Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("Error Message", userModel.ErrorMessage).ToFailLogString());
                        goto Outer;
                    }
                }

                // Failed Validation
                errorList = GetModelValidationErrors();
                userModel.ErrorMessage = String.Join("<br>", errorList.Select(o => o.Value));
            }
            catch (CustomException cex)
            {
                userModel.ErrorMessage = cex.Message == Resources.Msg_CannotConnectToDB ? Resources.Msg_LoginFailed : cex.Message;
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Login").Add("Error Message", userModel.ErrorMessage).ToFailLogString());
            }

            Outer:
            ViewBag.ReturnUrl = returnUrl;
            return View(userModel);
        }

        //[CheckUserSession]
        public ActionResult ForceChangePassword(string Username) {
            UserChangePsswdModel pModel = new UserChangePsswdModel();
            pModel.UserName = Username;
            pModel.ForceChangePsswd = "Y";

            return View("ChangePassword", pModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserChangePsswdModel pModel) {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("ChangePassword").Add("UserName", pModel.UserName).ToInputLogString());
            try {
                // Validate the input data
                IDictionary<string, object> errorList = null;
                if (ModelState.IsValid)
                {
                    if (System.Web.HttpContext.Current.Session["login_his_id"] != null) {
                        long LoginHisId = Convert.ToInt64(System.Web.HttpContext.Current.Session["login_his_id"]);
                        long userId = Convert.ToInt64(System.Web.HttpContext.Current.Session["user_id"]);
                        userFacade = new UserFacade();
                        if (userFacade.UpdateChangePsswd(pModel.UserName, LoginHisId, System.Web.HttpContext.Current.Session["UserName"].ToString(), pModel.NewPassword, WebConfig.GetSystemCode()) == false) {
                            pModel.ErrorMessage = "Logout Fail";
                        }
                    }
                }
                else {
                    // Failed Validation
                    errorList = GetModelValidationErrors();
                    pModel.ErrorMessage = String.Join("<br>", errorList.Select(o => o.Value));
                }
            }
            catch (Exception ex) {
                Logger.Error("Exception occur:\n", ex);
            }

            return View(pModel);
        }

        [HttpPost]
        [CheckUserSession]
        public ActionResult Logout()
        {
            string loginUrl = FormsAuthentication.LoginUrl;
            long userId = this.UserInfo != null ? this.UserInfo.UserId : 0;

            try
            {
                userFacade = new UserFacade();
                bool ret = userFacade.UpdateLogoutTime(Convert.ToInt64(System.Web.HttpContext.Current.Session["login_his_id"]), DateTime.Now, true);
                if (ret == true)
                {
                    Logger.Info(_logMsg.Clear().SetPrefixMsg("Logout").Add("UserID", userId).ToInputLogString());
                    ClearSession();
                    FormsAuthentication.SignOut();
                    Logger.Info(_logMsg.Clear().SetPrefixMsg("Logout").ToSuccessLogString());
                }
                else {
                    Logger.Info(_logMsg.Clear().SetPrefixMsg("Logout").Add("Error Message", "Update Logout Time fail").ToFailLogString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Logout").Add("Exception Message", ex.Message).ToFailLogString());
            }
            return Redirect(loginUrl);
        }

        [HttpGet]
        [CheckUserSession]
        public ActionResult AccessDenied()
        {
            return View();
        }

        private bool SetupFormsAuthTicket(string userName, long userId)
        {
            var userData = userId.ConvertToString();
            var authTicket = new FormsAuthenticationTicket(1, //version
                                                        userName, // user name
                                                        DateTime.Now,             //creation
                                                        DateTime.Now.AddMinutes(30), //Expiration
                                                        false, //Persistent
                                                        userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return true;

        }
        #endregion;
    }
}