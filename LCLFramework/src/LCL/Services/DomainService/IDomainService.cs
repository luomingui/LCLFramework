using System;
using System.Reflection;
using LCL.Reflection;

namespace LCL
{
    /// <summary>
    /// 跨 C/S，B/S 的服务
    /// </summary>
    public interface IDomainService
    {
        /// <summary>
        /// 调用服务。
        /// </summary>
        void Invoke();
    }

    /// <summary>
    /// 一种过程化服务的基类
    /// <remarks>
    /// 过程化简单地指：进行一系列操作，返回是否成功以及相应的提示消息。
    /// </remarks>
    /// </summary>
    public interface IFlowService : IDomainService
    {
        /// <summary>
        /// 过程返回的结果。
        /// </summary>
        [ServiceOutput]
        Result Result { get; set; }
    }

    /// <summary>
    /// 服务的输入属性标记。
    /// 
    /// <remarks>
    /// 默认情况下，服务的属性都是输入属性。当某个属性即是输入也是输出时，才需要标记这个标记。
    /// </remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ServiceInputAttribute : Attribute { }

    /// <summary>
    /// 服务的输出属性标记。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ServiceOutputAttribute : Attribute { }

    public class ServiceHelper
    {
        public static bool IsInput(PropertyInfo property)
        {
            //没有标记 ServiceOutput 的属性、或者同时标记了两个标记的属性，都是输入属性。
            return !property.HasMarked<ServiceOutputAttribute>() ||
                property.HasMarked<ServiceInputAttribute>();
        }

        public static bool IsOutput(PropertyInfo property)
        {
            return property.HasMarked<ServiceOutputAttribute>();
        }
    }
}