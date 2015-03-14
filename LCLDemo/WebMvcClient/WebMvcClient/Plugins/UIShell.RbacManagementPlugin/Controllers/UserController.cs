using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class UserController : BaseRepoController<User>
    {
        public UserController()
        {
            base.Bundle = BundleActivator.Bundle;
        }
    }
}