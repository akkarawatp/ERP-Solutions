using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSetting.Controllers
{
    public class MenuNavigatorController : Controller
    {
        // GET: MenuNavigator
        public ActionResult MainMenu()
        {
            return PartialView("~/Views/MenuNavigator/MainMenu.cshtml");
        }
    }
}