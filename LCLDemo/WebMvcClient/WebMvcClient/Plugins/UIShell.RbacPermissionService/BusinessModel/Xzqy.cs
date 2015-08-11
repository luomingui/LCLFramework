using System;

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
    public class XzqyTreeModel
    {
        public XzqyTreeModel()
        {
            ProvinceId = Guid.Empty;
            CityId = Guid.Empty;
            CountyId = Guid.Empty;
        }
        public Guid ProvinceId { get; set; }
        public string Province { get; set; }
        public Guid CityId { get; set; }
        public string City { get; set; }
        public Guid CountyId { get; set; }
        public string County { get; set; }
    }
}
