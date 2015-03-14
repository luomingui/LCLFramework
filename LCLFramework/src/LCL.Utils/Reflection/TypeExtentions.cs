using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Reflection
{
    /// <summary>
    /// 扩展<see cref="Type"/>。
    /// </summary>
    public static class TypeExtentions
    {
        /// <summary>
        /// 创建<typeparamref name="T"/>类型的实例。
        /// </summary>
        public static T CreateInstance<T>(this Type type, params object[] args)
        {

            return (T)type.CreateInstance(args);
        }

        /// <summary>
        /// 创建<paramref name="type"/>类型的实例。
        /// </summary>
        public static object CreateInstance(this Type type, params object[] args)
        {

            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 是否是具体类型，凡是能直接实例化的类型都是具体类型。
        /// </summary>
        public static bool IsConcreteType(this Type type)
        {

            return
                    !type.IsGenericTypeDefinition
                    &&
                    !type.IsAbstract
                    &&
                    !type.IsInterface;
        }
        /// <summary>
        /// 在<paramref name="type"/>的接口定义中，是否拥有<paramref name="genericInterfaceTypeDefinition"/>指定的泛型接口类型定义 。
        /// </summary>
        public static bool HasGenericInterfaceTypeDefinition(this Type type, Type genericInterfaceTypeDefinition)
        {

            foreach (var item in type.GetInterfaces())
            {
                if (item.IsGenericType
                    &&
                    item.GetGenericTypeDefinition() == genericInterfaceTypeDefinition)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Determines whether [is base type] [the specified type].
        /// 判断type是否继承baseType
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns></returns>
        public static bool IsBaseType(this Type type, Type baseType)
        {
            if (type.BaseType == null || type.BaseType == typeof(System.Object))
                return false;
            if (type.BaseType == baseType)
            {
                if (type.IsAbstract || type.IsInterface)
                    return false;
                return true;
            }
            else
            {
                if (type.BaseType.BaseType != null)
                    return IsBaseType(type.BaseType.BaseType, baseType);
                else
                    return false;
            }
        }
        /// <summary>
        /// 判断某个类型是否继承自某个父类
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <param name="requiredBaseType">基类类型</param>
        public static void AssertTypeInheritance(this Type type, Type requiredBaseType)
        {
            if (!requiredBaseType.IsAssignableFrom(type))
            {
                throw new Exception(string.Format("类型{0}不是一个有效的类型，因为没有继承类型{1}", type.FullName, requiredBaseType.FullName));
            }
        }
    }
}
