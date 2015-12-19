using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户组
    /// </summary>
    public partial class Group : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  组类型
        /// </summary>
        public int GroupType { get; set; }
    }
}
