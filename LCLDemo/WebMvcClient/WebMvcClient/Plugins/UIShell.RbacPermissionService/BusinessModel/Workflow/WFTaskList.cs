using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 任务列表
    /// </summary>
    public partial class WFTaskList : BaseModel
    {
        /// <summary>
        /// 项目ID 
        /// </summary>
        public Guid Item_ID { get; set; }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public Guid Actor_ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }
    }

}
