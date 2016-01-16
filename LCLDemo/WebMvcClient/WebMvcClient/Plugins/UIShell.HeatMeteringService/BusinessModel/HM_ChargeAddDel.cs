using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 费用增减类别
    /// </summary>
    [Serializable]
    public partial class HM_ChargeAddDel : BaseModel
    {
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 父类别
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
    }
}
