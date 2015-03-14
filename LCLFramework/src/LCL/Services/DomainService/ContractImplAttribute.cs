using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.DomainServices
{
    /// <summary>
    /// 标记在服务类型上的契约实现标记。
    /// 用于描述一个服务实现了何种契约，以及相应的具体元数据。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ContractImplAttribute : Attribute
    {
        public static readonly string DefaultVersion = "1.0.0.0";

        /// <summary>
        /// 构造函数。用于构造一个使用当前服务为契约的契约实现标记。
        /// </summary>
        public ContractImplAttribute()
        {
            this.Version = DefaultVersion;
        }

        /// <summary>
        /// 构造函数。用于构造一个使用指定契约的契约实现标记。
        /// </summary>
        /// <param name="contractType"></param>
        public ContractImplAttribute(Type contractType)
        {
            if (contractType == null) throw new ArgumentNullException("contractType");

            this.ContractType = contractType;
            this.Version = DefaultVersion;
        }

        /// <summary>
        /// 实现的契约的类型
        /// </summary>
        public Type ContractType { get; private set; }

        /// <summary>
        /// 服务实现的版本号
        /// 默认版本号是 1.0.0.0。
        /// </summary>
        public string Version { get; set; }
    }
}