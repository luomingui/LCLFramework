using System.Security.Principal;

namespace LCL
{
    /// <summary>
    /// 当前用户的接口定义
    /// 为后期扩展预留的接口。
    /// </summary>
    public interface ILCLIdentity : IIdentity
    {
        /// <summary>
        /// 登录标识
        /// </summary>
        string UserCode { get; set; }
    }
}