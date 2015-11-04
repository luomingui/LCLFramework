using System.Collections.Generic; 
using UIShell.RbacPermissionService; 
 
namespace LCL.MvcExtensions 
{ 
   public class RoleAddOrEditViewModel : AddOrEditViewModel<Role> 
    { 
        
    }
   public class RolePagedListViewModel : PagedResult<Role> 
    { 
        public RolePagedListViewModel(int currentPageNum, int pageSize, List<Role> allModels) 
            : base(currentPageNum, pageSize, allModels) 
        { 
 
        } 
    } 
 
} 

