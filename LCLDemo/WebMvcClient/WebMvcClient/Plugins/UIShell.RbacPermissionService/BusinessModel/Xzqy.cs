using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 行政区域
    /// </summary>
    public partial class Xzqy : BaseTreeModel
    {
        /// <summary>
        /// 区划代码
        /// </summary>
        public string HelperCode { get; set; }
        /// <summary>
        /// 区划名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区划介绍
        /// </summary>
        public string Intro { get; set; }
    }
}
