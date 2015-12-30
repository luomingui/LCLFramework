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
        public WFActor()
        {
            Rout_ID = Guid.Empty;
        }
        /// <summary>
        /// 流程ID 
        /// </summary>
        public Guid Rout_ID { get; set; }
        /// <summary>
        /// 步骤序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单据地址
        /// </summary>
        public string BillAddess { get; set; }
        /// <summary>
        /// 操作人处理串行还是并行
        /// </summary>
        public bool IsSerial { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public int IsManyPeople { get; set; }
    }
  
}
