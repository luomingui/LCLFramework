using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LCL.WebMvc.Code
{
    public class MemberProviderEX
    {
        public static MembershipUser GetCurrentUser()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null && httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                return Membership.GetUser();
            }

            return null;
        }

        /// <summary>
        /// Safe check of authenticity. Better than Request.IsAuthenticated in that if there's a used-to-be-valid cookie which does not correspond to the current database, it will fail safe
        /// </summary>
        /// <returns></returns>
        public static bool IsUserAuthenticated()
        {
            if (HttpContext.Current == null)
                return false;

            var request = HttpContext.Current.Request;

            if (!request.IsAuthenticated)
                return false;

            var membershipUser = GetCurrentUser();

            if (membershipUser != null)
                return true;

            return false;
        }
    }
}