using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 步骤处理人
    /// </summary>
    public partial class WFActorUser : BaseModel
    {
        /// <summary>
        /// 步骤ID
        /// </summary>
        public WFActor Actor { get; set; }
        /// <summary>
        /// 处理人ID
        /// </summary>
        public User User { get; set; }
    }
}
