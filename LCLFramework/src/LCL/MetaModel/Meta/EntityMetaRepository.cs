using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LCL.MetaModel
{
    /// <summary>
    /// 实体默认视图及实体信息的仓库
    /// </summary>
    public class EntityMetaRepository : MetaRepositoryBase<EntityMeta>
    {
        #region 外部接口 - 实体覆盖

        private Dictionary<Type, Type> _overriding = new Dictionary<Type, Type>(10);

        /// <summary>
        /// 使用子类完全覆盖父类。
        /// 一般用于客户化的扩展。
        /// </summary>
        /// <typeparam name="TSubclass"></typeparam>
        public void OverrideBase<TSubclass>()
        {
            var type = typeof(TSubclass);
            this._overriding[type.BaseType] = type;
        }

        /// <summary>
        /// 获取所有的客户化信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<Type, Type>> EnumerateAllOverriding()
        {
            return this._overriding;
        }

        /// <summary>
        /// 如果设置了覆盖，则返回覆盖的子类。
        /// </summary>
        /// <param name="entityType"></param>
        public void ReplaceIfOverrided(ref Type entityType)
        {
            Type subType;
            if (this._overriding.TryGetValue(entityType, out subType))
            {
                entityType = subType;
            }
        }

        internal bool IsOverrided(Type entityType)
        {
            return this._overriding.ContainsKey(entityType);
        }

        #endregion


        /// <summary>
        /// 添加一个原始的实体类型
        /// </summary>
        /// <param name="entityType"></param>
        internal void AddRootPrime(Type entityType)
        {
            if (!this.IsOverrided(entityType))
            {
                this.CreateEntityMetaRecur(entityType);
            }
        }
        private EntityMeta CreateEntityMetaRecur(Type entityType, EntityMeta parentMeta = null)
        {
            ReplaceIfOverrided(ref entityType);

            var entityMeta = new EntityMeta
            {
                EntityType = entityType
            };

            this.AddPrime(entityMeta);
            return entityMeta;
        }
    }
}