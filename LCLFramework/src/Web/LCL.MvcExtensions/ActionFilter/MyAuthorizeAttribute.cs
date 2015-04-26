using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace LCL.MvcExtensions
{
    //验证角色和用户名的类
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return LCL.LEnvironment.Principal.Identity.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法

            filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
        }
    }
}
