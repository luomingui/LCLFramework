using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;
using UIShell.RbacManagementPlugin.Controllers;

namespace UIShell.RbacManagementPlugin
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            app.ModuleOperations += app_ModuleOperations;
        }
        void app_ModuleOperations(object sender, System.EventArgs e)
        {
            PageFlowService.PageNodes.AddPageNode(new PageNode
            {
                Name = "LayoutLogin",
                Bundle = this,
                Value = @"UIShell.RbacManagementPlugin\Account\Login",
                Priority = 1
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "基础数据",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image="icon-sitemap", Label = "单位管理", CustomUI="/UIShell.RbacManagementPlugin/Department/Index",EntityType=typeof(DepartmentController)},
                    new MvcModuleMeta{ Image="icon-group", Label = "角色管理", CustomUI="/UIShell.RbacManagementPlugin/Role/Index",EntityType=typeof(RoleController)},
                    new MvcModuleMeta{ Image="icon-group", Label = "用户管理", CustomUI="/UIShell.RbacManagementPlugin/User/Index",EntityType=typeof(RoleController)},
                    new MvcModuleMeta{ Image="icon-book", Label = "日志管理", CustomUI="/UIShell.RbacManagementPlugin/TLog/Index",EntityType=typeof(TLogController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "系统管理",
                Image = "icon-cog",
                Bundle = this,
                Children =
                {
                      new MvcModuleMeta{ Image="icon-cogs", Label = "系统设置", CustomUI="/UIShell.RbacManagementPlugin/GeneralConfigInfo/Index",EntityType=typeof(GeneralConfigInfoController)},
                    new MvcModuleMeta{ Image="icon-sitemap", Label = "行政区域", CustomUI="/UIShell.RbacManagementPlugin/Xzqy/Index",EntityType=typeof(XzqyController)},
                    new MvcModuleMeta{ Image="icon-qrcode", Label = "字典管理", CustomUI="/UIShell.RbacManagementPlugin/DictType/Index",EntityType=typeof(DictTypeController)},
                    new MvcModuleMeta{ Image="icon-time", Label = "计划任务", CustomUI="/UIShell.RbacManagementPlugin/Schedule/Index",EntityType=typeof(ScheduleController)},   
                }
            });
        }
    }
}
