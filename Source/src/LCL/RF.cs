using LCL.Caching;
using LCL.Config;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.LData;
using LCL.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    public static class RF
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        public static TService Service<TService>() where TService : class
        {
            if (!_services.ContainsKey(typeof(TService)))
            {
                _services[typeof(TService)] = EngineContext.Current.Resolve<TService>();
            }
            var ser = (TService)_services[typeof(TService)];
            if (ser == null)
            {
                throw new LException("IOC不存在" + typeof(TService).FullName + "此类型！请将此类型加入到IOC中！");
            }
            return ser;
        }
        public static IRepository<TAggregateRoot> Repository<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            var repo = Service<IRepository<TAggregateRoot>>();
            return repo;
        }
        public static IWebHelper WebHelper
        {
            get
            {
                return Service<IWebHelper>();
            }
        }
        public static LConfig Config
        {
            get
            {
                return Service<LConfig>();
            }
        }
        public static ICacheManager Cache
        {
            get
            {
                return Service<ICacheManager>();
            }
        }
        public static ISettingService Setting
        {
            get
            {
                return Service<ISettingService>();
            }
        }
        public static ILocalizationService Localization
        {
            get
            {
                return Service<ILocalizationService>();
            }
        }
        public static IPluginFinder PluginFinder
        {
            get
            {
                return Service<IPluginFinder>();
            }
        }
        public static ITypeFinder TypeFinder
        {
            get
            {
                return Service<ITypeFinder>();
            }
        }
        public static LCL.Serialization.IObjectSerializer Serializer
        {
            get
            {
                return new LCL.Serialization.ObjectBinarySerializer();
            }
        }
        public static LIDbAccesser DbUtility(string connectionString, DbProviderType providerType)
        {
            return new LIDbAccesser(connectionString, providerType);
        }
    }
}
