using LCL;
using LCL.Infrastructure;
using LCL.Web.Framework.Security;
using System;
using System.Web.Mvc;

namespace LCL.Web.Framework.Security
{
    /// <summary>
    /// SSL 传输
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class LCLHttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public LCLHttpsRequirementAttribute(SslRequirement sslRequirement)
        {
            this.SslRequirement = sslRequirement;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            // only redirect for GET requests, 
            // otherwise the browser might not propagate the verb and request body correctly.
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            //all pages are forced to be SSL no matter of the specified value
            //this.SslRequirement = SslRequirement.No;

            switch (this.SslRequirement)
            {
                case SslRequirement.Yes:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (!currentConnectionSecured)
                        {
                            //redirect to HTTPS version of page
                            //string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                            string url = webHelper.GetThisPageUrl(true, true);
                            //301 (permanent) redirection
                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                    break;
                case SslRequirement.No:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            //redirect to HTTP version of page
                            //string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                            string url = webHelper.GetThisPageUrl(true, false);
                            //301 (permanent) redirection
                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                    break;
                case SslRequirement.NoMatter:
                    {
                        //do nothing
                    }
                    break;
                default:
                    throw new LException("Not supported SslProtected parameter");
            }
        }

        public SslRequirement SslRequirement { get; set; }
    }
}
