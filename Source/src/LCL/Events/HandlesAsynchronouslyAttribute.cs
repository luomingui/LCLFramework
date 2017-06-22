using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Domain.Events
{
    /// <summary>
    /// 表示应用该属性的事件处理程序将处理异步流程中的事件。 
    /// 此属性仅适用于消息处理程序被消息总线或消息调度程序使用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute : Attribute
    {

    }
}
