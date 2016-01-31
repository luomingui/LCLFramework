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
        /// 助记码
        /// </summary>
        public string HelpeCode { get; set; }    
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNumber { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string Cardno { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 收费面积
        /// </summary>
        public double HeatArea { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }
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
