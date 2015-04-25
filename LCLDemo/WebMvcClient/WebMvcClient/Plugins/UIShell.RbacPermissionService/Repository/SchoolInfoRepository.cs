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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
  
namespace UIShell.RbacPermissionService  
{  
    public interface ISchoolInfoRepository : IRepository<SchoolInfo>  
    {
        List<SchoolTeacherViewModel> GetSchoolTeacherList(Guid schoolId);
    }  
    public class SchoolInfoRepository : EntityFrameworkRepository<SchoolInfo>, ISchoolInfoRepository  
    {  
        public SchoolInfoRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }

        public List<SchoolTeacherViewModel> GetSchoolTeacherList(Guid schoolId)
        {
            string sql = @"SELECT u.ID,ui.TName,u.Name,u.Password,ut.IsGetZGZ,DegreeID,Sex,s.Email,
Telephone,UserQQ,IdCard,ui.Birthday,IsLockedOut FROM dbo.UnitInfo s
INNER JOIN dbo.User_Teacher ut ON s.ID = ut.UnitInfo_ID
INNER JOIN dbo.[User] u ON ut.User_ID = u.ID
INNER JOIN dbo.UserInfo ui ON u.ID = ui.ID WHERE s.ID='" + schoolId + "'";
            DataTable dt = DbFactory.DBA.QueryDataTable(sql);
            var arr = dt.ToArray<SchoolTeacherViewModel>();
            var list = arr.ToList();
            return list;
        }
    }  
}  

