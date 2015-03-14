using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    /// <summary>
    /// 自定义登录认证用户的标识对象(实现IIdentity接口) 
    /// </summary>
    public class LCLIdentity : ILCLIdentity, IIdentity
    {
        #region 构造函数
        public LCLIdentity()
        {
            AuthenticationType = "Custom";
            Name = "";
            UserCode = "";
        }
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected LCLIdentity(SerializationInfo info, StreamingContext context) { }
        #endregion
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
    }
}
