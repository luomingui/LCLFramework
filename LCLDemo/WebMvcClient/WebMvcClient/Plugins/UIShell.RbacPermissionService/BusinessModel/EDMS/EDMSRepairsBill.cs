using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 报修单
    /// </summary>
    public class EDMSRepairsBill : BaseModel
    {
        /// <summary>
        /// 报修人
        /// </summary>
        public string RepairsPerson { get; set; }
        /// <summary>
        /// 报修人电话
        /// </summary>
        public string RepairsPersonPhone { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        public Guid DeviceSite_ID { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public string DeviceSite_Name { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string DeviceBrand { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string DeviceModel { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string DeviceDescribe { get; set; }
        /// <summary>
        /// 是否提交报修
        /// </summary>
        public bool IsRepairsSubmit { get; set; }
        /// <summary>
        /// 流程ID
        /// </summary>
        public WFItem WFItem { get; set; }
    }
}
