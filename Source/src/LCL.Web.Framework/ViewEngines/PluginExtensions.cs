using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.Web.Framework.ViewEngines
{
    public static class PluginExtensions
    {
        public static string GetPluginSymbolicName(this System.Web.Routing.RequestContext requestContext)
        {
            return requestContext.HttpContext.GetPluginSymbolicName();
        }

        public static string GetPluginSymbolicName(this ControllerContext requestContext)
        {
            return requestContext.HttpContext.GetPluginSymbolicName();
        }

        public static string GetPluginSymbolicName(this HttpContextBase context)
        {
            string areaName = context.Request.RequestContext.RouteData.GetAreaName();
            if (areaName != null)
            {
                if (areaName.Contains("Plugin"))
                    return areaName;
                else
                    return areaName + "Plugin";
            }
            return context.Request.QueryString["plugin"];
        }

        public static string GetAreaName(this RouteBase route)
        {
            IRouteWithArea routeWithArea = route as IRouteWithArea;
            if (routeWithArea != null)
            {
                return routeWithArea.Area;
            }
            Route castRoute = route as Route;
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["area"] as string;
            }
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["plugin"] as string;
            }
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["pluginName"] as string;
            }
            return null;
        }
    }

    public static class RouteExtension
    {
        public static string GetAreaName(this RouteBase route)
        {
            var routeWithArea = route as IRouteWithArea;
            if (routeWithArea != null)
            {
                return routeWithArea.Area;
            }
            var castRoute = route as Route;
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["area"] as string;
            }
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["plugin"] as string;
            }
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["pluginName"] as string;
            }
            return null;
        }

        public static string GetAreaName(this RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
            {
                return area as string;
            }
            if (routeData.DataTokens.TryGetValue("plugin", out area))
            {
                return area as string;
            }
            if (routeData.DataTokens.TryGetValue("pluginName", out area))
            {
                return area as string;
            }
            if (routeData.Values.ContainsKey("pluginName"))
            {
                return routeData.GetRequiredString("pluginName");
            }
            return GetAreaName(routeData.Route);
        }
    }
}