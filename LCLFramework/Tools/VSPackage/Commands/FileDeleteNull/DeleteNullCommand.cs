/*******************************************************
 * 
 * Copyright(c)2012-2018 Luomg.All rights reserved.
 * CLR版本：.NET 4.0.0
 * 组织：Luomg@中国
 * 网站：http://luomingui.cnblogs.com
 * 说明：
 *
 * 历史记录：
 * 创建文件 Luomg 20150730
 * 
*******************************************************/
using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LCL.VSPackage.Commands.FileDeleteNull
{
    /// <summary>
    /// 删除当前文档空行
    /// </summary>
    class DeleteNullCommand: Command
    {
        public DeleteNullCommand()
        {
            this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidDeleteNullCommandCommand);
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
        protected override void ExecuteCore()
        {
            var activeDoc = DTE.ActiveDocument;
            if (activeDoc != null)
            {
                this.TryAddFileHeader(activeDoc, false);
            }
        }
    }
}
