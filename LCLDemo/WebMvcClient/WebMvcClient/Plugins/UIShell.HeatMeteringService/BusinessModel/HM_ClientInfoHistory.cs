using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 采暖变更历史
    /// </summary>
    [Serializable]
    public partial class HM_ClientInfoHistory : BaseModel
    {
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid User_ID { get; set; }
        /// <summary>
        /// 客户信息
        /// </summary>
        public HM_ClientInfo ClientInfo { get; set; }
        /// <summary>
        /// 变更类别
        /// </summary>
        public int RecordType { get; set; }
        /// <summary>
        /// 变更信息
        /// </summary>
        public string Record { get; set; }
    }
}
