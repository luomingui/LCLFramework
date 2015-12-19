using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// ×é½ÇÉ«
    /// </summary>
    public partial class GroupRole : BaseModel
    {
        public Group Group { get; set; }
        public Role Role { get; set; }
    }
}
