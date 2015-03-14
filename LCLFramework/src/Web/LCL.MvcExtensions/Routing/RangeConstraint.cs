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
    public class RangeConstraint<TValue> : IRouteConstraint where TValue : IComparable, IConvertible
    {
        private readonly TValue min;
        private readonly TValue max;
        private readonly bool optional;
        public RangeConstraint(TValue min, TValue max)
            : this(min, max, false)
        {
        }
        public RangeConstraint(TValue min, TValue max, bool optional)
        {
            this.min = min;
            this.max = max;
            this.optional = optional;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                if (values[parameterName] == null)
                {
                    return optional;
                }
                TValue value;
                try
                {
                    value = (TValue)Convert.ChangeType(values[parameterName], typeof(TValue), Culture.Current);
                }
                catch
                {
                    return false;
                }
                bool matched = (min.CompareTo(value) <= 0) && (max.CompareTo(value) >= 0);
                return matched;
            }
            return optional;
        }
    }
}