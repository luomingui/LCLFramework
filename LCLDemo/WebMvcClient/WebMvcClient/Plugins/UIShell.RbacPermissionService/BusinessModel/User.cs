using System.Collections.Generic;
using System.ComponentModel;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : BaseModel
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.Groups = new HashSet<Group>();
        }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { set; get; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DefaultValue(false)]
        public bool IsLockedOut { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public ICollection<Role> Roles { get; set; }        
        /// <summary>
        /// 用户组
        /// </summary>
        public ICollection<Group> Groups { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
        #region UserInfo
        /// <summary>
        /// 照片
        /// </summary>
        public string UserPhoto{ get; set; }
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
        public string NationalID { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalID { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string UserQQ { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        #endregion
    }
}
