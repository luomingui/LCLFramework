using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 供热合同
    /// </summary>
    [Serializable]
    public partial class HM_ClientCompact : BaseModel
    {
        /// <summary>
        /// 客户
        /// </summary>
        public Guid ClientInfo_ID { set; get; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string ClientInfo_Name { set; get; }
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public Guid ChargeAnnual_ID { set; get; }
        /// <summary>
        /// 年度供热标识
        /// </summary>
        public string ChargeAnnual_Name { set; get; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid User_ID { set; get; }
        /// <summary>
        /// 合同文件
        /// </summary>
        public string FilePath { set; get; }

    }
}
