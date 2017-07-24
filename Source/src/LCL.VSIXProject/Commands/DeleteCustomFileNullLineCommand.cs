//------------------------------------------------------------------------------
// <copyright file="DeleteCustomFileNullLineCommand.cs" company="微软中国">
//     Copyright (c) 微软中国.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using System.Text.RegularExpressions;

namespace LCL.VSIXProject.Commands
{
    /// <summary>
    /// 删除当前文档空行
    /// </summary>
    internal sealed class DeleteCustomFileNullLineCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("523c66c1-e25c-4324-9dc9-6045fedc7a25");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCustomFileNullLineCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private DeleteCustomFileNullLineCommand(Package package)
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
        public static DeleteCustomFileNullLineCommand Instance
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
            Instance = new DeleteCustomFileNullLineCommand(package);
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
        private void OnDocumentSaved(Document document)
        {
            this.TryAddFileHeader(document, true);
        }
        private void TryAddFileHeader(Document activeDoc, bool alert)
        {
            var selection = activeDoc.Selection as TextSelection;
            if (selection != null)
            {
                var ac = selection.ActivePoint.AbsoluteCharOffset;
                selection.SelectAll();
                var t = selection.Text.Replace("\r\n", string.Empty);
                selection.MoveToAbsoluteOffset(ac);
                Match m = Regex.Match(t, "");
                selection.StartOfDocument();
                selection.StartOfDocument();
                selection.FindText("$end$");
                selection.Delete();
            }
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
