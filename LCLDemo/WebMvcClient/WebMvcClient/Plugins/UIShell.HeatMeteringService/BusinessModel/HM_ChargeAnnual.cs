using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    ///<summary>
    /// 年度供热单价 
    ///</summary>
    [Serializable]
    public partial class HM_ChargeAnnual : BaseModel
    {
        /// <summary>
        /// 标识名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否当前供热年度
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 供热开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 供热结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 供热天数
        /// </summary>
        public int GongreDay { get; set; }
        /// <summary>
        /// 缔纳开始日期
        /// </summary>
        public DateTime DnaBeginDate { get; set; }
        /// <summary>
        /// 违约金比例
        /// </summary>
        public double BreakMoney { get; set; }
        /// <summary>
        /// 停热基础费比例
        /// </summary>
        public double StopHeatRatio { get; set; }
        /// <summary>
        ///固定热费比例
        /// </summary>
        public double Fixedportion { get; set; }
        /// <summary>
        ///公建
        /// </summary>
        public double Gongjian { get; set; }
        /// <summary>
        ///居民
        /// </summary>
        public double Resident { get; set; }
        /// <summary>
        ///底商
        /// </summary>
        public double Dishang { get; set; }
        /// <summary>
        ///公建1
        /// </summary>
        public double Gongjian1 { get; set; }
        /// <summary>
        ///居民1
        /// </summary>
        public double Resident1 { get; set; }
        /// <summary>
        ///底商1
        /// </summary>
        public double Dishang1 { get; set; }
    }
}
