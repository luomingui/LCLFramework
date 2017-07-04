using Autofac;

namespace LCL.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 容器注册类
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册ICO
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="typeFinder"></param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);
        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
    }
}
