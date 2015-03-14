using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class OrgController : BaseRepoController<Org>
    {
        public OrgController()
        {
            base.Bundle = BundleActivator.Bundle;
        }
    }
}
