using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    ///<summary>
    /// 热计量采暖费结算单
    ///</summary>
    [Serializable]
    public partial class HM_ClientHeatCharge : BaseModel
    {
        /// <summary>
        /// 客户
        /// </summary>
        public HM_ClientInfo ClientInfo { get; set; }
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public HM_ChargeUser ChargeUser { get; set; }
        /// <summary>
        /// 供热开始表数
        /// </summary>
        public double BeginHeat { get; set; }
        /// <summary>
        /// 供热结束表数
        /// </summary>
        public double EndHeat { get; set; }
        /// <summary>
        /// 用热量（KMH）
        /// </summary>
        public double UseHeat { get; set; }
        /// <summary>
        /// 计量热费
        /// </summary>
        public double MoneyHeat { get; set; }
        /// <summary>
        /// 基础热费
        /// </summary>
        public double MoneyBaseHeat { get; set; }
        /// <summary>
        /// 预收金额
        /// </summary>
        public double MoneyAdvance { get; set; }
        /// <summary>
        /// 退（补）金额
        /// </summary>
        public double MoneyOrRefunded { get; set; }
    }
}
