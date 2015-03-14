
using System;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace LCL
{
    /// <summary>
    /// 匿名用户
    /// </summary>
    [Serializable]
    public class AnonymousIdentity : IIdentity, ILCLIdentity
    {
        public AnonymousIdentity()
        {
            UserCode = string.Empty;
        }
        string IIdentity.AuthenticationType
        {
            get { return string.Empty; }
        }

        bool IIdentity.IsAuthenticated
        {
            get { return false; }
        }

        string IIdentity.Name
        {
            get { return string.Empty; }
        }
        public string UserCode { get; set; }
       
    }
}