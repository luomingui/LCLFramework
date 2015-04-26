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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    public interface IRoleRepository : IRepository<Role>
    {
        List<RoleAuthority> GetRoleAuthority(Guid roleId);
    }
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public List<RoleAuthority> GetRoleAuthority(Guid roleId)
        {
            string sql = @"SELECT * FROM RoleAuthority WHERE Role_ID='" + roleId + "' ORDER BY BlockKey";
            var dt = DbFactory.DBA.QueryDataTable(sql);
            var list = dt.ToArray<RoleAuthority>();
            return list.ToList();
        }
    }
}

