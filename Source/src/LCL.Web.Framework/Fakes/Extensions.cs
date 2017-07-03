using System;
using System.Web;

namespace LCL.Web.Framework.Fakes
{
    public static class Extensions
    {
        public static bool IsFakeContext(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            return httpContext is FakeHttpContext;
        }

    }
}
