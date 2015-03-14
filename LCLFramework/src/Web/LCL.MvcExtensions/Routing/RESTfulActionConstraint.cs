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
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    public class RESTFulActionConstraint : IRouteConstraint
    {
        public const string IdParameterName = "id";

        private static readonly IEnumerable<HttpVerbs> knownVerbs = Enum.GetValues(typeof(HttpVerbs)).Cast<HttpVerbs>();

        private readonly IEnumerable<string> verbs;

        public RESTFulActionConstraint(HttpVerbs httpVerb) : this(httpVerb, false)
        {
        }
        public RESTFulActionConstraint(HttpVerbs httpVerb, bool requiresId)
        {
            HttpVerbs = httpVerb;
            RequiresId = requiresId;
            verbs = ConvertToStringList(HttpVerbs);
        }
        public HttpVerbs HttpVerbs { get; private set; }
        public bool RequiresId { get; private set; }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration)
            {
                return !RequiresId || HasId(values);
            }
            string originalVerb = httpContext.Request.HttpMethod;
            if ((originalVerb != null) && verbs.Contains(originalVerb, StringComparer.OrdinalIgnoreCase))
            {
                return !RequiresId || HasId(values);
            }
            string overriddenVerb = httpContext.Request.GetHttpMethodOverride();
            bool matched = overriddenVerb != null && verbs.Contains(overriddenVerb, StringComparer.OrdinalIgnoreCase);
            if (matched)
            {
                return !RequiresId || HasId(values);
            }
            return false;
        }
        public override string ToString()
        {
            return string.Join(",", verbs);
        }
        private static IEnumerable<string> ConvertToStringList(HttpVerbs verb)
        {
            IList<string> list = new List<string>();
            Action<HttpVerbs> append = matching =>
            {
                if ((verb & matching) != 0)
                {
                    list.Add(matching.ToString().ToUpperInvariant());
                }
            };
            foreach (HttpVerbs known in knownVerbs)
            {
                append(known);
            }
            return list;
        }

        private static bool HasId(IDictionary<string, object> values)
        {
            if (!values.ContainsKey(IdParameterName))
            {
                return false;
            }
            object value = values[IdParameterName];
            return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}