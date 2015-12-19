using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// ×éÓÃ»§
    /// </summary>
    public partial class GroupUser : BaseModel
    {
        public Group Group { get; set; }
        public User User { get; set; }
    }
}
