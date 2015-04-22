/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 企业员工 
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
    public interface IUser_EmployeeRepository : IRepository<User_Employee>  
    {  
  
    }  
    public class User_EmployeeRepository : EntityFrameworkRepository<User_Employee>, IUser_EmployeeRepository  
    {  
        public User_EmployeeRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

