
using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using System.Data.Entity;
using System.Diagnostics;
using UIShell.Documents.Model;

namespace UIShell.Documents
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            Debug.WriteLine("UIShell.Documents Initialize....");
            app.AllPluginsIntialized += app_AllPluginsIntialized;
            app.ModuleOperations += app_ModuleOperations;
        }
        void app_AllPluginsIntialized(object sender, System.EventArgs e)
        {
            DatabaseInitializer.Initialize();

            ServiceLocator.Instance.Register<DbContext, DocContext>(LifeStyle.PerRequest);


            #region 默认仓库
            ServiceLocator.Instance.Register<IRepository<ProjectDocument>, EntityFrameworkRepository<ProjectDocument>>();

            #endregion

            #region 扩展仓库

            #endregion
        }
        void app_ModuleOperations(object sender, System.EventArgs e)
        {
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "文档中心",
                Image = "icon-sys",
                Bundle = this,
                OrderById = 0,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-add", Label = "文档首页", CustomUI="/UIShell.Documents/ProjectDocument/Index"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "文档管理",CustomUI="/UIShell.Documents/Org/List"},
                    }
            });
        }

    }
}
