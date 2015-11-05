using LCL;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    ///  验证用户是否登陆
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            try
            {
                if (LCL.LEnvironment.Principal.Identity.Name.Length > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //if (httpContext != null)
                //{
                //    if (httpContext.User.Identity.IsAuthenticated)
                //    {
                //        return true;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                Logger.LogError("统一认证:", ex);
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法
            filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
        }
    }
}
