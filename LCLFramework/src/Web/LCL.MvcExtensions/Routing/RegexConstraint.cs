/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    public class RegexConstraint : IRouteConstraint
    {
        private readonly Regex expression;
        private readonly bool optional;
        public RegexConstraint(string expression) : this(expression, false)
        {
        }
        public RegexConstraint(string expression, bool optional)
        {
            string pattern = "^(" + expression + ")$";
            this.expression = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            this.optional = optional;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value = values[parameterName];
            return value == null ? optional : expression.IsMatch(value.ToString());
        }
    }
}