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
using System.Web;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    public class GuidConstraint : IRouteConstraint
    {
        private readonly bool optional;
        public GuidConstraint() : this(false)
        {
        }
        public GuidConstraint(bool optional)
        {
            this.optional = optional;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value = values[parameterName];
            if (value == null)
            {
                return optional;
            }
            bool matched = false;
            Guid guid;
            if (Guid.TryParse(value.ToString(), out guid) && (guid != Guid.Empty))
            {
                matched = true;
            }
            return matched;
        }
    }
}