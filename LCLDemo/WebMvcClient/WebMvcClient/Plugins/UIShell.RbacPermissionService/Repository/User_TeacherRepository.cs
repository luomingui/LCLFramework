/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 教师 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 20154月22日 星期三 
*   
*******************************************************/  
using LCL.Repositories;  
using LCL.Repositories.EntityFramework;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
  
namespace UIShell.RbacPermissionService  
{  
    public interface IUser_TeacherRepository : IRepository<User_Teacher>  
    {  
  
    }  
    public class User_TeacherRepository : EntityFrameworkRepository<User_Teacher>, IUser_TeacherRepository  
    {  
        public User_TeacherRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

