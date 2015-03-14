using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace LCL
{
    /// <summary>
    /// 本类只有一个静态属性，该属性提供一个当前上下文可直接访问 KV 对。
    /// 
    /// 上下文是指：
    /// 分布式应用程序的服务端中，某一线程在为一个请求运行服务程序时所有的上下文数据。
    /// 
    /// 当在 Web 模式下时，可以构造一个新的 ServerContextProvider 子类，以 HttpContext.Current.Items 来作为实现。
    /// </summary>
    public class ServerContext
    {
        private static ServerContextProvider _provider = new ServerContextProvider();

        /// <summary>
        /// 设置上下文提供程序。
        /// </summary>
        /// <param name="context"></param>
        public static void SetCurrent(ServerContextProvider context)
        {
            _provider = context;
        }

        /// <summary>
        /// 当前上下文中的所有数据
        /// </summary>
        public static IDictionary<string, object> Items
        {
            get
            {
                var ctx = _provider.GetValueContainer();
                if (ctx == null)
                {
                    ctx = new Dictionary<string, object>();
                    _provider.SetValueContainer(ctx);
                }

                return ctx;
            }
        }

        /// <summary>
        /// 清空上下文数据
        /// </summary>
        public static void Clear()
        {
            _provider.SetValueContainer(null);
        }
    }
}