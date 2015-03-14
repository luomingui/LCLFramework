using LCL.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LCL
{
    /// <summary>
    ///  插件环境
    ///  PluginEnvironment
    /// </summary>
    public partial class LEnvironment
    {
        internal static void StartupPlugins()
        {
            Logger.LogInfo("LCL LEnvironment StartupPlugins ....");
            var libraries = PluginLoader.Initialize();
            _libraries = libraries;
            foreach (var pluginAssembly in libraries)
            {
                if (pluginAssembly.Instance != null)
                {
                    //调用 ILibrary
                    var library = pluginAssembly.Instance as LCLPlugin;
                    if (library != null)
                    {
                        library.Initialize(_appCore);
                    }
                }
            }
//#if !DEBUG
//            PluginWatcher.Start();
//#endif
        }
        private static List<PluginAssembly> _libraries;
        public static List<string> GetPluginsName()
        {
            return _libraries.Select(p => p.Assembly.GetName().Name).ToList();
        }
        public static List<PluginAssembly> GetAllPluginAssembly()
        {
            return _libraries.ToList();
        }
        public static List<IPlugin> GetAllPlugins()
        {
            return _libraries.Select(p => p.Instance).ToList();
        }
        public static PluginAssembly GetPlugin(string pluginName)
        {
            return _libraries.FirstOrDefault(p => p.Assembly.GetName().Name == pluginName);
        }
        public static List<Assembly> GetAllPluginAssemblys()
        {
            return _libraries.Select(p => p.Assembly).ToList();
        }
    }
}
