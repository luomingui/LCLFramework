using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : BaseModel
    {
        public Role()
        {
            this.RoleUsers = new HashSet<User>();
            this.RoleAuthoritys = new HashSet<RoleAuthority>();
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string HelperCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 角色用户
        /// </summary>
        public ICollection<User> RoleUsers { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public ICollection<RoleAuthority> RoleAuthoritys { get; set; }
    }
}
