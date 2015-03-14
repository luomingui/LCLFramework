using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RoleAuthority : BaseModel
    {
        public Role Role { set; get; }
        public string BlockKey { set; get; }
        public string ModuleKey { set; get; }
        public string OperationKey { set; get; }

        public AuthorityType AuthorityType { get; set; }
    }
   
}
