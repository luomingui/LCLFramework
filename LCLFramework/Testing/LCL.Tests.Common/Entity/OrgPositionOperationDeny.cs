namespace LCL.Tests.Common
{
    /// <summary>
    /// 岗位功能权限
    /// </summary>
    public class OrgPositionOperationDeny : MyEntity
    {
        public string BlockKey { set; get; }
        public string ModuleKey { set; get; }
        public string OperationKey { set; get; }
    }
}
