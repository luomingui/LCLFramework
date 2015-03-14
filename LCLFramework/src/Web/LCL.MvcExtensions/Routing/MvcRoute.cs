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
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
	[Obsolete]
	public class MvcRoute : Route
	{
		private MvcRoute(string url)
			: base(url, new MvcRouteHandler())
		{
			Constraints = new RouteValueDictionary();
			Defaults = new RouteValueDictionary();
		}
		public static MvcRoute MapUrl(string url)
		{
			return new MvcRoute(url);
		}
		[Obsolete("Use MapUrl instead")]
		public static MvcRoute MappUrl(string url)
		{
			return new MvcRoute(url);
		}
		public MvcRoute ToDefaultAction<T>(Expression<Func<T, ActionResult>> action, object defaults) where T : IController
		{
			foreach(var pair in new RouteValueDictionary(defaults))
			{
				Defaults.Add(pair.Key, pair.Value);
			}

			return this;
		}
		public MvcRoute WithConstraints(object constraints)
		{
			foreach(var pair in new RouteValueDictionary(constraints))
			{
				Constraints.Add(pair.Key, pair.Value);
			}

			return this;
		}
		public MvcRoute WithDefaults(object defaults)
		{
			foreach(var pair in new RouteValueDictionary(defaults))
			{
				Defaults.Add(pair.Key, pair.Value);
			}

			return this;
		}
		public MvcRoute WithNamespaces(string[] namespaces)
		{
			if(namespaces == null)
			{
				throw new ArgumentNullException("namespaces");
			}

			DataTokens = new RouteValueDictionary();
			DataTokens["Namespaces"] = namespaces;

			return this;
		}
		public MvcRoute AddWithName(string routeName, RouteCollection routes)
		{
			routes.Add(routeName, this);
			return this;
		}
	}
}