using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 学校信息
    /// </summary>
    public partial class SchoolInfo : BaseModel
    {        
        /// <summary>
        /// 基本信息
        /// </summary>
       [Required]  
        public UnitInfo UnitInfo { get; set; }
        /// <summary>
        /// 校长名称
        /// </summary>
        public string HeadmasterName { get; set; }
        /// <summary>
        /// 校长手机
        /// </summary>
        public string HeadmasterPhone { get; set; }
        /// <summary>
        /// 校庆日
        /// </summary>
        public Nullable<DateTime> SchoolYear { get; set; }
        /// <summary>
        /// 土地证号
        /// </summary>
        public string LandCardNo { get; set; }
        /// <summary>
        /// 是否公办
        /// </summary>
        public bool IsMB { get; set; }
    }
}
