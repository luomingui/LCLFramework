using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace LCL.VSPackage.Commands.RefreshCodeSnippets
{
    /// <summary>
    /// 注册所有的 CodeSnippets
    /// </summary>
    class InstallCodeSnippetsCommand : Command
    {
        public InstallCodeSnippetsCommand()
        {
            this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidRefreshCodeSnippetsCommand);
        }

        protected override void ExecuteCore()
        {
            var res = MessageBox.Show("点击‘是’安装代码段，点击‘否’删除代码段。", "安装/卸载", MessageBoxButton.YesNoCancel);

            if (res != MessageBoxResult.Cancel)
            {
                string[] files = GetFiles();
                var dir = GetSnippetsDir();

                if (res == MessageBoxResult.Yes)
                {
                    foreach (var name in files)
                    {
                        var resource = string.Format("LCL.VSPackage._CodeSnippets.{0}.snippet", name);
                        var content = Helper.GetResourceContent(typeof(VSPackagePackage).Assembly, resource);

                        var path = Path.Combine(dir, name + ".snippet");
                        File.WriteAllText(path, content);
                    }
                }
                else if (res == MessageBoxResult.No)
                {
                    foreach (var name in files)
                    {
                        var path = Path.Combine(dir, name + ".snippet");
                        File.Delete(path);
                    }
                }

                //MessageBox.Show("操作完成，需要重启后才能生效。", "提示");
                var res2 = MessageBox.Show("操作完成，可能需要重启 VS 后才能生效，是否打开代码段文件夹检查？", "提示", MessageBoxButton.YesNo);
                if (res2 == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(dir);
                }
            }
        }

        private static string[] GetFiles()
        {
            string[] files = new string[]{
                "LCL_AggregateRoot",
                "LCL_DbContext",
                "LCL_DomainService",
                "LCL_ModulePlugin",
                "LCL_MyRepository",
                "LCL_ServiceLocator",
            };
            return files;
        }

        private string GetSnippetsDir()
        {
            string path = Helper.GetVsShellDir(ServiceProvider);
            MessageBox.Show(path);
            var res = Path.Combine(path, @"VC#\Snippets\2052\Visual C#");//中文版本
            if (!Directory.Exists(res))
            {
                res = Path.Combine(path, @"VC#\Snippets\1033\Visual C#");
            }
            return res;
        }
    }
}