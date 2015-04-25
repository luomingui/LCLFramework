using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 企业员工
    /// </summary>
    public partial class User_Employee : BaseModel
    {
        /// <summary>
        /// 所属企业
        /// </summary>
        public UnitInfo UnitInfo { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string HelperCode { get; set; }
    }
}
