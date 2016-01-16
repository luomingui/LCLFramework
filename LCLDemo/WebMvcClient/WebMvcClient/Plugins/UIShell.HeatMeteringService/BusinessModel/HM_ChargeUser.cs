using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 收费员区域配置
    /// </summary>
    [Serializable]
    public partial class HM_ChargeUser : BaseModel
    {
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 小区
        /// </summary>
        public HM_Village Village { set; get; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid User_ID { set; get; }
    }
}
