using System.Collections.Generic;
using UIShell.RbacPermissionService; 
 
namespace LCL.MvcExtensions 
{ 
   public class XzqyAddOrEditViewModel : AddOrEditViewModel<Xzqy> 
    { 
        
    }
   public class XzqyPagedListViewModel : PagedResult<Xzqy> 
    { 
        public XzqyPagedListViewModel(int currentPageNum, int pageSize, List<Xzqy> allModels) 
            : base(currentPageNum, pageSize, allModels) 
        { 
 
        } 
    }
} 

