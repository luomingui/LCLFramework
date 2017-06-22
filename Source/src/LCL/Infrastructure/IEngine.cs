using System;
using LCL.Infrastructure.DependencyManagement;
using LCL.Config;

namespace LCL.Infrastructure
{
    /// <summary>
    ///  IOC（引擎接口）
    /// 实现了这个接口的类可以作为门户的各种服务组合LCL引擎。
    /// 编辑功能,模块和实现访问通过LCL大部分功能接口。
    /// </summary>
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }
        void Initialize(LConfig config);
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        T[] ResolveAll<T>();
    }
}
