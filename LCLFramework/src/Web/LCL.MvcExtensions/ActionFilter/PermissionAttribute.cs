using LCL.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 界面上显示的文本
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        public const string NoPermissionView = "NoPermission";
        public PermissionAttribute(string label, string name)
        {
            Label = label;
            Name = name;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //UIShell.RbacManagementPlugin/Users/List
            //{pluginName}/{controller}/{action}

            string pluginName = filterContext.RouteData.GetAreaName();
            string controller = filterContext.RouteData.GetRequiredString("controller");
            string action = filterContext.RouteData.GetRequiredString("action");

            if (string.IsNullOrWhiteSpace(Name)) Name = action;

            bool hasPermission = false;

            hasPermission = PermissionMgr.HasCommand(pluginName, controller, Name);

            if (hasPermission)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult { ViewName = NoPermissionView };
            }
        }
    }
}