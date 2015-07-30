using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using EnvDTE;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace LCL.VSPackage.Commands.FileHeader
{
    /// <summary>
    /// 添加文件头
    /// </summary>
    class FileHeaderCommands : Command
    {
        public FileHeaderCommands()
        {
            this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidFileHeaderCommandsCommand);
        }
        private void OnDocumentSaved(Document document)
        {
            this.TryAddFileHeader(document, true);
        }
        protected override void ExecuteCore()
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
                        MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.No) return;
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
 * CLR版本：.NET 4.0.0
 * 组织：Luomg@中国
 * 网站：http://luomingui.cnblogs.com
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
            
            content = content.Replace("$Now$", timeString);
            
            return content;
        }
    }
}