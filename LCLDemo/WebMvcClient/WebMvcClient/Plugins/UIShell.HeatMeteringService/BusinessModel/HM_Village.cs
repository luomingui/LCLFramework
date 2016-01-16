using System;
using System.Collections.Generic;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    ///<summary>
    /// 小区
    ///</summary>
    [Serializable]
    public partial class HM_Village : BaseTreeModel
    {
        /// <summary>
        /// 小区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拼音简称
        /// </summary>
        public string Pinyi { get; set; }
        /// <summary>
        /// 小区类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 外文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 人口
        /// </summary>
        public int Population { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public int TotalArea { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// 概况
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        ///  小区地址
        /// </summary>
        public string Address { get; set; }
    }
}
