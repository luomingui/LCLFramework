//------------------------------------------------------------------------------
// <copyright file="FileHeaderCommands.cs" company="微软中国">
//     Copyright (c) 微软中国.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;

namespace LCL.VSIXProject.Commands
{
    /// <summary>
    /// 添加文件头
    /// </summary>
    internal sealed class FileHeaderCommands
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4130;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("523c66c1-e25c-4324-9dc9-6045fedc7a25");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHeaderCommands"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private FileHeaderCommands(Package package)
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
        public static FileHeaderCommands Instance
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
            Instance = new FileHeaderCommands(package);
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
        public void ExecuteCore()
        {
            var activeDoc = DTE.ActiveDocument;
            if (activeDoc != null)
            {
                this.TryAddFileHeader(activeDoc, false);
            }
        }

        private void TryAddFileHeader(Document activeDoc, bool alert)
        {
            var selection = activeDoc.Selection as TextSelection;
            if (selection != null)
            {
                var ac = selection.ActivePoint.AbsoluteCharOffset;
                selection.SelectAll();
                var t = selection.Text;
                selection.MoveToAbsoluteOffset(ac);

                if (!t.Trim().StartsWith("/*") && alert)
                {
                    var res = MessageBox.Show("当前文档还没有添加文件头，是否立即添加文件头？", "提示",
                      MessageBoxButtons.YesNo);
                    if (res == DialogResult.No) return;
                }

                var content = this.GetTemplate();

                selection.StartOfDocument();
                selection.Insert(content);

                selection.StartOfDocument();
                selection.FindText("$end$");
                selection.Delete();
            }
        }
        private string GetTemplate()
        {
            var dir = this.GetType().Assembly.CodeBase.Replace("file:///", string.Empty);
            var file = Path.Combine(Path.GetDirectoryName(dir), "LuomgAddin_FileHeaderTemplate.xml");
            if (!File.Exists(file))
            {
                File.WriteAllText(file, @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<FileHeaderTemplate>
    <NowFormat>yyyyMMdd</NowFormat>
    <Content>
        <![CDATA[/*******************************************************
 * 
 * Copyright(c)2012-2018 Luomg.All rights reserved.
 * CLR版本：$verion$
 * 组织：Luomg@中国
 * 网站：https://github.com/luomingui/LCLFramework
 * 说明：$end$
 *
 * 历史记录：
 * 创建文件 Luomg $Now$
 * 
*******************************************************/
]]>
    </Content>
</FileHeaderTemplate>");
            }
            var xml = File.ReadAllText(file);

            var doc = XDocument.Load(new StringReader(xml));

            var nowFormat = doc.Descendants("NowFormat").First().Value;
            var content = doc.Descendants("Content").First().Value;

            var timeString = DateTime.Now.ToString(nowFormat);

            string ver = Environment.Version.ToString();

            content = content.Replace("$verion$", ver);
            content = content.Replace("$Now$", timeString);
            content = content.Replace("$end$", "");

            return content;
        }

        protected internal DTE DTE
        {
            get { return this.GetService(typeof(DTE)) as DTE; }
        }

        protected object GetService(Type type)
        {
            return this.ServiceProvider.GetService(type);
        }

        #endregion
    }
}
