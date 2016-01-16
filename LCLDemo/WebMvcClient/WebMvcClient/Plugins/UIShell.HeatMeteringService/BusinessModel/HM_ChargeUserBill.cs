
using UIShell.RbacPermissionService;
namespace UIShell.HeatMeteringService
{
    /// <summary>
    ///  收费员领用票据
    /// </summary>
    public partial class HM_ChargeUserBill : BaseModel
    {
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public HM_ChargeUser ChargeUser { get; set; }
        /// <summary>
        /// 领用票据数量
        /// </summary>
        public int BillNumber { get; set; }
    }
}
