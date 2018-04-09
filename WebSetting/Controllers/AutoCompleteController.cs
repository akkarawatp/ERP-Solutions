using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSetting.Controllers.Common;
using System.Web.Mvc;
using BusinessLogic;
using log4net;
using Common.Utilities;
using Entity;


namespace WebSetting.Controllers
{
    public class AutoCompleteController : BaseController
    {
        private const int AutoCompleteMaxResult = 10;

        private CommonFacade _CommonFacade;
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AutoCompleteController));

        //// GET: AutoComplete
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult AutoCompleteSearchPrefixName(string keyword)
        {
            Logger.Info(_logMsg.Clear().SetPrefixMsg("Auto Complete :: Search Prefixname").Add("Keyword", keyword).ToInputLogString());

            try
            {
                _CommonFacade = new CommonFacade();
                List<PrefixNameEntity> result = _CommonFacade.AutoCompleteSearchPrefixName(keyword, AutoCompleteMaxResult);
                return Json(result.Select(r => new {
                    r.PrefixNameId,
                    r.PrefixName
                }));
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occur:\n", ex);
                Logger.Info(_logMsg.Clear().SetPrefixMsg("Auto Complete :: Search Prefixname").ToFailLogString());
                return Error(new HandleErrorInfo(ex, this.ControllerContext.RouteData.Values["controller"].ToString(),
                    this.ControllerContext.RouteData.Values["action"].ToString()));
            }
        }
    }
}