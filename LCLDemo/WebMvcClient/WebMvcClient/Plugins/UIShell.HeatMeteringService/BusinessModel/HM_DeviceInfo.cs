using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    ///  客户设备管理
    /// </summary>
    [Serializable]
    public partial class HM_DeviceInfo : BaseModel
    {
        /// <summary>
        /// 客户设备
        /// </summary>
        public HM_ClientInfo ClientInfo { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { get; set; }
        /// <summary>
        /// 表开启状态
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 计量单位 
        /// </summary>
        public HeatUnitType HeatUnitType { get; set; }
        /// <summary>
        /// 厂商编码
        /// </summary>
        public string DeviceMac { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
