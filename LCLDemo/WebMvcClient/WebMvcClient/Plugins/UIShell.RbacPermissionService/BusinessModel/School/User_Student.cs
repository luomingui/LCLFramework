using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 学生
    /// </summary>
    public partial class User_Student : BaseModel
    {
        /// <summary>
        /// 所属学校
        /// </summary>
        public UnitInfo UnitInfo { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
    }
}
