
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 单位信息
    /// </summary>
    public partial class UnitInfo : BaseTreeModel
    {
        /// <summary>
        /// 行政区域
        /// </summary>
        public Xzqy Xzqy { get; set; }
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
        public OrgType OrgType { get; set; }
    }
}
