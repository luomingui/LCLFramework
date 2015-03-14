using LCL.DataPortal.DataPortalClient;
using System;
using System.Security.Principal;

namespace LCL.DataPortal
{
    /// <summary>
    /// 数据门户。
    /// 内部封装了对数据层的调用，如果是远程，则使用对应的代理来访问，这使得单机版、网络版的调用完全一致。
    /// 1：业务对象约束 必须要有 QueryBy DataPortal_Update 这两个方法。
    /// </summary>
    public static class DataPortalApi
    {
        public static object Action(Type objectType, string methodName, object criteria, DataPortalLocation loc = DataPortalLocation.Dynamic)
        {
            var proxy = GetDataPortalProxy(loc);

            var dpContext = new Server.DataPortalContext(GetPrincipal(), proxy.IsServerRemote);

            Server.DataPortalResult result = null;

            try
            {
                result = proxy.Action(objectType, methodName, criteria, dpContext);
            }
            finally
            {
                if (proxy.IsServerRemote && result != null) { DistributionContext.SetGlobalContext(result.GlobalContext); }
            }
            //不能等于 ReturnObject=null
            return result.ReturnObject;
        }
        public static object Fetch(Type objectType, object criteria, DataPortalLocation loc = DataPortalLocation.Dynamic)
        {
            var proxy = GetDataPortalProxy(loc);

            var dpContext = new Server.DataPortalContext(GetPrincipal(), proxy.IsServerRemote);

            Server.DataPortalResult result = null;

            try
            {
                result = proxy.Fetch(objectType, criteria, dpContext);
            }
            finally
            {
                if (proxy.IsServerRemote && result != null) { DistributionContext.SetGlobalContext(result.GlobalContext); }
            }
            //不能等于 ReturnObject=null
            return result.ReturnObject;
        }
        public static object Update(object obj, DataPortalLocation loc = DataPortalLocation.Dynamic)
        {
            var proxy = GetDataPortalProxy(loc);

            var dpContext = new Server.DataPortalContext(GetPrincipal(), proxy.IsServerRemote);

            var result = proxy.Update(obj, dpContext);

            if (proxy.IsServerRemote) DistributionContext.SetGlobalContext(result.GlobalContext);

            return result.ReturnObject;
        }

        #region Helpers
        private static Type _proxyType;
        private static IDataPortalProxy GetDataPortalProxy(DataPortalLocation loc)
        {
            if (loc == DataPortalLocation.Local) return new LocalProxy();

            if (_proxyType == null)
            {
                string proxyTypeName = DistributionContext.DataPortalProxy;
                if (proxyTypeName == "Local") return new LocalProxy();

                _proxyType = Type.GetType(proxyTypeName, true, true);
            }
            return Activator.CreateInstance(_proxyType) as IDataPortalProxy;
        }
        private static IPrincipal GetPrincipal()
        {
            if (DistributionContext.User == null)
                DistributionContext.User = LEnvironment.Principal;

            return LEnvironment.Principal;

            //if (DistributionContext.AuthenticationType == "Windows")
            //{
            //    // Windows integrated security
            //    return null;
            //}
            //else
            //{
            //    // we assume using the CSLA framework security
            //    return DistributionContext.User;
            //}
        }
        #endregion
    }
}