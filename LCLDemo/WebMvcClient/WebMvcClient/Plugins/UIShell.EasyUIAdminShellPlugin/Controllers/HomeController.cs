using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UIShell.RbacPermissionService;

namespace UIShell.EasyUIAdminShellPlugin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
	}
}