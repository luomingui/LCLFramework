
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
