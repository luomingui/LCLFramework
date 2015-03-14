using LCL.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LCL.Reflection;

namespace LCL.MetaModel
{
    [DebuggerDisplay("Label:{Label}")]
    public abstract class ModuleMeta : MetaBase
    {
        public ModuleMeta()
        {
            this._Children = new ModuleChildren(this);
            this._orderById = 1;
        }
        private int _orderById;
        public int OrderById
        {
            get { return this._orderById; }
            set { this.SetValue(ref this._orderById, value); }
        }
        private IPlugin _Bundle;
        public IPlugin Bundle
        {
            get { return this._Bundle; }
            set { this.SetValue(ref this._Bundle, value); }
        }
        private string _KeyLabel;
        /// <summary>
        /// 这个属性表示这个模块的名称。
        /// 
        /// 注意，这个名称在整个应用程序中所有模块中应该是唯一的，这样，就可以用它来实现权限控制。
        /// </summary>
        public string KeyLabel
        {
            get
            {
                var value = this._KeyLabel;

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = this.Label;
                }

                return value;
            }
            set { this.SetValue(ref this._KeyLabel, value); }
        }

        private string _Label;
        /// <summary>
        /// 友好显示标签
        /// </summary>
        public string Label
        {
            get { return this._Label; }
            set { this.SetValue(ref this._Label, value); }
        }
        private string _CustomUI;
        /// <summary>
        /// 如果该模块不是由某个类型自动生成的，则这个属性将不为空，并表示某个自定义的 UI 界面。
        /// 
        /// 如果是 WPF 程序，那么这个属性表示目标用户控件的全名称，
        /// 如果是 Web 程序，则这个属性表示目标页面的地址。
        /// </summary>
        public string CustomUI
        {
            get { return this._CustomUI; }
            set { this.SetValue(ref this._CustomUI, value); }
        }
        public string Image { get; set; }
        private Type _EntityType;
        /// <summary>
        /// 这个模块使用 AutoUI 功能的话，这个属性表示其显示的实体类型，否则返回 null。
        /// </summary>
        public Type EntityType
        {
            get { return this._EntityType; }
            set
            {
                this.SetValue(ref this._EntityType, value);
                CustomControllerActionList();
            }
        }

        private ModuleMeta _Parent;
        /// <summary>
        /// 对应的父模块
        /// </summary>
        public ModuleMeta Parent
        {
            get { return this._Parent; }
        }
        private ModuleChildren _Children;
        /// <summary>
        /// 模块中的子模块
        /// </summary>
        public IList<ModuleMeta> Children
        {
            get { return this._Children; }
        }
        /// <summary>
        /// 返回所有可显示的模块。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleMeta> GetChildrenWithPermission()
        {
            return this.Children.Where(PermissionMgr.CanShowModule);
        }
        private IList<ModuleOperation> _CustomOpertions = new List<ModuleOperation>(10);
        /// <summary>
        /// 开发人员可以在这个模块中添加许多自定义功能。
        /// 权限系统为读取这个属性用于用户配置。
        /// </summary>
        public IList<ModuleOperation> CustomOpertions
        {
            get { return this._CustomOpertions; }
        }
        protected abstract void CustomControllerActionList();

        protected override void OnFrozen()
        {
            base.OnFrozen();
            this._Children.IsFrozen = true;
            this._CustomOpertions = new ReadOnlyCollection<ModuleOperation>(this._CustomOpertions);
        }
        private class ModuleChildren : Collection<ModuleMeta>
        {
            internal bool IsFrozen;
            private ModuleMeta _owner;
            public ModuleChildren(ModuleMeta owner)
            {
                this._owner = owner;
            }
            protected override void InsertItem(int index, ModuleMeta item)
            {
                if (this.IsFrozen) throw new InvalidOperationException();
                base.InsertItem(index, item);
                if (item != null)
                {
                    item._Parent = this._owner;
                    if (item.Bundle == null)
                    {
                        item.Bundle = this._owner.Bundle;
                    }
                    item.OrderById++;
                }
            }

            protected override void ClearItems()
            {
                if (this.IsFrozen) throw new InvalidOperationException();

                base.ClearItems();
            }

            protected override void RemoveItem(int index)
            {
                if (this.IsFrozen) throw new InvalidOperationException();

                base.RemoveItem(index);
            }

            protected override void SetItem(int index, ModuleMeta item)
            {
                if (this.IsFrozen) throw new InvalidOperationException();

                base.SetItem(index, item);

                if (item != null)
                {
                    item._Parent = this._owner;
                    if (item.Bundle == null)
                    {
                        item.Bundle = this._owner.Bundle;
                    }
                    item.OrderById++;
                }
            }
        }
    }
}