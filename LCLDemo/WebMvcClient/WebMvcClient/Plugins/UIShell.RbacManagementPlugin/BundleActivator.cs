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
                        new MvcModuleMeta{ Image="icon-add", Label = "公司管理", CustomUI="/UIShell.RbacManagementPlugin/CompanyInfo/Index"},
                        new MvcModuleMeta{ Image="icon-nav", Label = "学校管理",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index"},
                       // new MvcModuleMeta{ Image="icon-role", Label = "小区管理", CustomUI="/UIShell.RbacManagementPlugin/Xzqy/Index"},
                        new MvcModuleMeta{ Image="icon-role", Label = "角色管理", CustomUI="/UIShell.RbacManagementPlugin/Role/Index"},
                        new MvcModuleMeta{ Image="icon-users", Label = "用户管理", CustomUI="/UIShell.RbacManagementPlugin/User/Index"},
                        new MvcModuleMeta{ Image="icon-role", Label = "行政区域", CustomUI="/UIShell.RbacManagementPlugin/Xzqy/Index"},
                        new MvcModuleMeta{ Image="icon-role", Label = "字典管理", CustomUI="/UIShell.RbacManagementPlugin/DictType/Index"},
                        new MvcModuleMeta{ Image="icon-role", Label = "计划任务", CustomUI="/UIShell.RbacManagementPlugin/Schedule/Index"},
                        new MvcModuleMeta{ Image="icon-role", Label = "日志管理", CustomUI="/UIShell.RbacManagementPlugin/TLog/Index"},
                        
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
            //_________________________________________________________________________________________________________________________
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "报修维修",
            //    Image = "icon-sys",
            //    Bundle = this,
            //    Children =
            //        {
            //            new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
            //        }
            //});
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "考试管理",
            //    Image = "icon-sys",
            //    Bundle = this,
            //    Children =
            //        {
            //            new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
            //        }
            //});
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "成绩管理",
            //    Image = "icon-sys",
            //    Bundle = this,
            //    Children =
            //        {
            //            new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
            //        }
            //});
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "教师培训",
            //    Image = "icon-sys",
            //    Bundle = this,
            //    Children =
            //        {
            //            new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
            //        }
            //});
            //CommonModel.Modules.AddRoot(new MvcModuleMeta
            //{
            //    Label = "资源管理",
            //    Image = "icon-sys",
            //    Bundle = this,
            //    Children =
            //        {
            //            new MvcModuleMeta{ Image="icon-nav",  Label = "单位信息", CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-users", Label = "个人信息",CustomUI="/UIShell.RbacManagementPlugin/Org/List"},
            //            new MvcModuleMeta{ Image="icon-nav", Label = "修改密码", CustomUI="/UIShell.RbacManagementPlugin/Users/List"},
            //        }
            //});
        }
    }
}
