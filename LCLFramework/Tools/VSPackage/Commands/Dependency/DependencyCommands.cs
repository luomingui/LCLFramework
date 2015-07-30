using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using EnvDTE;
using System.ComponentModel.Design;

namespace LCL.VSPackage.Commands.Dependency
{
    /// <summary>
    /// 创建父子依赖
    /// 为两个文件创建父子依赖
    /// </summary>
    class CreateDependency : Command
    {
        public CreateDependency()
        {
            this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidCreateDependencyCommand);
        }
        protected override void ExecuteCore()
        {
            var items = DTE.SelectedItems.OfType<SelectedItem>().ToArray();
            if (items.Length == 2)
            {
                ProjectItem childProjectItem = items[0].ProjectItem;
                ProjectItem parentProjectItem = items[1].ProjectItem;

                var message = String.Format(
                    "是否需要把 '{0}' 创建父子依赖，变为 '{1}' 的子文件?",
                    childProjectItem.Name, parentProjectItem.Name
                    );

                var res = MessageBox.Show(message, "提示", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    parentProjectItem.ProjectItems.AddFromFile(childProjectItem.get_FileNames(1));
                    parentProjectItem.ExpandView();
                }
            }
        }
    }
    /// <summary>
    ///  "移除父子依赖";
    //   "为文件移除父子依赖";
    /// </summary>
    class DropDependency : Command
    {
        public DropDependency()
        {

            this.CommandID = new CommandID(GuidList.guidVSPackageCmdSet, PkgCmdIDList.cmdidDropDependencyCommand);
        }

        protected override void ExecuteCore()
        {
            var items = DTE.SelectedItems.OfType<SelectedItem>().ToArray();
            if (items.Length == 1)
            {
                ProjectItem childProjectItem = items[0].ProjectItem;
                if (childProjectItem.ProjectItems.Count > 0)
                {
                    MessageBox.Show("该项下面还有子项，请先移除所有子项。", "提示");
                    return;
                }
                var res = MessageBox.Show("是否需要把移除父子依赖成为独立的文件？\r\n", "提示", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    var fileName = childProjectItem.get_FileNames(0);
                    var tmp = Path.GetTempFileName();
                    File.Copy(fileName, tmp, true);
                    var p = childProjectItem.ContainingProject;
                    childProjectItem.Delete();
                    if (!File.Exists(fileName))
                    {
                        File.Copy(tmp, fileName, true);
                    }
                    p.ProjectItems.AddFromFile(fileName);
                }
            }
        }
    }
}