using System;
using System.Linq;
using LCL.Reflection;
using LCL.DomainServices;
using LCL.Repositories;
namespace LCL
{
    /// <summary>
    ///  框架type约束
    /// </summary>
    public sealed class TypeMatrix
    {
        /// <summary>
        /// 判断给定类型是否是MVC Controller
        /// </summary>
        public static bool IsController(Type type)
        {
            return type != null
                && type.Name.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase)
                && !type.IsAbstract
                && !type.IsInterface;
            
        }
        /// <summary>
        /// 判断给定类型是否是RepositoryContext
        /// </summary>
        public static bool IsRepositoryContext(Type type)
        {
            return type.IsBaseType(typeof(RepositoryContext));
        }
        /// <summary>
        /// 判断给定类型是否是DbContext
        /// 名称规范：XXXXXDbContext
        /// </summary>
        public static bool IsDbContext(Type type)
        {
            return type != null
                 && type.Name.EndsWith("DbContext", StringComparison.InvariantCultureIgnoreCase)
                 && !type.IsAbstract
                 && !type.IsInterface;
        }
        /// <summary>
        /// 判断给定类型是否是DomainEntity
        /// </summary>
        public static bool IsDomainEntity(Type type)
        {
            return type.IsBaseType(typeof(AggregateRoot));
        }
        /// <summary>
        /// 判断给定类型是否是服务
        /// 名称规范：XXXXXDomainService
        /// </summary>
        public static bool IsDomainService(Type type)
        {
            return type.IsBaseType(typeof(DomainService));
        }
    }
}