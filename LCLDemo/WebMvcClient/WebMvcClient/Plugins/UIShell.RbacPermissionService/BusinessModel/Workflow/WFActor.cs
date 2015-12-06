using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 步骤
    /// </summary>
    public partial class WFActor : BaseModel
    {
        
        /// <summary>
        /// 流程ID 
        /// </summary>
        public WFRout Rout { get; set; }
        /// <summary>
        /// 步骤序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Name { get; set; }
 
    }
}
