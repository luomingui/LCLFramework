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
        List<User> GetDepartmentUsers(Guid depId,string key);
        User GetUserByName(string name);
        User GetUserByLoginName();
        bool ChangePassword(string userId, string password);
        bool LockedUser(string userId);
        bool ChangeLoginPassword(UserChangePassword userChangePassword);
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
        public List<User> GetDepartmentUsers(Guid depId,string key)
        {
            var spec = Specification<User>.Eval(p => p.Department.ID == depId);
            if (!string.IsNullOrWhiteSpace(key))
            { 
              spec.And(new KeyUserSpecification(key,"Name"));
            }         
            return this.FindAll(spec).ToList();

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
        public bool ChangeLoginPassword(UserChangePassword userChangePassword)
        {
            try
            {
                var user = GetUserByLoginName();
                if (user != null)
                {
                    user.Password = userChangePassword.ConfirmPassword;
                    this.Update(user);
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
        public bool ChangePassword(string userId, string password)
        {
            try
            {
                ISpecification<User> spec = Specification<User>.Eval(p => p.ID == Guid.Parse(userId));
                var user = this.Find(spec);
                if (user != null)
                {
                    user.Password = password;
                    this.Update(user);
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


        public bool LockedUser(string userId)
        {
            try
            {
                ISpecification<User> spec = Specification<User>.Eval(p => p.ID == Guid.Parse(userId));
                var user = this.Find(spec);
                if (user != null)
                {
                    user.IsLockedOut = user.IsLockedOut ? false : true;
                    this.Update(user);
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
                Logger.LogError("锁定用户", ex);
                return false;
            }
        }
    }
}

