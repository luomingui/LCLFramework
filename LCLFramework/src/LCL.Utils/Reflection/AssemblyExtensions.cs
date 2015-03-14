using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Diagnostics;

namespace LCL.Reflection
{
    /// <summary>
    /// 扩展<see cref="Assembly"/>。
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 判断当前的程序集是否是在Debug模式下编译
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool IsDebugBuild(this Assembly assembly)
        {
            return assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Any(x => x.IsJITTrackingEnabled);
        }
        /// <summary>
        /// 返回<paramref name="assembly"/>中的所有实现或继承了<typeparamref name="TBaseType"/>的具体类型实例。
        /// </summary>
        public static IEnumerable<TBaseType> CreateConcreteDescendentInstances<TBaseType>(this Assembly assembly)
        {

            return
                    assembly
                    .GetConcreteDescendentTypes<TBaseType>()
                    .Where(type => !type.ContainsGenericParameters)
                    .Select(type => type.CreateInstance<TBaseType>())
                    .ToList();
        }

        /// <summary>
        /// 返回<paramref name="assembly"/>中的所有实现或继承了<typeparamref name="TBaseType"/>的具体类型。
        /// </summary>
        public static IEnumerable<Type> GetConcreteDescendentTypes<TBaseType>(this Assembly assembly)
        {

            return assembly.GetConcreteDescendentTypes(typeof(TBaseType));
        }

        /// <summary>
        /// 返回<paramref name="assembly"/>中的所有实现或继承了<paramref name="baseType"/>的具体类型。
        /// </summary>
        public static IEnumerable<Type> GetConcreteDescendentTypes(this Assembly assembly, Type baseType)
        {

            return
                    assembly
                    .GetTypes()
                    .Where(type =>
                        baseType.IsAssignableFrom(type)
                        &&
                        type.IsConcreteType()
                    );
        }

        public static Version AssemblyVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }
    }
}
