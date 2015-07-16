using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.DomainServices
{
    /// <summary>
    /// 表明某个类型是一个服务的契约。
    /// </summary>
    /// 暂时没有用到，未来可能需要对所有的契约遍历。
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ContractAttribute : Attribute { }
}
