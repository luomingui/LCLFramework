/*******************************************************  
*   
* 作者：罗敏贵  
* 说明：  
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 20154月18日 星期六 
*   
*******************************************************/
using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIShell.RbacPermissionService
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetDepartmentUsers(Guid depId);
        User GetUserByName(string name);
        User GetUserByLoginName();
        bool ChangePassword(UserChangePassword userChangePassword);
        User GetBy(string code, string password);
    }
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public List<User> GetDepartmentUsers(Guid depId)
        {
            try
            {
                ISpecification<User> spec = Specification<User>.Eval(p => p.Department.ID == depId);
                return this.FindAll(spec).ToList();
            }
            catch (Exception)
            {
                var datatable = DbFactory.DBA.QueryDataTable("SELECT * FROM [User] WHERE Department_ID='" + depId + "'");
                return datatable.ToArray<User>().ToList();
            }
        }
        public User GetUserByName(string name)
        {
            ISpecification<User> spec = Specification<User>.Eval(p => p.Name == name);
            return this.Find(spec);
        }
        public User GetUserByLoginName()
        {
            try
            {
                ISpecification<User> spec = Specification<User>.Eval(p => p.Name == LCL.LEnvironment.Principal.Identity.Name);
                return this.Find(spec);
            }
            catch
            {
                return null;
            }
        }
        public bool ChangePassword(UserChangePassword userChangePassword)
        {
            try
            {
                var user = GetUserByLoginName();
                if (user != null)
                {
                    user.Password = userChangePassword.ConfirmPassword;
                    this.Create(user);
                    this.Context.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("修改密码", ex);
                return false;
            }
        }
        public User GetBy(string code, string password)
        {

            var user = this.GetUserByName(code);

            if (user != null && user.Password == password) { return user; }

            return null;
        }
    }
}

