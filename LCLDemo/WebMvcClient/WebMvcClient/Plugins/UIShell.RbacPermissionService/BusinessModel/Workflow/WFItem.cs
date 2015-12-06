using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 项目
    /// </summary>
    public partial class WFItem : BaseModel
    {
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 流程ID 
        /// </summary>
        public WFRout Rout { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public Guid ApplyUserID { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public int State { get; set; }
    }
}
