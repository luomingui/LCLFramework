using LCL;
using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebMvcClient
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : PluginMvcApplication
    {
        //public MvcApplication()
        //{
        //    this.AuthorizeRequest += MvcApplication_AuthorizeRequest;
        //}
        //void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        //{
        //    var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
        //    if (formsIdentity != null && formsIdentity.IsAuthenticated && formsIdentity.AuthenticationType == "Forms")
        //    {
        //        HttpContext.Current.User = UIShell.RbacPermissionService.
        //            LCLPrincipal.TryParsePrincipal(HttpContext.Current.Request);
        //        LEnvironment.Principal = HttpContext.Current.User;
        //    }
        //}    
        protected override  void Application_Start(object sender, EventArgs e)
        {   
            base.Application_Start(sender, e);
        }
        public override void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
        public override void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}