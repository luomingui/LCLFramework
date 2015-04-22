using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 教师信息审核
    /// </summary>
    public partial class User_TeacherCheck : BaseModel
    {
        /// <summary>
        /// 隶属教师
        /// </summary>
        public User_Teacher User_Teacher { get; set; }
        /// <summary>
        /// 学校审核通过
        /// </summary>
        public bool IsPassSchool { get; set; }
        /// <summary>
        /// 学校审核者
        /// </summary>
        public Guid SchoolPassBy { get; set; }
        /// <summary>
        /// 学校审核时间
        /// </summary>
        public DateTime? SchoolPassTime { get; set; }
        /// <summary>
        /// 学校审核意见
        /// </summary>
        public string SchoolPassComments { get; set; }
        #region 行政审核
        /// <summary>
        /// 县局审核通过
        /// </summary>
        public bool IsPassCounty { get; set; }
        /// <summary>
        /// 县局审核者
        /// </summary>
        public Guid CountyPassBy { get; set; }
        /// <summary>
        /// 县局审核时间
        /// </summary>
        public DateTime? CountyPassTime { get; set; }
        /// <summary>
        /// 县局审核意见
        /// </summary>
        public string CountyPassComments { get; set; }
        /// <summary>
        /// 市局审核通过
        /// </summary>
        public bool IsPassCity { get; set; }
        /// <summary>
        /// 市局审核时间
        /// </summary>
        public DateTime? CitylPassTime { get; set; }
        /// <summary>
        /// 市局审核者
        /// </summary>
        public Guid CityPassBy { get; set; }
        /// <summary>
        /// 市局审核意见
        /// </summary>
        public string CityPassComments { get; set; }
        /// <summary>
        /// 省局审核通过
        /// </summary>
        public bool IsPassProvince { get; set; }
        /// <summary>
        /// 省局审核者
        /// </summary>
        public Guid ProvincePassBy { get; set; }
        /// <summary>
        /// 省局审核时间
        /// </summary>
        public DateTime? ProvincePassTime { get; set; }
        /// <summary>
        /// 省局审核意见
        /// </summary>
        public string ProvincePassComments { get; set; }
        #endregion
    }
}
