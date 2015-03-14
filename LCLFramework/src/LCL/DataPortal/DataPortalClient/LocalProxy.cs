using LCL.DataPortal.Server;
using System;

namespace LCL.DataPortal.DataPortalClient
{
    /// <summary>
    /// 当机版本的访问代理类。
    /// </summary>
    public class LocalProxy : IDataPortalProxy
    {
        public bool IsServerRemote
        {
            get { return false; }
        }

        public DataPortalResult Fetch(Type objectType, object criteria, DataPortalContext context)
        {
            var server = new DataPortalFacade();
            try
            {
                LEnvironment.ThreadPortalCount++;
                return server.Fetch(objectType, criteria, context);
            }
            finally
            {
                LEnvironment.ThreadPortalCount--;
            }
        }

        public DataPortalResult Update(object obj, DataPortalContext context)
        {
            /*********************** 代码块解释 *********************************
             * 
             * 由于开发人员平时会使用单机版本开发，而正式部署时，又会选用 C/S 架构。
             * 所以需要保证单机版本和 C/S 架构版本的模式是一样的。也就是说，在单机模式下，
             * 在通过门户访问时，模拟网络版，clone 出一个新的对象。
             * 这样，在底层 Update 更改 obj 时，不会影响上层的实体。
             * 而是以返回值的形式把这个被修改的实体返回给上层。
             * 
             * 20120828 
             * 但是，当在服务端本地调用时，不需要此模拟功能。
             * 这是因为在服务端本地调用时（例如服务端本地调用 RF.Save），
             * 在开发体验上，数据层和上层使用的实体应该是同一个，数据层的修改应该能够带回到上层，不需要克隆。
             * 
            **********************************************************************/

            //ThreadPortalCount == 0 表示第一次进入数据门户
            if (LEnvironment.Location.IsWPFUI && LEnvironment.Location.ConnectDataDirectly &&
                LEnvironment.ThreadPortalCount == 1)
            {
                obj = ObjectCloner.Clone(obj);
            }

            var server = new DataPortalFacade();
            try
            {
                LEnvironment.ThreadPortalCount++;
                return server.Update(obj, context);
            }
            finally
            {
                LEnvironment.ThreadPortalCount--;
            }
        }

        public DataPortalResult Action(Type objectType, string methodName, object criteria, DataPortalContext context)
        {
            var server = new DataPortalFacade();
            try
            {
                LEnvironment.ThreadPortalCount++;
                return server.Action(objectType, methodName, criteria, context);
            }
            finally
            {
                LEnvironment.ThreadPortalCount--;
            }
        }
    }
}
