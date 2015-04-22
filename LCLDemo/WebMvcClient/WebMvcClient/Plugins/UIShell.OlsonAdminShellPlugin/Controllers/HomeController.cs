using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UIShell.OlsonAdminShellPlugin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowOrHideNav()
        {
            AppConfig.NavHidden = !AppConfig.NavHidden;
            return new JsonResult { Data = new { Hidden = AppConfig.NavHidden }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


	}
}