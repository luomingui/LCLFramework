using LCL;
using System;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 模型基类
    /// </summary>
    [Serializable]
    public class BaseModel : AggregateRoot
    {

    }
    /// <summary>
    /// 树形模型基类
    /// </summary>
    [Serializable]
    public class BaseTreeModel : AggregateRoot, ITreeNode
    {
        public BaseTreeModel()
        {
            IsLast = false;
            Level = 0;
            NodePath = "";
            OrderBy = 0;
            ParentId = Guid.NewGuid();
        }
        /// <summary>
        /// 是否最后一级
        /// </summary>
        public bool IsLast { get; set; }
        /// <summary>
        /// 树形深度
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 树形路径
        /// </summary>
        public string NodePath { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 上一级
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
