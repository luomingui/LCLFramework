using LCL.Data;
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
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
  
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
            string sql = @"INSERT INTO RoleAuthority (ID,BlockKey,ModuleKey,OperationKey,
AuthorityType,AddDate,UpdateDate,Role_ID,url)VALUES(@ID,@BlockKey,
@ModuleKey,@OperationKey,@AuthorityType,@AddDate,@UpdateDate,@Role_ID,@Url)";

            sql = SqlUtil.setString(sql, "@ID", entity.ID.ToString());
            sql = SqlUtil.setString(sql, "@BlockKey", entity.BlockKey.ToString());
            sql = SqlUtil.setString(sql, "@ModuleKey", entity.ModuleKey.ToString());
            sql = SqlUtil.setString(sql, "@OperationKey", entity.OperationKey.ToString());
            sql = SqlUtil.setNumber(sql, "@AuthorityType", ((int)entity.AuthorityType).ToString());
            sql = SqlUtil.setString(sql, "@AddDate", entity.AddDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = SqlUtil.setString(sql, "@UpdateDate", entity.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            sql = SqlUtil.setString(sql, "@Role_ID", entity.Role.ID.ToString());
            sql = SqlUtil.setString(sql, "@Url", entity.Url.ToString());

            int ret = DbFactory.DBA.ExecuteText(sql);
            return ret;
        }
        public bool IsAuthority(string url)
        {
            ISpecification<RoleAuthority> spec = Specification<RoleAuthority>.Eval(p => p.Url == url);
            var model = this.Find(spec);
            if (model != null)
                return false;
            else
                return true;
        }
    }  
}  

