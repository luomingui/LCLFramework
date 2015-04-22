using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    public partial class TLog : BaseModel
    {
        /// <summary>
        /// 单位
        /// </summary>
        public Guid Org_Id { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
