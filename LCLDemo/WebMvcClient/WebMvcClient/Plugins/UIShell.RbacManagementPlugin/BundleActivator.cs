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
                Label = "成绩系统",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "综合素质评价系统",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "选修课系统",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "平安考勤系统",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "班级社区",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "教育博客",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "智能试题库",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "央馆教学资源库",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            //-----------------------------------------------------------------------------------------
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "学校管理",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image=" icon-sitemap", Label = "学校信息",CustomUI="/UIShell.RbacManagementPlugin/SchoolInfo/Index",EntityType=typeof(SchoolInfoController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师管理",CustomUI="/UIShell.RbacManagementPlugin/User_Teacher/Index",EntityType=typeof(User_TeacherController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "学生管理",CustomUI="/UIShell.RbacManagementPlugin/User_Student/Index",EntityType=typeof(User_StudentController)},
                    new MvcModuleMeta{ Image="icon-user", Label = "教师审核",CustomUI="/UIShell.RbacManagementPlugin/User_TeacherCheck/Index",EntityType=typeof(User_TeacherCheckController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "企业管理",
                Image = "icon-sitemap",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image="icon-sitemap", Label = "公司信息", CustomUI="/UIShell.RbacManagementPlugin/CompanyInfo/Index",EntityType=typeof(CompanyInfoController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "员工管理",CustomUI="/UIShell.RbacManagementPlugin/User_Employee/Index",EntityType=typeof(User_EmployeeController)},
                }
            });
            CommonModel.Modules.AddRoot(new MvcModuleMeta
            {
                Label = "系统设置",
                Image = "icon-cog",
                Bundle = this,
                Children =
                {
                    new MvcModuleMeta{ Image="icon-group", Label = "角色管理", CustomUI="/UIShell.RbacManagementPlugin/Role/Index",EntityType=typeof(RoleController)},
                    new MvcModuleMeta{ Image="icon-user-md", Label = "用户管理", CustomUI="/UIShell.RbacManagementPlugin/User/Index",EntityType=typeof(UserController)},
                    new MvcModuleMeta{ Image="icon-sitemap", Label = "行政区域", CustomUI="/UIShell.RbacManagementPlugin/Xzqy/Index",EntityType=typeof(XzqyController)},
                    new MvcModuleMeta{ Image="icon-qrcode", Label = "字典管理", CustomUI="/UIShell.RbacManagementPlugin/DictType/Index",EntityType=typeof(DictTypeController)},
                    new MvcModuleMeta{ Image="icon-time", Label = "计划任务", CustomUI="/UIShell.RbacManagementPlugin/Schedule/Index",EntityType=typeof(ScheduleController)},
                    new MvcModuleMeta{ Image="icon-book", Label = "日志管理", CustomUI="/UIShell.RbacManagementPlugin/TLog/Index",EntityType=typeof(TLogController)},
                }
            });
        }
    }
}
