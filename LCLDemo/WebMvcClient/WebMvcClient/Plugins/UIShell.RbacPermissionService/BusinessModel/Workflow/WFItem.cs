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
        /// 单据编号
        /// </summary>
        public Guid Pbo_ID { get; set; }
        /// <summary>
        /// 单据名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单据地址
        /// </summary>
        public string Pbo_Adderss { get; set; }
        /// <summary>
        /// 流程ID 
        /// </summary>
        public Guid Rout_ID { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public Guid ApplyUserID { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public int State { get; set; }
    }

    public enum WFRoutType
    {
        维修审批流程
    }
}
