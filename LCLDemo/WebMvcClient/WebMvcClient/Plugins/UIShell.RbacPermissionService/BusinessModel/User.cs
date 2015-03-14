using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public partial class User : BaseModel
    {
        public User()
        {
            this.Role =new HashSet<Role>();
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
        /// 简称
        /// </summary>
        public string NameShort { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        public string Password { set; get; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public ICollection<Role> Role { get; set; }
        /// <summary>
        /// Gets or sets the org.
        /// </summary>
        public Org Org { get; set; }
    }

}
