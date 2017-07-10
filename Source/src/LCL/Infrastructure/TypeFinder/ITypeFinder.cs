using System;
using System.Collections.Generic;
using System.Reflection;

namespace LCL.Infrastructure
{
    /// <summary>
    /// 类型查找抽象接口
    /// </summary>
    public interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();

        Type[] Find(Func<Type, bool> predicate);
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
