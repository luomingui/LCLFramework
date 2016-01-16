using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 客户票据
    /// </summary>
    [Serializable]
    public partial class HM_ClientBill : BaseModel
    {
        /// <summary>
        /// 客户热计量采暖费结算单
        /// </summary>
        public HM_ClientHeatCharge ClientHeatCharge { get; set; }
        /// <summary>
        /// 票据单号
        /// </summary>
        public string BillNumber{ get; set; }
    }
}
