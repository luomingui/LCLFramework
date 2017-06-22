using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization;
using LCL.Domain.Entities;
using LCL.Infrastructure;

namespace LCL.Domain.Events
{
    /// <summary>
    /// 领域事件
    /// </summary>
    /// <remarks>领域事件是领域模型所引发的事件。</remarks>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// 获取或设置生成域事件的源实体。
        /// </summary>
        IEntity Source { get;  }
    }
}
