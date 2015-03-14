
using LCL.DataPortal.Server;
namespace LCL.DataPortal.DataPortalClient
{
    /// <summary>
    /// 客户端的代理
    /// 它同样有 IDataPortalServer 的所有方法。
    /// </summary>
    public interface IDataPortalProxy :IDataPortalServer
    {
        /// <summary>
        /// Get a value indicating whether this proxy will invoke
        /// a remote data portal server, or run the "server-side"
        /// data portal in the caller's process and AppDomain.
        /// </summary>
        bool IsServerRemote { get; }
    }
}
