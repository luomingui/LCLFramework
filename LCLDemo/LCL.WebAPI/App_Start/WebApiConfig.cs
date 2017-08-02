using LCL.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace LCL.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
                new MyControllerSelector(GlobalConfiguration.Configuration));

            //new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration).UseThis();

            // Web API 路由

            //只有一级编目
            config.Routes.MapHttpRoute(
                            name: "AreaApi1",
                            routeTemplate: "api/{area}/{controller}/{id}",
                            defaults: new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                          name: "AreaApi2",
                          routeTemplate: "api/{area}/{controller}/{action}",
                          defaults: new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                  name: "AreaApi3",
                  routeTemplate: "api/{area}/{controller}/{action}/{name}",
                  defaults: new { id = RouteParameter.Optional });
            //包含二级编目
            config.Routes.MapHttpRoute(
                            name: "AreaCategoryApi",
                            routeTemplate: "api/{area}/{category}/{controller}/{id}",
                            defaults: new { id = RouteParameter.Optional });
        }
    }
}
