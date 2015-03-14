
namespace LCL
{
    /// <summary>
    /// 系统的功能权限 id 列表
    /// 
    /// 此些 Id 会保存到数据库中，不能更改值。
    /// </summary>
    public class SystemOperationKeys
    {
        /// <summary>
        /// 是否可查看某对象的功能的权限标记。
        /// 模块根对象对应为打开模块功能，子对象对应为显示子对象功能。
        /// </summary>
        public static readonly string Read = "系统权限 - 查看";

        /// <summary>
        /// 是否可编辑某对象的功能的权限标记。
        /// </summary>
        public static readonly string Edit = "系统权限 - 编辑";
    }
}