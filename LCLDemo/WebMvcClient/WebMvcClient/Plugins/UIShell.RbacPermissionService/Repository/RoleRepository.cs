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
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIShell.RbacPermissionService
{
    public interface IRoleRepository : IRepository<Role>
    {
        List<RoleAuthority> GetRoleAuthority(Guid roleId);
        List<Role> GetRoleByUserId(Guid userId);
        List<Role> GetRoleByUserId(string userName);
        bool AddRole(string userName, string roleName);
        bool AddUserToRoles(Guid userId, string[] roleNames);
        bool CheckRoleName(string RoleName);
    }
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        {

        }
        protected override void DoRemove(object keyValue)
        {
            string sql = @"DELETE RoleUser WHERE Role_ID='{0}';DELETE [User] WHERE id IN(SELECT [User_ID] FROM RoleUser WHERE Role_ID='{0}');DELETE [Role] WHERE ID='{0}';";
            DbFactory.DBA.ExecuteText(string.Format(sql, keyValue));
        }
        public List<RoleAuthority> GetRoleAuthority(Guid roleId)
        {
            string sql = @"SELECT * FROM RoleAuthority WHERE Role_ID='" + roleId + "' ORDER BY BlockKey";
            var dt = DbFactory.DBA.QueryDataTable(sql);
            var list = dt.ToArray<RoleAuthority>();
            return list.ToList();
        }
        public List<Role> GetRoleByUserId(Guid userId)
        {
            var dt = DbFactory.DBA.QueryDataTable(@"SELECT r.* FROM Role r INNER JOIN RoleUser ru ON ru.Role_ID = r.ID 
INNER JOIN [User] u ON u.ID = ru.User_ID 
WHERE u.ID='" + userId + "'");
            var list = dt.ToArray<Role>();

            return list.ToList();
        }
        public List<Role> GetRoleByUserId(string userName)
        {
            var dt = DbFactory.DBA.QueryDataTable(@"SELECT r.* FROM Role r INNER JOIN RoleUser ru ON ru.Role_ID = r.ID 
INNER JOIN [User] u ON u.ID = ru.User_ID 
WHERE u.Name='" + userName + "'");
            var list = dt.ToArray<Role>();

            return list.ToList();
        }
        public bool AddRole(string userName, string roleName)
        {
            try
            {
                string sql = @"INSERT INTO [RoleUser]([Role_ID],[User_ID])VALUES('{0}','{1}')";
                Guid roleId = Guid.Parse(DbFactory.DBA.QueryValue("SELECT ID FROM [Role] WHERE Name='" + roleName + "'").ToString());
                Guid userId = Guid.Parse(DbFactory.DBA.QueryValue("SELECT * FROM [User] WHERE Name='" + userName + "'").ToString());
                DbFactory.DBA.ExecuteText(string.Format(sql, roleId, userId));

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("添加" + userName + "用户到" + roleName + "角色错误：", ex);
                return false;
            }
        }
        public bool AddUserToRoles(Guid userId, string[] roleNames)
        {
            try
            {
                string sql = @"INSERT INTO [RoleUser]([Role_ID],[User_ID])VALUES('{0}','{1}')";
                for (int i = 0; i < roleNames.Length; i++)
                {
                    var roleId = Guid.Parse(DbFactory.DBA.QueryValue("SELECT ID FROM [Role] WHERE Name='" + roleNames[i] + "'").ToString());
                    DbFactory.DBA.ExecuteText(string.Format(sql, roleId, userId));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckRoleName(string RoleName)
        {
            try
            {
                var spec = Specification<Role>.Eval(p => p.Name == RoleName);
                return this.DoExists(spec);
            }
            catch (System.Exception)
            {
                bool isValidate = false;
                var dt = DbFactory.DBA.QueryDataTable("SELECT * FROM Role WHERE Name='" + RoleName + "'");
                if (dt.Rows.Count > 0)
                    isValidate = true;
                return isValidate;
            }
        }
    }
}


