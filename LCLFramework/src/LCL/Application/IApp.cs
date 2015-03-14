
using LCL.ComponentModel;
using System;
using System.ComponentModel;

namespace LCL
{
    /// <summary>
    /// 应用程序生成周期定义
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// 所有实体元数据初始化完毕，包括实体元数据之间的关系。
        /// </summary>
        event EventHandler AllPluginsIntialized;

        /// <summary>
        /// 模块的定义先于其它模型的操作。这样可以先设置好模板默认的按钮。
        /// 一般定义菜单，命令
        /// </summary>
        event EventHandler ModuleOperations;
        
    }
}
