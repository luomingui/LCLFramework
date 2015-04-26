using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public partial class RoleAuthority : BaseModel
    {
        /// <summary>
        /// 所属角色
        /// </summary>
        public Role Role { set; get; }
        /// <summary>
        /// 块
        /// </summary>
        public string BlockKey { set; get; }
        /// <summary>
        /// 模块
        /// </summary>
        public string ModuleKey { set; get; }
        /// <summary>
        /// 功能
        /// </summary>
        public string OperationKey { set; get; }
        public string Url { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public AuthorityType AuthorityType { get; set; }
    }
   
}
