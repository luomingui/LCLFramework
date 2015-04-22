using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 企业信息
    /// </summary>
    public partial class CompanyInfo : BaseModel
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        [Required]  
        public UnitInfo UnitInfo { get; set; }
        /// <summary>
        /// 企业种类
        /// </summary>
        [DefaultValue(0)]
        public CompanyType CompanyType { get; set; }
        /// <summary>
        /// 经济类型
        /// </summary>
         [DefaultValue(0)]
        public EconomicType EconomicType { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public Nullable<DateTime> RegisterDate { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        public double RegisterMoney { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterAddress { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string EgalPerson { get; set; }

    }
}
