using System.Web.Routing;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
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
