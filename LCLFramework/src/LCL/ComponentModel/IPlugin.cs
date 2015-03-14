/*******************************************************
 * 
 * 作者：罗敏贵
 * 说明：
 * 运行环境：.NET 4.0.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 罗敏贵 20141209
 * 
*******************************************************/

using System.Reflection;
namespace LCL.ComponentModel
{
    /// <summary>
    ///  插件定义。
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件对应的程序集。
        /// </summary>
        Assembly Assembly { get; }
        /// <summary>
        /// 插件的启动级别
        /// </summary>
        int SetupLevel { get; }

        /// <summary>
        /// 插件的初始化方法。
        /// 框架会在启动时根据启动级别顺序调用本方法。
        /// 
        /// 方法有两个职责：
        /// 1.依赖注入。
        /// 2.注册 app 生命周期中事件，进行特定的初始化工作。
        /// </summary>
        /// <param name="app">应用程序对象。</param>
        void Initialize(IApp app);
    }
}
