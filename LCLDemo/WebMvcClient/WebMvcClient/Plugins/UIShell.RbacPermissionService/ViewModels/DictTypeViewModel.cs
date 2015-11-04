using System.Collections.Generic; 
using UIShell.RbacPermissionService; 
 
namespace LCL.MvcExtensions 
{ 
   public class DictTypeAddOrEditViewModel : AddOrEditViewModel<DictType> 
    { 
        
    }
   public class DictTypePagedListViewModel : PagedResult<DictType> 
    { 
        public DictTypePagedListViewModel(int currentPageNum, int pageSize, List<DictType> allModels) 
            : base(currentPageNum, pageSize, allModels) 
        { 
 
        } 
    } 
 
} 

