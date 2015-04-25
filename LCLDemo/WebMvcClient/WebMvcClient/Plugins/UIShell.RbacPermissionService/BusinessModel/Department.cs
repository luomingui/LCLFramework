using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 部门
    /// </summary>
    public partial class Department : BaseTreeModel
    {
        /// <summary>
        /// 隶属单位
        /// </summary>
        public UnitInfo UnitInfo { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string HelperCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
