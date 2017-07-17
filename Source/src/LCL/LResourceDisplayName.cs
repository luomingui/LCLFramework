using LCL.Domain.Services;
using LCL.Infrastructure;

namespace LCL
{
    public interface IModelAttribute
    {
        string Name { get; }
    }
    public class LResourceDisplayName : System.ComponentModel.DisplayNameAttribute, IModelAttribute
    {
        private string _resourceValue = string.Empty;

        public LResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                return localizationService.GetResource(ResourceKey);
            }
        }

        public string Name
        {
            get { return "LResourceDisplayName"; }
        }
    }
}
