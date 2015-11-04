using System;
using System.Collections.Generic;
using UIShell.RbacPermissionService;

namespace LCL.MvcExtensions
{
    public class DictionaryAddOrEditViewModel : AddOrEditViewModel<Dictionary>
    {
        public Guid DicTypeId { get; set; }
    }
    public class DictionaryPagedListViewModel : PagedResult<Dictionary>
    {
        public DictionaryPagedListViewModel(int currentPageNum, int pageSize, List<Dictionary> allModels)
            : base(currentPageNum, pageSize, allModels)
        {

        }
        public Guid DicTypeId { get; set; }
    }
}

