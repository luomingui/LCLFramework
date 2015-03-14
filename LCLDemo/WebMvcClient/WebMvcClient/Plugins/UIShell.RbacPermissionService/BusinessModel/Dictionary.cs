using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 字典管理
    /// </summary>
    public class Dictionary:BaseTreeModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
}
