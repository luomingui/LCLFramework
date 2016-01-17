using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 匿名身份。
    /// </summary>
    [Serializable]
    public class AnonymousPrincipal : IPrincipal
    {
        private AnonymousIdentity _identity = new AnonymousIdentity();

        IIdentity IPrincipal.Identity
        {
            get { return this._identity; }
        }

        bool IPrincipal.IsInRole(string role)
        {
            return false;
        }
    }
}
