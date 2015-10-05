using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    /// <summary>
    /// 一个拥有名称的对象。
    /// </summary>
    public interface IHasName
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
