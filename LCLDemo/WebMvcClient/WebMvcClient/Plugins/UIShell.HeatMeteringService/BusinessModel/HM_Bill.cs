using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 票据入库
    /// </summary>
    [Serializable]
    public partial class HM_Bill : BaseModel
    {
        /// <summary>
        /// 票据作用域
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }
        /// <summary>
        /// 起始编号
        /// </summary>
        public int StartNumber { get; set; }
        /// <summary>
        /// 结束编号
        /// </summary>
        public int EndNumber { get; set; }
        /// <summary>
        /// 版本编号
        /// </summary>
        public int VersionNumber { get; set; }
        /// <summary>
        /// 票据规格 数目【本】
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 票据种类
        /// </summary>
        public HM_BillType BillType { get; set; }
    }
}
