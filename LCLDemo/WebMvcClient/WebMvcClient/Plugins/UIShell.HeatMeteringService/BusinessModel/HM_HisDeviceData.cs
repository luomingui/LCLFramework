using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 抄表数据
    /// </summary>
    [Serializable]
    public partial class HM_HisDeviceData : BaseModel
    {
        /// <summary>
        /// 客户
        /// </summary>
        public HM_DeviceInfo DeviceInfo { get; set; }
        /// <summary>
        /// 累计用热量
        /// </summary>
        public double TotalHeat { get; set; }
        /// <summary>
        /// 入口温度
        /// </summary>
        public double SupplyWaterT { get; set; }
        /// <summary>
        /// 出口温度
        /// </summary>
        public double BackWaterT { get; set; }
        /// <summary>
        /// 温差
        /// </summary>
        public double DifferenceT { get; set; }
        /// <summary>
        /// 抄表人
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 抄表时间
        /// </summary>
        public DateTime RealTime { get; set; }
    }
}
