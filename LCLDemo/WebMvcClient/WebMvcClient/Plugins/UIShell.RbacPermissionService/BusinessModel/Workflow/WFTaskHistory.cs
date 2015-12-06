using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 任务历史记录
    /// </summary>
    public partial class WFTaskHistory : BaseModel
    {
        /// <summary>
        /// 项目ID 
        /// </summary>
        public WFItem Item { get; set; }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public WFActor Actor { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 操作人ID 
        /// </summary>
        public Guid OperateUserId { get; set; }
    }

}
