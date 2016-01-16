using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 预交优惠率
    /// </summary>
    [Serializable]
    public partial class HM_Favorable : BaseModel
    {
        /// <summary>
        /// 供热年度
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 优惠开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 优惠结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 优惠率 5%
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 优惠范围
        /// </summary>
        public string ClientTypeIdList { get; set; }
    }
}
