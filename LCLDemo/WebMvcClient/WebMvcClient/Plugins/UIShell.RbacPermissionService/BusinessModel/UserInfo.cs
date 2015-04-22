using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public partial class UserInfo : User
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TName { get; set; }
        /// <summary>
        /// 曾用名
        /// </summary>
        public string CengYongMing { get; set; }
        /// <summary>
        /// 姓名全拼
        /// </summary>
        public string NameQuanPin { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public int? NationalID { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public int? PoliticalID { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativeID { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string UserPhoto { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string UserQQ { get; set; }
    }
}
