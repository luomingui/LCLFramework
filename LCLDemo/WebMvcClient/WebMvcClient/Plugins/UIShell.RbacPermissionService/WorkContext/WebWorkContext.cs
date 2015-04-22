using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UIShell.RbacPermissionService
{
    public partial class WebWorkContext : IWorkContext
    {
        public WebWorkContext()
        {

        }
        public GeneralConfigInfo Config
        {
            get {
                GeneralConfigInfo config = GeneralConfigs.GetConfig();
                return config;
            }
        }
    }
}
