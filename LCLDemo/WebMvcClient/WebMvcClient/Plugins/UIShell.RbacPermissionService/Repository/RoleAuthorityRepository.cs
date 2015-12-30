using LCL;
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
using LCL.Data;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using System;
using System.Linq;

namespace UIShell.RbacPermissionService
{
    public interface IRoleAuthorityRepository : IRepository<RoleAuthority>
    {
        int AddDbo(RoleAuthority entity);
        bool IsAuthority(string url);
    }
    public class RoleAuthorityRepository : EntityFrameworkRepository<RoleAuthority>, IRoleAuthorityRepository
    {
        public RoleAuthorityRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public int AddDbo(RoleAuthority entity)
        {
            string sql = @"INSERT INTO RoleAuthority (ID,BlockKey,ModuleKey,OperationKey,[Level],NodePath,AuthorityType,AddDate,UpdateDate,Role_ID,url)
VALUES(@ID,@BlockKey,@ModuleKey,@OperationKey,@Level,@NodePath,@AuthorityType,@AddDate,@UpdateDate,@Role_ID,@Url)";

            sql = SqlUtil.setString(sql, "@ID", entity.ID.ToString());
            sql = SqlUtil.setString(sql, "@BlockKey", entity.BlockKey.ToString());
            sql = SqlUtil.setString(sql, "@ModuleKey", entity.ModuleKey.ToString());
            sql = SqlUtil.setString(sql, "@OperationKey", entity.OperationKey.ToString());

            sql = SqlUtil.setString(sql, "@Level", entity.Level.ToString());
            sql = SqlUtil.setString(sql, "@NodePath", entity.NodePath.ToString());

            sql = SqlUtil.setNumber(sql, "@AuthorityType", ((int)entity.AuthorityType).ToString());
            sql = SqlUtil.setString(sql, "@AddDate", entity.AddDate.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = SqlUtil.setString(sql, "@UpdateDate", entity.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = SqlUtil.setString(sql, "@Role_ID", entity.Role.ID.ToString());
            sql = SqlUtil.setString(sql, "@Url", entity.Url.ToString());

            int ret = DbFactory.DBA.ExecuteText(sql);
            return ret;
        }

        //TODO: 加入角色和管理员控制
        public bool IsAuthority(string url)
        {
            try
            {
                var user = RF.Concrete<IUserRepository>().GetUserByLoginName();
                if (user != null && user.Roles != null)
                {
                    var roleIds = user.Roles.Select(p => p.ID).ToArray();
                    if (roleIds != null && roleIds.Length > 0)
                    {
                        string str = string.Join(",", roleIds);
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            string sql = @"SELECT * FROM RoleAuthority WHERE url='" + url + "' AND  Role_ID IN(" + str + ")";
                            var datatable = DbFactory.DBA.QueryDataTable(sql);
                            if (datatable.Rows.Count > 0)
                                return true;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("权限控制", ex);
                return true;
            }
        }
    }
}

