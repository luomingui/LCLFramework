using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.MvcExtensions
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
}