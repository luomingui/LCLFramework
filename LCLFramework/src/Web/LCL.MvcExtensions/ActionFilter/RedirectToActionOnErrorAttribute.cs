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
using System.Web.Mvc;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 重定向活动错误
    /// </summary>
    public class RedirectToActionOnErrorAttribute : RedirectOnErrorAttribute
    {

        public Type Controller { get; set; }
        public string Action { get; set; }

        protected override bool Validate(ActionExecutedContext filterContext)
        {

            //### the url property is always needed
            if (
                Controller == null ||
                (string.IsNullOrEmpty(Action) || Action.Trim() == string.Empty)
            )
                throw new ArgumentNullException("RedirectToUrlOnActionAttribute's Controller and Action properties must have values.");

            //### make sure the Contoller property is a Controller
            if (!typeof(System.Web.Mvc.Controller).IsAssignableFrom(Controller))
                throw new ArgumentException("RedirectToUrlOnActionAttribute's Controller property's value must derive from System.Web.Mvc.Controller.");

            //### continue processing
            return true;

        }

        protected override void Redirect(ActionExecutedContext filterContext)
        {

            //### turn "Foo.Foo.Foo.BarController" into "Bar"
            string controllerName = Controller.ToString();
            controllerName = controllerName.Substring(controllerName.LastIndexOf(".") + 1);
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));

            //### turn route data into url
            RouteValueDictionary rvd = new RouteValueDictionary(new
            {
                controller = controllerName,
                action = Action
            });
            ControllerContext ctx = new ControllerContext(
                filterContext.HttpContext,
                filterContext.RouteData,
                filterContext.Controller
            );
            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(ctx.RequestContext, rvd);
            string url = vpd.VirtualPath;

            //### redirect
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Redirect(url, true);

        }

    }
}
