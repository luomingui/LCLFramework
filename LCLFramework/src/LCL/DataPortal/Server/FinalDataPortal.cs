using LCL.DomainServices;
using LCL.Reflection;
using System;

namespace LCL.DataPortal.Server
{  
    /// <summary>
    /// 最终调用实体的 IDataPortalServer 门户实现。
    /// </summary>
    public class FinalDataPortal : IDataPortalServer
    {
        /// <summary>
        /// 非仓库类在使用 Fetch 时的回调方法名。
        /// </summary>
        private const string FetchMethod = "QueryBy";
        /// <summary>
        /// 非仓库类在使用 Update 时的回调方法名。
        /// </summary>
        private const string UpdateMethod = "DataPortal_Update";
        /// <summary>
        /// 调用某个类型的查询方法以返回它的数据
        /// Fetches the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public DataPortalResult Fetch(Type objectType, object criteria, DataPortalContext context)
        {
            var obj = Activator.CreateInstance(objectType, true);
            var res = MethodCaller.CallMethodIfImplemented(obj, FetchMethod, criteria);
            if (res != null)
                return new DataPortalResult(res);
            else
                return new DataPortalResult(obj);
        }
        public DataPortalResult Update(object obj, DataPortalContext context)
        {
            var target = obj as AggregateRoot;
            if (target != null)
            {
                target.SaveRoot();
            }
            else if (obj is DomainService)
            {
                (obj as DomainService).ExecuteByDataPortal();
            }
            else
            {
                MethodCaller.CallMethodIfImplemented(obj, UpdateMethod);
            }
            return new DataPortalResult(obj);
        }
        public DataPortalResult Action(Type objectType, string methodName, object criteria, DataPortalContext context)
        {
            var obj = Activator.CreateInstance(objectType, true);
            var res = MethodCaller.CallMethodIfImplemented(obj, methodName, criteria);
            if (res != null)
                return new DataPortalResult(res);
            else
                return new DataPortalResult(obj);
        }
    }
}