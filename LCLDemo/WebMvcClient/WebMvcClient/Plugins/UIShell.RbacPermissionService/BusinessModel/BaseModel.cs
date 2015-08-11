using LCL;
using System;
using System.ComponentModel;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 模型基类
    /// </summary>
    [Serializable]
    public partial class BaseModel : Entity
    {

    }
    /// <summary>
    /// 树形模型基类
    /// </summary>
    [Serializable]
    public partial class BaseTreeModel : BaseModel,IEntityTree
    {
        public BaseTreeModel()
        {
            
            IsLast = false;
            Level = 0;
            NodePath = "";
            OrderBy = 0;
            ParentId = Guid.Empty;
        }
        /// <summary>
        /// 是否最后一级
        /// </summary>
        [DefaultValue(false)]
        public bool IsLast { get; set; }
        /// <summary>
        /// 树形深度
        /// </summary>
        [DefaultValue(0)]
        public int Level { get; set; }
        /// <summary>
        /// 树形路径
        /// </summary>
        public string NodePath { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DefaultValue(0)]
        public int OrderBy { get; set; }
        /// <summary>
        /// 上一级
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
