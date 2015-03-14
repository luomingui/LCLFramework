
using System;

namespace LCL.Serialization.Mobile
{
    /// <summary>
    /// 类型标记此特性，表示该类型及其父类中的字段都不需要 MobileSerialization 引擎自动序列化。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
    public class MobileNonSerializedAttribute : Attribute { }
}