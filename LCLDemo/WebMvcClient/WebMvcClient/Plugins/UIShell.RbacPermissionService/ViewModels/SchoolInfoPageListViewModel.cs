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
        OrgType ,
        HeadmasterName ,
        HeadmasterPhone ,
        SchoolYear ,
        LandCardNo,u.UpdateDate
FROM    dbo.UnitInfo u
        INNER JOIN dbo.SchoolInfo s ON u.ID = s.UnitInfo_ID
     
     */
    public class SchoolInfoPageListViewModel
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
        public UnitType UnitType { get; set; }

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
        public DateTime UpdateDate{ get; set; }
    }
}
