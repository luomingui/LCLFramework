using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class Role : BaseModel
    {
        public Role()
        {
            ID = Guid.NewGuid();
            this.Users = new HashSet<User>();
            this.RoleAuthoritys = new HashSet<RoleAuthority>();
            this.Groups = new HashSet<Group>();
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  角色类型
        /// </summary>
        public int RoleType { get; set; }
        /// <summary>
        /// 角色用户
        /// </summary>
        public ICollection<User> Users { get; set; }
        /// <summary>
        /// 角色组
        /// </summary>
        public ICollection<Group> Groups { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public ICollection<RoleAuthority> RoleAuthoritys { get; set; }
    }
}
