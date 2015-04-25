using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.MvcExtensions;

namespace UIShell.RbacPermissionService
{
    /*
SELECT  u.ID ,
        HelperCode ,
        u.Name ,
        u.NameFull ,
        u.NameEN ,
        Email ,
        OfficePhone ,
        FaxPhone ,
        Address ,
        ZipCode ,
        HomePage ,
        Remark ,
     
        EconomicType ,
        RegisterDate ,
        RegisterMoney ,
        RegisterAddress ,
        EgalPerson ,
        u.UpdateDate
FROM    dbo.UnitInfo u
        INNER JOIN dbo.CompanyInfo s ON u.ID = s.UnitInfo_ID
     
     */
    public class CompanyInfoPageListViewModel
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string HelperCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 全称
        /// </summary>
        public string NameFull { get; set; }
        /// <summary>
        /// 外文名称
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 传真电话
        /// </summary>
        public string FaxPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 单位网址
        /// </summary>
        public string HomePage { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 单位类型
        /// </summary>
        public CompanyType CompanyType { get; set; }
        /// <summary>
        /// 经济类型
        /// </summary>
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

        public DateTime UpdateDate{ get; set; }
    }
}
