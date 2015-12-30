using LCL;
using System;
using System.Web;
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
            bool IsFlg = false;
            try
            {
                var identity = HttpContext.Current.Session["LCLPrincipal"];
                if (identity != null)
                {
                    LEnvironment.Principal = (LCLPrincipal)identity;
                    IsFlg = true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("统一认证:", ex);
            }
            return IsFlg;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法
            filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
        }
    }
}
