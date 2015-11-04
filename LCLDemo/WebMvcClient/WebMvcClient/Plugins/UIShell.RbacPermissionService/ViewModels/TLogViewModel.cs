using System.Collections.Generic; 
using UIShell.RbacPermissionService; 
 
namespace LCL.MvcExtensions 
{ 
   public class TLogAddOrEditViewModel : AddOrEditViewModel<TLog> 
    { 
        
    }
   public class TLogPagedListViewModel : PagedResult<TLog> 
    { 
        public TLogPagedListViewModel(int currentPageNum, int pageSize, List<TLog> allModels) 
            : base(currentPageNum, pageSize, allModels) 
        { 
 
        } 
    } 
 
} 

