using System;
using System.Collections.Generic;
using UIShell.RbacPermissionService;

namespace LCL.MvcExtensions
{
    public class UserAddOrEditViewModel : AddOrEditViewModel<User>
    {
        public UserAddOrEditViewModel()
        {
            LstRoles = new string[] { };
        }
        public Guid DepId { get; set; }
        public string[] LstRoles { get; set; }
    }
    public class UserPagedListViewModel : PagedResult<User>
    {
        public UserPagedListViewModel(int currentPageNum, int pageSize, List<User> allModels)
            : base(currentPageNum, pageSize, allModels)
        {

        }
        public Guid DepId { get; set; }
    }

}

