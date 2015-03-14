using LCL;
using LCL.ComponentModel;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UIShell.RbacManagementPlugin;

namespace UIShell.RbacPermissionService
{
    public class PluginActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            DatabaseInitializer.Initialize();
            Debug.WriteLine("UIShell.RbacPermissionService Initialize....");
            //设置权限提供程序为本模块中实体类
            PermissionMgr.Provider = new LCLPermissionProvider();
            app.AllPluginsIntialized += app_AllPluginsIntialized;
        }
        void app_AllPluginsIntialized(object sender, System.EventArgs e)
        {
            ServiceLocator.Instance.Register<ILCLIdentity, LCLIdentity>();
            ServiceLocator.Instance.Register<IRepositoryContext, EntityFrameworkRepositoryContext>();
            ServiceLocator.Instance.Register<DbContext, EFContext>();
            ServiceLocator.Instance.Register<IRepository<Org>, EntityFrameworkRepository<Org>>();
            ServiceLocator.Instance.Register<IRepository<Role>, EntityFrameworkRepository<Role>>();
            ServiceLocator.Instance.Register<IRepository<RoleAuthority>, EntityFrameworkRepository<RoleAuthority>>();
            ServiceLocator.Instance.Register<IRepository<User>, EntityFrameworkRepository<User>>();

        }
    }
}
