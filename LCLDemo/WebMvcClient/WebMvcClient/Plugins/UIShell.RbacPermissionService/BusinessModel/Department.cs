
namespace UIShell.RbacPermissionService
{
    /// <summary>
    ///  部门
    /// </summary>
    public partial class Department : BaseTreeModel
    {
        public Department()
        {
            DepartmentType = DepartmentType.公司;
        }
        /// <summary>
        /// 行政区域
        /// </summary>
        public Xzqy Xzqy { get; set; }
        /// <summary>
        /// 部门类型
        /// </summary>
        public DepartmentType DepartmentType { get; set; }
        /// <summary>
        /// 办公名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 办公地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
    public enum DepartmentType
    {
        公司 = 1,
        部门 = 2,
        职位 = 2,
    }
}
