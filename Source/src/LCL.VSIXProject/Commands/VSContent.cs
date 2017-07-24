using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace LCL.VSIXProject.Commands
{
    public abstract class VSContext
    {
        protected internal Package Package { get; internal set; }

        protected internal IServiceProvider ServiceProvider
        {
            get { return this.Package; }
        }

        protected internal DTE DTE
        {
            get { return this.GetService(typeof(DTE)) as DTE; }
        }

        protected object GetService(Type type)
        {
            return this.ServiceProvider.GetService(type);
        }

        /// <summary>
        /// 返回当前选择的项目列表。
        /// 注意，如果没有选中某个项目，则传入的列表是空列表。
        /// </summary>
        /// <returns></returns>
        protected List<Project> GetSelectedProjects()
        {
            var projects = new List<Project>();

            foreach (SelectedItem item in this.DTE.SelectedItems)
            {
                var project = item.Project;
                if (project != null)
                {
                    if (!projects.Contains(project))
                    {
                        projects.Add(project);
                    }
                }
            }
            return projects;
        }
    }

}
