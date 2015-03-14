/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public static class RouteDictionaryExtensions
    {
        public static void IgnoreRoute(this IDictionary<string, RouteBase> routes, string url)
        {
            IgnoreRoute(routes, url, null /* constraints */);
        }

        public static void IgnoreRoute(this IDictionary<string, RouteBase> routes, string url, object constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            IgnoreRouteInternal route = new IgnoreRouteInternal(url)
            {
                Constraints = new RouteValueDictionary(constraints)
            };

            routes.Add(Guid.NewGuid().ToString(), route);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url)
        {
            return MapRoute(routes, name, url, null /* defaults */, (object)null /* constraints */);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url, object defaults)
        {
            return MapRoute(routes, name, url, defaults, (object)null /* constraints */);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url, object defaults, object constraints)
        {
            return MapRoute(routes, name, url, defaults, constraints, null /* namespaces */);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url, string[] namespaces)
        {
            return MapRoute(routes, name, url, null /* defaults */, null /* constraints */, namespaces);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapRoute(routes, name, url, defaults, null /* constraints */, namespaces);
        }

        public static Route MapRoute(this IDictionary<string, RouteBase> routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            Route route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens = new RouteValueDictionary();
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }

        private sealed class IgnoreRouteInternal : Route
        {
            public IgnoreRouteInternal(string url)
                : base(url, new StopRoutingHandler())
            {
            }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
            {
                // Never match during route generation. This avoids the scenario where an IgnoreRoute with
                // fairly relaxed constraints ends up eagerly matching all generated URLs.
                return null;
            }
        }
    }
}
