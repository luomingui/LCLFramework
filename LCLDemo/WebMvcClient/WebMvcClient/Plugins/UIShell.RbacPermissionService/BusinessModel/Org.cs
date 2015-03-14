
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public partial class Org : BaseTreeModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string HelperCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string NameShort { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public OrgType OrgType { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
}
