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
        /// 流程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID 
        /// </summary>
        public Guid DepID { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 设定审批人分为
        /// </summary>
        public int  SetApprovalPerson { get; set; }
    }


}
