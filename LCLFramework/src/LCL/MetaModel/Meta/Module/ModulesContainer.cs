using LCL.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LCL.MetaModel
{
    /// <summary>
    /// 模块容器
    /// </summary>
    public class ModulesContainer
    {
        private IList<ModuleMeta> _roots = new List<ModuleMeta>();

        internal ModulesContainer() { }

        /// <summary>
        /// 冻结所有的元数据
        /// </summary>
        internal void Freeze()
        {
            foreach (var v in this._roots) { v.Freeze(); }
            _roots = new ReadOnlyCollection<ModuleMeta>(_roots);
        }

        /// <summary>
        /// 获取所有根模块。
        /// 可通过此属性来变更根模块列表。
        /// </summary>
        /// <returns></returns>
        public IList<ModuleMeta> Roots
        {
            get { return this._roots.OrderBy(e => e.OrderById).ToList(); }
        }
        public int Count
        {
            get { return this._roots.Count; }
        }


        /// <summary>
        /// 获取所有满足当前权限要求的根模块
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleMeta> GetModuleWithPermission()
        {
            return this._roots.Where(PermissionMgr.CanShowModule).OrderBy(e => e.OrderById);
        }
        /// <summary>
        /// 获取所有满足当前权限要求的根模块
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleMeta> GetBlockWithPermission()
        {
            return this._roots.Where(PermissionMgr.CanShowBlock).OrderBy(e => e.OrderById);
        }
        /// <summary>
        /// 添加一个根模块
        /// </summary>
        /// <param name="module"></param>
        public ModuleMeta AddRoot(ModuleMeta module)
        {
            this._roots.Add(module);

            return module;
        }

        /// <summary>
        /// 根据唯一的名称来查找某个模块
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public ModuleMeta FindModule(string keyName)
        {
            foreach (var root in this._roots)
            {
                var m = this.FindModule(root, keyName);
                if (m != null) return m;
            }

            return null;
        }

        /// <summary>
        /// 找到第一个实体类型为指定类型的模块
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public ModuleMeta FindModule(Type entityType)
        {
            foreach (var root in this._roots)
            {
                var m = this.FindModule(root, entityType);
                if (m != null) return m;
            }

            return null;
        }

        /// <summary>
        /// 直接获得某一个模块。
        /// 如果没有找到，则会抛出异常。
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public ModuleMeta this[string keyName]
        {
            get
            {
                var m = this.FindModule(keyName);
                return m;
            }
        }
        public ModuleMeta this[Type entityType]
        {
            get
            {
                var m = this.FindModule(entityType);
                return m;
            }
        }
        private ModuleMeta FindModule(ModuleMeta module, string keyName)
        {
            if (module.KeyLabel == keyName) { return module; }

            foreach (var child in module.Children)
            {
                var m = this.FindModule(child, keyName);
                if (m != null) return m;
            }
            return null;
        }

        private ModuleMeta FindModule(ModuleMeta module, Type entityType)
        {
            if (module.EntityType == entityType) { return module; }

            foreach (var child in module.Children)
            {
                var m = this.FindModule(child, entityType);
                if (m != null) return m;
            }

            return null;
        }
    }

    public class ModuleOperationContainer
    {
        private List<ModuleOperation> _roots = new List<ModuleOperation>();

        internal ModuleOperationContainer()
        {
            _roots.Add(new ModuleOperation
            {
                Label = "显示",
                Name = "view"
            });
            _roots.Add(new ModuleOperation
            {
                Label = "添加",
                Name = "add"
            });
            _roots.Add(new ModuleOperation
            {
                Label = "修改",
                Name = "edit"
            });
            _roots.Add(new ModuleOperation
            {
                Label = "删除",
                Name = "delete"
            });
        }
    }
}