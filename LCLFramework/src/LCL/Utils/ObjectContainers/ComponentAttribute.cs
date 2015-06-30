
using System;

namespace LCL
{
    /// <summary>
    /// 标记某个类是一个组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public LifeStyle LifeStyle { get; set; }
        public ComponentAttribute() : this(LifeStyle.Transient) { }
        public ComponentAttribute(LifeStyle lifeStyle)
        {
            this.LifeStyle = lifeStyle;
        }
    }
    /// <summary>
    /// 服务生命周期。
    /// </summary>
    public enum LifeStyle
    {
        /// <summary>
        /// 每次都创建不同的实例。
        /// </summary>
        Transient,

        /// <summary>
        /// 请求级别的单例。
        /// </summary>
        PerRequest,

        /// <summary>
        /// 线程级别的单例。
        /// </summary>
        PerThread,

        /// <summary>
        /// 单例。
        /// </summary>
        Singleton
    }
}
