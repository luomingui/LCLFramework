using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 票据分类
    /// </summary>
    [Serializable]
    public partial class HM_BillType : BaseModel
    {
        /// <summary>
        /// 票据种类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 每本页数
        /// </summary>
        public int PageSum { get; set; }
        /// <summary>
        /// 票据编号长度
        /// </summary>
        public int BillLength { get; set; }
    }
}
