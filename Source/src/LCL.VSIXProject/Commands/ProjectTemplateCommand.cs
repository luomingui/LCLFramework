//------------------------------------------------------------------------------
// <copyright file="ProjectTemplateCommand.cs" company="微软中国">
//     Copyright (c) 微软中国.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Forms;
using System.IO;

namespace LCL.VSIXProject.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ProjectTemplateCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4131;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("523c66c1-e25c-4324-9dc9-6045fedc7a25");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplateCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ProjectTemplateCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ProjectTemplateCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ProjectTemplateCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ExecuteCore();
        }

        #region MyRegion
        protected void ExecuteCore()
        {
            var res = MessageBox.Show("点击‘是’安装项目模板，点击‘否’删除项目模板。", "安装/卸载",
                MessageBoxButtons.YesNoCancel);

            if (res != DialogResult.Cancel)
            {
                string[] files = GetFiles();
                var dir = GetSnippetsDir();

                if (res == DialogResult.Yes)
                {
                    foreach (var name in files)
                    {
                        var resource = string.Format("LCL.VSIXProject.Templates.ProjectTemplates.{0}.zip", name);
                        var content = Helper.GetResourceContent(typeof(VSPackagePackage).Assembly, resource);

                        var path = Path.Combine(dir, name + ".zip");
                        File.WriteAllText(path, content);
                    }
                }
                else if (res == DialogResult.No)
                {
                    foreach (var name in files)
                    {
                        var path = Path.Combine(dir, name + ".zip");
                        File.Delete(path);
                    }
                }

                //MessageBox.Show("操作完成，需要重启后才能生效。", "提示");
                var res2 = MessageBox.Show("操作完成，可能需要重启 VS 后才能生效，是否打开代码段文件夹检查？", "提示", MessageBoxButtons.YesNo);
                if (res2 == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(dir);
                }
            }
        }

        private static string[] GetFiles()
        {
            string[] files = new string[]{
                "UIShell.AdminiseShellCoursePlugin",
                "UIShell.AdminiseShellPlugin",
                "UIShell.AdvancedMVC4Shell",
                "UIShell.EFDomainService",
                "UIShell.EmptyClassLibraryPlugin",
                "UIShell.MVC4EmptyPlugin",
                "UIShell.OlsonAdminShellCoursePlugin",
                "UIShell.OlsonAdminShellPlugin",
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
        #endregion
    }
}
