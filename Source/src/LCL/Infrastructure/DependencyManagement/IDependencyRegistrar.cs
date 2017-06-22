using Autofac;

namespace LCL.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 依赖注册
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
