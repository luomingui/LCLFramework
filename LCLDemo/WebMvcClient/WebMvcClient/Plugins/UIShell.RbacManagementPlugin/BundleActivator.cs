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
                Label = "系统设置",
                Image = "icon-sys",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-add", Label = "公司管理", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "部门管理",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-role", Label = "岗位管理", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-role", Label = "角色管理", CustomUI="/UIShell.RbacManagementPlugin/Role/Index"},
                        new MvcModuleMeta{ Image="icon-users", Label = "用户管理", CustomUI="/UIShell.RbacManagementPlugin/User/Index"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "个人中心",
                Image = "icon-sys",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
                    }
            });
            //-------------------------------------------------------
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "客户管理",
                Image = "icon-sys",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-nav",  Label = "添加客户", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-users", Label = "修改客户",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "客户列表", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "财务管理",
                Image = "icon-sys",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-nav",  Label = "收支分类", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-users", Label = "报表统计",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "添加支出", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
                    }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "员工管理",
                Image = "icon-sys",
                Bundle = this,
                Children =
                    {
                        new MvcModuleMeta{ Image="icon-nav",  Label = "添加员工", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-users", Label = "修改员工",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "员工列表", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
                    }
            });

            //-----------end--------------------
        }
    }
}
