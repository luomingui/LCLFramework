using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;

namespace LCL.ComponentModel
{
    /// <summary>
    /// 加载插件
    /// 1：复制插件到临时目录
    /// 2：把插件加入到AppDomain中
    /// </summary>
    public class PluginLoader
    {
        private static readonly DirectoryInfo PluginFolder;
        private static readonly DirectoryInfo TempPluginFolder;
        private static readonly DirectoryInfo FrameworkPrivateBin;
        private static readonly List<string> FrameworkPrivateBinFiles;
        static PluginLoader()
        {
            try
            {
                PluginFolder = new DirectoryInfo(LEnvironment.Provider.PluginsDirectory);

                if (AppDomain.CurrentDomain.DynamicDirectory != null)
                    TempPluginFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
                else
                {
                    TempPluginFolder = new DirectoryInfo(LEnvironment.Provider.PluginsShadowCopyDirectory);
                    Directory.CreateDirectory(TempPluginFolder.FullName);
                }
#if DEBUG
                TempPluginFolder = new DirectoryInfo(LEnvironment.Provider.PluginsShadowCopyDirectory);
                Directory.CreateDirectory(TempPluginFolder.FullName);
#endif
                if (System.AppDomain.CurrentDomain.SetupInformation.PrivateBinPath != null)
                {
                    FrameworkPrivateBin = new DirectoryInfo(System.AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
                }
                else
                {
                    FrameworkPrivateBin = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
                }
                FrameworkPrivateBinFiles = FrameworkPrivateBin.GetFiles("*.dll").Select(p => p.Name).ToList();

                Directory.CreateDirectory(PluginFolder.FullName);
            }
            catch (Exception ex)
            {
                var ss = ex.Message;
                Logger.LogError("检查插件目录错误 PluginLoader ctor ：", ex);
            }
        }
        public static List<PluginAssembly> Initialize()
        {
            //程序集复制到临时目录。
            CopyToTempPluginFolderDirectory();

            //加载 bin 目录下的所有程序集。
            Logger.LogInfo("加载 bin 目录下的所有程序集");
            List<Assembly> assemblies = FrameworkPrivateBin.GetFiles("*.dll").Select(x => Assembly.LoadFile(x.FullName)).ToList();

            Logger.LogInfo("加载临时目录下的所有程序集");
            List<PluginAssembly> plugins;

            var list = TempPluginFolder.GetFiles("*.dll")
              .Select(x => Assembly.LoadFile(x.FullName)).ToList().FindAll(p => assemblies.Contains(p) == false);

            list.AddRange(assemblies);

            plugins = GetAssemblies(list);

            return plugins;
        }
        private static List<PluginAssembly> GetAssemblies(IEnumerable<Assembly> assemblies)
        {
            Logger.LogInfo("LCL GetAssemblies PluginAssembly ....");
            List<PluginAssembly> plugins = new List<PluginAssembly>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var pluginTypes = assembly.GetTypes().Where(type => type.GetInterface(typeof(IPlugin).Name) != null
                                      && type.IsClass && !type.IsAbstract);
                    foreach (var pluginType in pluginTypes)
                    {
                        var plugin = GetPluginInstance(pluginType, assembly);
                        if (plugin != null)
                        {
                            plugins.Add(plugin);
                            Logger.LogInfo("LCL GetAssemblies PluginAssembly " + plugin.Assembly.GetName().Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("LCL GetAssemblies PluginAssembly " + assembly.GetName().Name, ex);
                }
            }
            return plugins.OrderBy(a => a.Instance.SetupLevel).ToList();
        }
        private static PluginAssembly GetPluginInstance(Type pluginType, Assembly assembly)
        {
            if (pluginType != null)
            {
                var plugin = Activator.CreateInstance(pluginType) as IPlugin;
                if (plugin != null)
                {
                    return new PluginAssembly(assembly, plugin);
                }
            }
            return null;
        }
        private static void CopyToTempPluginFolderDirectory()
        {
            #region 清空临时目录
            Logger.LogInfo("清空临时目录:" + TempPluginFolder.FullName);
            if (TempPluginFolder.FullName.Contains("Bin") || TempPluginFolder.FullName.Contains("root"))
            {
                var binFiles = TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                    .Where(p => FrameworkPrivateBinFiles.Contains(p.Name) == false);
                foreach (var f in binFiles)
                {
                    Logger.LogInfo("Deleting " + f.Name);
                    try
                    {
                        File.Delete(f.FullName);
                    }
                    catch (Exception exc)
                    {
                        Logger.LogError("Error deleting file " + f.Name + ".", exc);
                    }
                }
            }
            #endregion
            #region 复制插件到临时目录
            Logger.LogInfo("复制插件到临时目录:");
            var files = PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories).Distinct().ToList();
            foreach (var plug in files)
            {
                if (!FrameworkPrivateBinFiles.Contains(plug.Name) && !plug.FullName.Contains("obj"))
                {
                    var shadowCopiedPlug = new FileInfo(Path.Combine(TempPluginFolder.FullName, plug.Name));
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
#if DEBUG
                    //if (plug.FullName.EndsWith(".dll"))
                    //{
                    //    var srcPdbPath = plug.FullName.Substring(0, plug.FullName.Length - 4) + ".pdb";
                    //    var pdfName = plug.Name.Substring(0, plug.Name.Length - 4) + ".pdb";
                    //    var toPdbPath = Path.Combine(TempPluginFolder.FullName, pdfName);
                    //    if (File.Exists(srcPdbPath))
                    //    {
                    //        File.Copy(srcPdbPath, toPdbPath, true);
                    //    }
                    //}
#endif
                    Logger.LogInfo(plug.FullName + " to " + shadowCopiedPlug.FullName);
                }
            }
            #endregion
        }
    }
}

