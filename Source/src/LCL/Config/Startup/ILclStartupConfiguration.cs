using LCL.Config.Startup;
using System;
using System.Collections.Generic;

namespace LCL.Config
{
    public interface ILclStartupConfiguration : IDictionaryBasedConfig
    {
        string DefaultNameOrConnectionString { get; set; }
        IModuleConfigurations Modules { get; }
        void ReplaceService(Type type, Action replaceAction);
        TService Get<TService>() where TService : class;
    }

    internal class LclStartupConfiguration : DictionaryBasedConfig, ILclStartupConfiguration
    {
        public string DefaultNameOrConnectionString { get; set; }
        public IModuleConfigurations Modules { get; private set; }
        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }
        public void Initialize()
        {
            Modules = RF.Service<IModuleConfigurations>();
            //Settings = IocManager.Resolve<ISettingsConfiguration>();
            //EventBus = IocManager.Resolve<IEventBusConfiguration>();
            //Caching = IocManager.Resolve<ICachingConfiguration>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }
        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public TService Get<TService>() where TService : class
        {
            return GetOrCreate(typeof(TService).FullName, () => RF.Service<TService>());
        }
    }
}
