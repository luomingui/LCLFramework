using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户组
    /// </summary>
    public partial class Group : BaseModel
    {
        public Group()
        {
            ID = Guid.NewGuid();
            this.Users = new HashSet<User>();
            this.Roles = new HashSet<Role>();
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
        ///  组类型
        /// </summary>
        public int GroupType { get; set; }        
        /// <summary>
        /// 角色列表
        /// </summary>
        public ICollection<Role> Roles { get; set; }
        /// <summary>
        /// 角色用户
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
