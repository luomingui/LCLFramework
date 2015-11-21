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
                        try
                        {
                            library.Initialize(_appCore);
                            Logger.LogInfo(library.Assembly.FullName + " Initialize....");
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError("【" + library.Assembly.FullName + "】组件初始化失败！", ex);
                        }
                    }
                }
            }
        }
        private static List<PluginAssembly> _libraries;
        public static List<string> GetPluginsName()
        {
            if (_libraries == null)
            {
                Logger.LogWarn("LEnvironment libraries==null:" + LCL.LEnvironment.Provider.PluginsDirectory);
            }
            return _libraries.Select(p => p.Assembly.GetName().Name).ToList();
        }
        public static List<PluginAssembly> GetAllPluginAssembly()
        {
            if (_libraries == null)
            {
                Logger.LogWarn("LEnvironment libraries==null:" + LCL.LEnvironment.Provider.PluginsDirectory);
            }
            return _libraries.ToList();
        }
        public static List<IPlugin> GetAllPlugins()
        {
            if (_libraries == null)
            {
                Logger.LogWarn("LEnvironment libraries==null:" + LCL.LEnvironment.Provider.PluginsDirectory);
            }
            return _libraries.Select(p => p.Instance).ToList();
        }
        public static PluginAssembly GetPlugin(string pluginName)
        {
            if (_libraries == null)
            {
                Logger.LogWarn("LEnvironment libraries==null:" + LCL.LEnvironment.Provider.PluginsDirectory);
            }
            return _libraries.FirstOrDefault(p => p.Assembly.GetName().Name == pluginName);
        }
        public static List<Assembly> GetAllPluginAssemblys()
        {
            if (_libraries == null)
            {
                Logger.LogWarn("LEnvironment libraries==null:" + LCL.LEnvironment.Provider.PluginsDirectory);
            }
            return _libraries.Select(p => p.Assembly).ToList();
        }
    }
}
