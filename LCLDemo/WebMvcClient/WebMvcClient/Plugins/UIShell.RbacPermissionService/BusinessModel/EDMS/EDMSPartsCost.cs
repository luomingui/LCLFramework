using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 配件/服务费
    /// </summary>
    public partial class EDMSPartsCost : BaseModel
    {
        /// <summary>
        /// 维修单
        /// </summary>
        public EDMSMaintenanceBill MaintenanceBill { get; set; }
        /// <summary>
        /// 费用类型
        /// </summary>
        public string CostType { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string DeviceBrand { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string DeviceModel { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string UnitCost { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 质保期
        /// </summary>
        public string Warranty { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
