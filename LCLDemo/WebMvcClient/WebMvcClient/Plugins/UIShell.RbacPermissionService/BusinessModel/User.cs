using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : BaseModel
    {
        public User()
        {
            this.Role =new HashSet<Role>();
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
        public ICollection<Role> Role { get; set; }
        /// <summary>
        /// 隶属单位
        /// </summary>
        public UnitInfo UnitInfo { get; set; }

        
    }

}
