using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 服务器端上下文提供器。
    /// 默认实现：一个标记了 ThreadStatic 的字段。
    /// </summary>
    public class ServerContextProvider
    {
        [ThreadStatic]
        private static IDictionary<string, object> _items;

        protected internal virtual IDictionary<string, object> GetValueContainer()
        {
            return _items;
        }

        protected internal virtual void SetValueContainer(IDictionary<string, object> value)
        {
            _items = value;
        }

        //protected virtual IDictionary GetLocalContext()
        //{
        //    var slot = Thread.GetNamedDataSlot(LocalContextName);
        //    return (IDictionary)Thread.GetData(slot);
        //}

        //protected virtual void SetLocalContext(IDictionary localContext)
        //{
        //    var slot = Thread.GetNamedDataSlot(LocalContextName);
        //    Thread.SetData(slot, localContext);
        //}
    }
}
