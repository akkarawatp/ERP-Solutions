using System;
using System.Web.Mvc;
using System.Collections;
using WebSetting.Controllers.Common;
using WebSetting.Models;
using System.Globalization;
using log4net;
using Common.Utilities;
using Entity;
using BusinessLogic;

namespace WebSetting.Controllers
{
    public class RolesController : BaseController
    {
        private MasterRoleFacase _MasterRoleFacase;
        private CommonFacade _CommonFacade;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RolesController));

        public ActionResult SearchRole()
        {
            SearchRolesViewModel model = new SearchRolesViewModel();
            model.SearchFilter = new RolesSearchFilter
            {
                RoleName = string.Empty,
                PageNo = 1,
                PageSize = 15,
                SortField = "RoleName",
                SortOrder = "ASC"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        ///[CheckUserRole(ScreenCode.SearchCustomer)]
        public ActionResult SearchRole(RolesSearchFilter SearchFilter)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Search Role").Add("Role Name", SearchFilter.RoleName).ToInputLogString());
            try {
                if (ModelState.IsValid)
                {
                    _MasterRoleFacase = new MasterRoleFacase();
                    var model = new SearchRolesViewModel();
                    model.RoleList = _MasterRoleFacase.searchRoleList(SearchFilter);
                    model.SearchFilter = SearchFilter;

                    return PartialView("~/Views/Roles/_RoleList.cshtml", model);
                }

                return Json(new
                {
                    Valid = false,
                    Error = string.Empty,
                    Errors = GetModelValidationErrors()
                });
            }
            catch (Exception ex) {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Search Role").Add("Error Message", ex.Message).ToFailLogString());
                return Error(new HandleErrorInfo(ex, this.ControllerContext.RouteData.Values["controller"].ToString(),
                    this.ControllerContext.RouteData.Values["action"].ToString()));
            }
        }

        [HttpPost]
        public ActionResult NewRole()
        {
            try {
                ViewBag.Title = "New Role";
                RoleModel roleVM = new RoleModel();
                _CommonFacade = new CommonFacade();

                roleVM.ActiveStatusList = new SelectList((IEnumerable)_CommonFacade.GetStatusSelectList(), "Key", "Value");
                return View("MasterRoleForm", roleVM);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("New Role").Add("Error Message", ex.Message).ToFailLogString());
                return Error(new HandleErrorInfo(ex, this.ControllerContext.RouteData.Values["controller"].ToString(),
                    this.ControllerContext.RouteData.Values["action"].ToString()));
            }
        }

        [HttpPost]
        public ActionResult EditRole(long roleId)
        {
            try
            {
                ViewBag.Title = "Edit Role";
                RoleModel roleVM = getMasterRoleModel(roleId);
                return View("MasterRoleForm", roleVM);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("New Role").Add("Error Message", ex.Message).ToFailLogString());
                return Error(new HandleErrorInfo(ex, this.ControllerContext.RouteData.Values["controller"].ToString(),
                    this.ControllerContext.RouteData.Values["action"].ToString()));
            }
        }

        private RoleModel getMasterRoleModel(long roleId)
        {
            RoleModel ret = new RoleModel();
            _MasterRoleFacase = new MasterRoleFacase();
            _CommonFacade = new CommonFacade();
            RoleEntity role = _MasterRoleFacase.getMasterRoleEntity(roleId);
            if (role != null) {
                ret.roleID = role.RoleId;
                ret.roleName = role.RoleName;
                ret.activeStatus = role.ActiveStatus;
                ret.ActiveStatusList = new SelectList((IEnumerable)_CommonFacade.GetStatusSelectList(), "Key", "Value");
            }

            return ret;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveRole(RoleModel model)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Save Role").Add("RoleName", model.roleName).Add("ActiveStatus", model.activeStatus).ToInputLogString());
            try
            {
                if (ModelState.IsValid)
                {
                    RoleEntity roleEntity = new RoleEntity
                    {
                        RoleId = model.roleID,
                        RoleName = model.roleName,
                        ActiveStatus = model.activeStatus,
                        Username = UserInfo.Username
                    };

                    _MasterRoleFacase = new MasterRoleFacase();
                    bool isSuccess = _MasterRoleFacase.SaveRole(roleEntity);
                    _MasterRoleFacase = null;
                    return isSuccess ? Json(new { is_success = true, message = "บันทึก Role สำเร็จ" }) : Json(new { is_success = false, message = "บันทึก Role ไม่สำเร็จ" });
                }

                return Json(new
                {
                    is_success = false,
                    message = string.Empty
                });
            }
            catch (Exception ex) {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Save Area").Add("Error Message", ex.Message).ToFailLogString());
                return Json(new { is_success = false, message = string.Format(CultureInfo.InvariantCulture, "Error : {0}", ex.Message) });
            }
        }
    }
}