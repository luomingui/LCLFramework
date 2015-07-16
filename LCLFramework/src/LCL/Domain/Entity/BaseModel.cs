using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 模型基类
    /// </summary>
    [Serializable]
    public partial class BaseModel : Entity
    {
        public BaseModel()
        {
            ID = Guid.NewGuid();
            AddDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        public bool IsDelete { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public Nullable<DateTime> AddDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
    }
    /// <summary>
    /// 树形模型基类
    /// </summary>
    [Serializable]
    public partial class BaseTreeModel : BaseModel, ITreeNode
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
        public int ChildNum { get; set; }
    }
}
