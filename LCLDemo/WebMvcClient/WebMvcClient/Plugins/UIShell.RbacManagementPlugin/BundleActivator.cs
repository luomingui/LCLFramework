using LCL;
using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.MvcExtensions;
using System.Diagnostics;
using System.Web;
using UIShell.RbacManagementPlugin.Controllers;

namespace UIShell.RbacManagementPlugin
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            Debug.WriteLine("UIShell.RbacManagementPlugin Initialize....");
            PageFlowService.PageNodes.AddPageNode(new PageNode
            {
                Name = "LayoutLogin",
                Bundle = this,
                Value = @"UIShell.RbacManagementPlugin\Account\Login",
                Priority = 1
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "学校管理",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校管理",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index"},
                        new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index"},
                        new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index"},
                        new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "企业管理",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-sitemap", Label = "公司管理", CustomUI="/UIShell.RbacManagementPlugin/CompanyInfo/Index"},
                        new MvcModuleMeta{ Image="icon-user-md", Label = "员工管理",CustomUI="/UIShell.RbacManagementPlugin/User_Employee/Index"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "系统设置",
                Image = "icon-cog",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-group", Label = "角色管理", CustomUI="/UIShell.RbacManagementPlugin/Role/Index"},
                        new MvcModuleMeta{ Image="icon-user-md", Label = "用户管理", CustomUI="/UIShell.RbacManagementPlugin/User/Index"},
                        new MvcModuleMeta{ Image="icon-sitemap", Label = "行政区域", CustomUI="/UIShell.RbacManagementPlugin/Xzqy/Index"},
                        new MvcModuleMeta{ Image="icon-qrcode", Label = "字典管理", CustomUI="/UIShell.RbacManagementPlugin/DictType/Index"},
                        new MvcModuleMeta{ Image="icon-time", Label = "计划任务", CustomUI="/UIShell.RbacManagementPlugin/Schedule/Index"},
                        new MvcModuleMeta{ Image="icon-book", Label = "日志管理", CustomUI="/UIShell.RbacManagementPlugin/TLog/Index"},
                    }
            });
        }
    }
}
