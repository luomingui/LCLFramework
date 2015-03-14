using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace LCL.MetaModel
{
    /// <summary>
    /// 实体元数据
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class EntityMeta : Meta
    {
        /// <summary>
        /// 类型名
        /// </summary>
        public override string Name
        {
            get { return this.EntityType.Name; }
            set { throw new NotSupportedException(); }
        }

        private Type _entityType;
        /// <summary>
        /// 当前模型是对应这个类型的。
        /// </summary>
        public Type EntityType
        {
            get { return this._entityType; }
            set { this.SetValue(ref this._entityType, value); }
        }
    }
}