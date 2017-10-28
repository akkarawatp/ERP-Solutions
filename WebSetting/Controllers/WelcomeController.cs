using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Common.Utilities;
using Common.Resources;
using WebSetting.Models;
using WebSetting.Controllers.Common;
using Entity;
using BusinessLogic;
using Filters;

namespace WebSetting.Controllers
{
    public class WelcomeController : Controller
    {
        private LogMessageBuilder _logMsg = new LogMessageBuilder();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WelcomeController));

        // GET: Welcome
        public ActionResult Index()
        {
            Logger.Info(_logMsg.Clear().Add("Welcome", "Display Welcome Page"));
            return View();
        }
    }
}