using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 流程
    /// </summary>
    public partial class WFRout : BaseModel
    {
        /// <summary>
        /// 流程描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID 
        /// </summary>
        public Guid DeptId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 状态(发布，草稿，删除)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
