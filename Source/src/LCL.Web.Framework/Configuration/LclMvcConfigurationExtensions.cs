using LCL.Config.Startup;

namespace LCL.Web.Framework.Configuration
{
    public static class LclMvcConfigurationExtensions
    {
        public static ILclMvcConfiguration LclMvc(this IModuleConfigurations configurations)
        {
            return configurations.LclConfiguration.Get<ILclMvcConfiguration>();
        }
    }
}
