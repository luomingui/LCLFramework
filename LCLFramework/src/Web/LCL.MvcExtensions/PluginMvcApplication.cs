using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace LCL.MvcExtensions
{
    public class PluginMvcApplication : System.Web.HttpApplication
    {
        public PluginMvcApplication()
        {
            this.AuthorizeRequest += MvcApplication_AuthorizeRequest;
        }
        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
            if (formsIdentity != null && formsIdentity.IsAuthenticated && formsIdentity.AuthenticationType == "Forms")
            {
                HttpContext.Current.User =
                     MyFormsAuthentication<MyUserDataPrincipal>.TryParsePrincipal(HttpContext.Current.Request);
                LEnvironment.Principal = HttpContext.Current.User;
            }
        }  
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ControllerBuilder.Current.SetControllerFactory(new PluginControllerFactory());
            ViewEngines.Engines.Add(new PluginRuntimeViewEngine(new PluginRazorViewEngineFactory()));
            ViewEngines.Engines.Add(new PluginRuntimeViewEngine(new PluginWebFormViewEngineFactory()));

            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new LCLMetadataProvider();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);

            DefaultControllerConfig.Register(typeof(PluginMvcApplication).Assembly);
        }
        public virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackPageLoadPerformanceAttribute());
        }
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // Route name
                "{pluginName}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}
