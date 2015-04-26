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
            ServiceLocator.Instance.Register<DbContext, EFContext>(LifeStyle.PerRequest);
            ServiceLocator.Instance.Register<ILCLIdentity, LCLIdentity>();
            ServiceLocator.Instance.Register<IRepositoryContext, EntityFrameworkRepositoryContext>();
            #region 默认仓库 
            ServiceLocator.Instance.Register<IRepository<CompanyInfo>, EntityFrameworkRepository<CompanyInfo>>();
            ServiceLocator.Instance.Register<IRepository<Department>, EntityFrameworkRepository<Department>>();
            ServiceLocator.Instance.Register<IRepository<Dictionary>, EntityFrameworkRepository<Dictionary>>();
            ServiceLocator.Instance.Register<IRepository<DictType>, EntityFrameworkRepository<DictType>>();
            ServiceLocator.Instance.Register<IRepository<Role>, EntityFrameworkRepository<Role>>();
            ServiceLocator.Instance.Register<IRepository<RoleAuthority>, EntityFrameworkRepository<RoleAuthority>>();
            ServiceLocator.Instance.Register<IRepository<ScheduledEvents>, EntityFrameworkRepository<ScheduledEvents>>();
            ServiceLocator.Instance.Register<IRepository<SchoolInfo>, EntityFrameworkRepository<SchoolInfo>>();
            ServiceLocator.Instance.Register<IRepository<TLog>, EntityFrameworkRepository<TLog>>();
            ServiceLocator.Instance.Register<IRepository<UnitInfo>, EntityFrameworkRepository<UnitInfo>>();
            ServiceLocator.Instance.Register<IRepository<User>, EntityFrameworkRepository<User>>();
            ServiceLocator.Instance.Register<IRepository<User_Employee>, EntityFrameworkRepository<User_Employee>>();
            ServiceLocator.Instance.Register<IRepository<User_Student>, EntityFrameworkRepository<User_Student>>();
            ServiceLocator.Instance.Register<IRepository<User_Teacher>, EntityFrameworkRepository<User_Teacher>>();
            ServiceLocator.Instance.Register<IRepository<User_TeacherCheck>, EntityFrameworkRepository<User_TeacherCheck>>();
            ServiceLocator.Instance.Register<IRepository<UserInfo>, EntityFrameworkRepository<UserInfo>>();
            ServiceLocator.Instance.Register<IRepository<Xzqy>, EntityFrameworkRepository<Xzqy>>();
            #endregion

            #region 扩展仓库
            ServiceLocator.Instance.Register<ICompanyInfoRepository, CompanyInfoRepository>();
            ServiceLocator.Instance.Register<IDepartmentRepository, DepartmentRepository>();
            ServiceLocator.Instance.Register<IDictionaryRepository, DictionaryRepository>();
            ServiceLocator.Instance.Register<IDictTypeRepository, DictTypeRepository>();
            ServiceLocator.Instance.Register<IRoleRepository, RoleRepository>();
            ServiceLocator.Instance.Register<IRoleAuthorityRepository, RoleAuthorityRepository>();
            ServiceLocator.Instance.Register<IScheduledEventsRepository, ScheduledEventsRepository>();
            ServiceLocator.Instance.Register<ISchoolInfoRepository, SchoolInfoRepository>();
            ServiceLocator.Instance.Register<ITLogRepository, TLogRepository>();
            ServiceLocator.Instance.Register<IUnitInfoRepository, UnitInfoRepository>();
            ServiceLocator.Instance.Register<IUserRepository, UserRepository>();
            ServiceLocator.Instance.Register<IUser_EmployeeRepository, User_EmployeeRepository>();
            ServiceLocator.Instance.Register<IUser_StudentRepository, User_StudentRepository>();
            ServiceLocator.Instance.Register<IUser_TeacherRepository, User_TeacherRepository>();
            ServiceLocator.Instance.Register<IUser_TeacherCheckRepository, User_TeacherCheckRepository>();
            ServiceLocator.Instance.Register<IUserInfoRepository, UserInfoRepository>();
            ServiceLocator.Instance.Register<IXzqyRepository, XzqyRepository>();
            #endregion
        }
    }
}
