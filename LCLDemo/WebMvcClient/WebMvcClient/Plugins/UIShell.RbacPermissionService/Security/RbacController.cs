using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace UIShell.RbacPermissionService
{
    //[LogActionFilter]
    //[MyAuthorizeAttribute]
    public class RbacController : BaseController
    {
        public GeneralConfigInfo Config
        {
            get
            {
                GeneralConfigInfo config = GeneralConfigs.GetConfig();
                return config;
            }
        }
        public RbacController()
        {
            PermissionMgr.IsOpenPermission = false;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(LCL.LEnvironment.Principal.Identity.Name)
                && LCL.LEnvironment.Principal.Identity.Name.Length > 1)
            {
          
            }
            else
            {
                string loginUrl = FormsAuthentication.LoginUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(filterContext.GetPluginSymbolicName() + "插件错误信息：", filterContext.Exception);
        }

    }
    //[LogActionFilter]
    //[MyAuthorizeAttribute]
    public class RbacController<TEntity> : BaseRepoController<TEntity> where TEntity : class, IAggregateRoot
    {
        public GeneralConfigInfo Config
        {
            get
            {
                GeneralConfigInfo config = GeneralConfigs.GetConfig();
                return config;
            }
        }
        public RbacController()
        {
            PermissionMgr.IsOpenPermission = false;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(LCL.LEnvironment.Principal.Identity.Name)
                && LCL.LEnvironment.Principal.Identity.Name.Length > 1)
            {
              
            }
            else
            {
                string loginUrl = FormsAuthentication.LoginUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(filterContext.GetPluginSymbolicName() + "插件错误信息：", filterContext.Exception);
        }
    }
}