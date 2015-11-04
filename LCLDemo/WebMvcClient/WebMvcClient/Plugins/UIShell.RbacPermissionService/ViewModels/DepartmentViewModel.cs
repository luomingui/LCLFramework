using System;
using System.Collections.Generic;
using UIShell.RbacPermissionService;

namespace LCL.MvcExtensions
{
    public class DepartmentAddOrEditViewModel : AddOrEditViewModel<Department>
    {
        /// <summary>
        /// …œº∂
        /// </summary>
        public Guid superiorId { get; set; }
        public Guid superiorName { get; set; }
    }
    public class DepartmentPagedListViewModel : PagedResult<Department>
    {
        public DepartmentPagedListViewModel(int currentPageNum, int pageSize, List<Department> allModels)
            : base(currentPageNum, pageSize, allModels)
        {

        }
        public DepartmentType DepartmentType { get; set; }
        public Guid CURRPLACEID1 { get; set; }
        public Guid CURRPLACEID2 { get; set; }
        public Guid CURRPLACEID3 { get; set; }
    }

}

