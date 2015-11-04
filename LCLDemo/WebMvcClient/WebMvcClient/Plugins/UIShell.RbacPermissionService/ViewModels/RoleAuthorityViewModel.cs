using System.Collections.Generic; 
using UIShell.RbacPermissionService; 
 
namespace LCL.MvcExtensions 
{ 
   public class RoleAuthorityAddOrEditViewModel : AddOrEditViewModel<RoleAuthority> 
    { 
        
    }
   public class RoleAuthorityPagedListViewModel : PagedResult<RoleAuthority> 
    { 
        public RoleAuthorityPagedListViewModel(int currentPageNum, int pageSize, List<RoleAuthority> allModels) 
            : base(currentPageNum, pageSize, allModels) 
        { 
 
        } 
    } 
 
} 

