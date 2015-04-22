/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 学生 
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
    public interface IUser_StudentRepository : IRepository<User_Student>  
    {  
  
    }  
    public class User_StudentRepository : EntityFrameworkRepository<User_Student>, IUser_StudentRepository  
    {  
        public User_StudentRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

