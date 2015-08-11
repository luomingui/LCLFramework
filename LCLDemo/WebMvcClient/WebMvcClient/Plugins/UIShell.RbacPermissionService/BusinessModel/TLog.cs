using System;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    public partial class TLog : BaseModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 机器名
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EnumLogType LogType { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 支持ActiveX
        /// </summary>
        public bool IsActiveX { get; set; }
    }
}
