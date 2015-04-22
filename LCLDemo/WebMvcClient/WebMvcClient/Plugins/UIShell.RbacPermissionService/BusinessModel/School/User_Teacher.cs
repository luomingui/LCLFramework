using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 教师
    /// </summary>
    public partial class User_Teacher : BaseModel
    {
        /// <summary>
        /// 所属学校
        /// </summary>
        public SchoolInfo SchoolInfo { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 是否取得教师资格证书
        /// </summary>
        public bool IsGetZGZ { get; set; }
        /// <summary>
        /// 取得教师资格证书书时间
        /// </summary>
        public DateTime? GetZGZTime { get; set; }
        /// <summary>
        /// 教师资格证书号
        /// </summary>
        public string ZGZNo { get; set; }
        /// <summary>
        /// 计算机等级
        /// </summary>
        public string ComputerRank { get; set; }
        /// <summary>
        /// 最高学位
        /// </summary>
        public int? DegreeID { get; set; }
    }
}
