
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LCL.ComponentModel
{
    /// <summary>
    /// 表示某一个插件程序集
    /// </summary>
    [DebuggerDisplay("{Assembly.FullName}")]
    public class PluginAssembly
    {
        public PluginAssembly(Assembly assembly, IPlugin instance)
        {
            this.Instance = instance;
            this.Assembly = assembly;
        }
        /// <summary>
        /// 程序集当中的插件对象。
        /// 如果插件中没有定义，则此属性为 null。
        /// </summary>
        public IPlugin Instance { get; private set; }
        /// <summary>
        /// 程序集本身
        /// </summary>
        public Assembly Assembly { get; private set; }
    }
}
