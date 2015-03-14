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
using System.Web;

namespace LCL.MvcExtensions
{
    public class FastRoute : RouteBase
    {
        public FastRoute(string url, IRouteHandler routeHandler)
            : this(url, null, routeHandler)
        { }

        public FastRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : this(url, defaults, null, routeHandler)
        { }

        public FastRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
        {
            this.ParsingRoute = new Route(url, defaults, constraints, routeHandler);

            this.Url = url;
            this.Defaults = defaults ?? new RouteValueDictionary();
            this.Constraints = constraints ?? new RouteValueDictionary();
            this.RouteHandler = routeHandler;

            this.m_tokenBuilder = new PathBuilder(this.Url, this.Defaults, this.Constraints);
        }

        private PathBuilder m_tokenBuilder;
        public RouteValueDictionary Constraints { get; private set; }
        public RouteValueDictionary Defaults { get; private set; }
        public IRouteHandler RouteHandler { get; set; }
        public string Url { get; set; }
        public RouteBase ParsingRoute { get; set; }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var data = this.ParsingRoute.GetRouteData(httpContext);

            if (data == null) return null;

            data.Route = this;
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var token = this.m_tokenBuilder.Build(values);
            if (String.IsNullOrEmpty(token)) return null;
            return new VirtualPathData(this, token);
        }
    }
}
