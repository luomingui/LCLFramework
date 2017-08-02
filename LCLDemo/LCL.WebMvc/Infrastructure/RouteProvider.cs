using System.Web.Mvc;
using System.Web.Routing;
using LCL.Web.Localization;
using LCL.Web.Mvc.Routes;

namespace LCL.Web.Mvc.Routes
{
    /// <summary>
    /// 路由文件
    /// </summary>
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //login
            routes.MapLocalizedRoute("Login",
                            "login/",
                            new { controller = "Customer", action = "Login" },
                            new[] { "LCL.Plugin.Rbac.Controllers" });
            //register
            routes.MapLocalizedRoute("Register",
                            "register/",
                            new { controller = "Customer", action = "Register" },
                            new[] { "LCL.Plugin.Rbac.Controllers" });
            //logout
            routes.MapLocalizedRoute("Logout",
                            "logout/",
                            new { controller = "Customer", action = "Logout" },
                            new[] { "LCL.Plugin.Rbac.Controllers" });

        }


        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
