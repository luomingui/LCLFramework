using System;

namespace LCL
{
    /// <summary>
    /// 树的节点。
    /// </summary>
    public interface ITreeNode
    {
        /// <summary>
        /// 父节点ID。
        /// </summary>
        Guid ParentId { get; }
        /// <summary>
        /// 深度
        /// </summary>
        int Level { get; }
        /// <summary>
        /// 排序
        /// </summary>
        int OrderBy { get; }
        /// <summary>
        /// 是否最后一级
        /// </summary>
        bool IsLast { get; }
        /// <summary>
        /// 节点在树中的路径，如：/A/B/C/D，包含自己。
        /// </summary>
        string NodePath { get; }
    }
}
