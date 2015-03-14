using LCL;
using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
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