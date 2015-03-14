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
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    public class EnumConstraint<TEnum> : IRouteConstraint where TEnum : IComparable, IFormattable, IConvertible
    {
        private readonly bool optional;

        public EnumConstraint() : this(false)
        {
        }
        public EnumConstraint(bool optional)
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
            bool matched = Enum.GetNames(typeof(TEnum)).Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            return matched;
        }
    }
}