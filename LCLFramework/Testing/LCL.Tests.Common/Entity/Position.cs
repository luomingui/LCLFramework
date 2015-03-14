
using System.Collections.Generic;
namespace LCL.Tests.Common
{
    /// <summary>
    /// 岗位管理
    /// </summary>
    public class Position : MyEntity
    {
        public Position()
        {
            this.User = new HashSet<User>();
            this.OrgPositionOperationDeny = new HashSet<OrgPositionOperationDeny>();
        }

        public string Code { set; get; }
        public string Name { set; get; }
        public Org Org { get; set; }
        /// <summary>
        /// 部门岗位人员
        /// </summary>
        public virtual ICollection<User> User { get; set; }
        /// <summary>
        /// 部门岗位功能权限
        /// </summary>
        public virtual ICollection<OrgPositionOperationDeny> OrgPositionOperationDeny { get; set; }
    }
}
