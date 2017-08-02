using LCL.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using LCL.Domain.Services;
using System.IO;
using LCL.Web.Mvc.Routes;

namespace LCL.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetDataDir();
            EngineContext.Initialize(false);
            //model binders
            ModelBinders.Binders.Add(typeof(BaseLclModel), new LclModelBinder());
            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new LclMetadataProvider();
            //Registering some regular mvc stuff
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void SetDataDir()
        {
            DirectoryInfo baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string data_dir = baseDir.FullName;
            if ((baseDir.Name.ToLower() == "debug" || baseDir.Name.ToLower() == "release")
                && (baseDir.Parent.Name.ToLower() == "bin"))
            {
                data_dir = Path.Combine(baseDir.Parent.Parent.FullName, "App_Data");
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", data_dir);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //忽略静态资源
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            //维持页面请求(我们忽略它,以防止创建一个客户记录)
            string keepAliveUrl = string.Format("{0}keepalive/index", webHelper.GetStoreLocation());
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            LogException(exception);
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            SetWorkingCulture();
        }
        private void SetWorkingCulture()
        {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            string keepAliveUrl = string.Format("{0}keepalive/index", webHelper.GetStoreLocation());
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;
            if (webHelper.GetThisPageUrl(false).StartsWith(string.Format("{0}admin", webHelper.GetStoreLocation()),
               StringComparison.InvariantCultureIgnoreCase))
            {
                Utils.SetTelerikCulture();
            }
            else
            {
                //public store
                //var workContext = EngineContext.Current.Resolve<IWorkContext>();
                //var culture = new CultureInfo(workContext.WorkingLanguage.LanguageCulture);
                //Thread.CurrentThread.CurrentCulture = culture;
                //Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
#if DEBUG
            routes.IgnoreRoute("{*browserlink}", new { browserlink = @".*/arterySignalR/ping" });
#endif
            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "LCL.WebMvc.Controllers" }
            );
        }
        private void LogException(Exception exception)
        {
            Logger.LogError("Application Error: ", exception);
        }

    }
}
