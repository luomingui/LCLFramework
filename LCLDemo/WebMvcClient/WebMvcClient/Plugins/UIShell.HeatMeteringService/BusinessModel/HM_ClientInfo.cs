using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    ///<summary>
    ///客户信息 
    ///</summary>
    [Serializable]
    public partial class HM_ClientInfo : BaseModel
    {
        /// <summary>
        /// 年度供热单价
        /// </summary>
        public HM_ChargeAnnual ChargeAnnual { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public HM_Village Village { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// 取暖类型
        /// </summary>
        public int HeatType { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string HelpeCode { get; set; }
        /// <summary>
        /// 房间卡号
        /// </summary>
        public string Cardno { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 入网用户名
        /// </summary>
        public string NetInName { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNumber { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }
        /// <summary>
        /// 套内建筑面积
        /// </summary>
        public double InsideBuildArea { get; set; }
        /// <summary>
        /// 超高
        /// </summary>
        public double Superelevation { get; set; }
        /// <summary>
        /// 阳台状态
        /// </summary>
        public int BalconyState { get; set; }
        /// <summary>
        /// 阳台面积
        /// </summary>
        public int BalconyArea { get; set; }
        /// <summary>
        /// 阳台采暖状态
        /// </summary>
        public int BalconyHeatState { get; set; }
        /// <summary>
        /// 阳台收费面积
        /// </summary>
        public int BalconyHeatArea { get; set; }
        /// <summary>
        /// 阁楼夹层面积
        /// </summary>
        public double InterlayerArea { get; set; }
        /// <summary>
        /// 阁楼夹层采暖状态
        /// </summary>
        public int InterlayerState { get; set; }
        /// <summary>
        /// 阁楼夹层收费面积
        /// </summary>
        public double InterlayerHeatArea { get; set; }
        /// <summary>
        /// 套内面积
        /// </summary>
        public double InsideArea { get; set; }
        /// <summary>
        /// 收费面积
        /// </summary>
        public double HeatArea { get; set; }
        /// <summary>
        /// 单价类别
        /// </summary>
        public int UnitPriceType { get; set; }
        /// <summary>
        /// 入网日期
        /// </summary>
        public DateTime NetworkDate { get; set; }
        /// <summary>
        /// 拆网/入网
        /// </summary>
        public bool IsNetwork { get; set; }
        /// <summary>
        /// 开始供暖日期
        /// </summary>
        public DateTime BeginHeatDate { get; set; }
        /// <summary>
        /// 总热源厂
        /// </summary>
        public string TotalHeatSourceFactory { get; set; }
        /// <summary>
        /// 热源
        /// </summary>
        public string HeatSource { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        ///  线别
        /// </summary>
        public string LineType { get; set; }
        /// <summary>
        /// 报停/强停/复热
        /// </summary>
        public int HeatState { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工作地址
        /// </summary>
        public string JobAddress { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string HomeAddress { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }

    }
}
