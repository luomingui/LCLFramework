using System;
using System.Collections.Generic;
using UIShell.RbacPermissionService;

namespace LCL.MvcExtensions
{
    public class WFActorAddOrEditViewModel : AddOrEditViewModel<WFActor>
    {

    }
    public class WFActorPagedListViewModel : PagedResult<WFActor>
    {
        public WFActorPagedListViewModel(int currentPageNum, int pageSize, List<WFActor> allModels)
            : base(currentPageNum, pageSize, allModels)
        {

        }
        public Guid RoutId { get; set; }
    }
}

