using LCL.Infrastructure;
using LCL.Web.Mvc.Routes;
using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LCL.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetDataDir();
            EngineContext.Initialize(false);
            //model binders
            ModelBinders.Binders.Add(typeof(BaseLclModel), new LclModelBinder());
            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new LclMetadataProvider();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
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
    }
}
