using LCL;
using LCL.Bus;
using LCL.Caching;
using LCL.Config;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.ObjectMapping;
using LCL.Plugins;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LCL.Application
{
    /// <summary>
    /// 表示应用层服务的抽象类。
    /// </summary>
    public abstract class ApplicationService : DisposableObject
    {
        #region Private Fields
        private readonly IRepositoryContext context;
        private readonly IEventBus bus;
        public IObjectMapper ObjectMapper { get; set; }
        #endregion

        #region Ctor
        public ApplicationService(IRepositoryContext context, IEventBus bus)
        {
            this.context = context;
            this.bus = bus;
            ObjectMapper = NullObjectMapper.Instance;
        }
        #endregion

        #region Protected Properties
        protected IEventBus EventBus
        {
            get { return this.bus; }
        }
        protected IRepositoryContext Context
        {
            get { return this.context; }
        }
        public IWebHelper WebHelper
        {
            get
            {
                return RF.Service<IWebHelper>();
            }
        }
        public LConfig Config
        {
            get
            {
                return RF.Service<LConfig>();
            }
        }
        public ISettingService Setting
        {
            get
            {
                return RF.Service<ISettingService>();
            }
        }
        public ILocalizationService Localization
        {
            get
            {
                return RF.Service<ILocalizationService>();
            }
        }
        public IPluginFinder PluginFinder
        {
            get
            {
                return RF.Service<IPluginFinder>();
            }
        }
        public ITypeFinder TypeFinder
        {
            get
            {
                return RF.Service<ITypeFinder>();
            }
        }
        #endregion

        #region Protected Methods
        public static IRepository<TAggregateRoot> Repository<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            var repo = RF.Service<IRepository<TAggregateRoot>>();
            return repo;
        }
        protected virtual string L(string name)
        {
            return Localization.GetResource(name);
        }
        protected string L(string name, params object[] args)
        {
            return Localization.GetString(name, args);
        }

        protected virtual string L(string name, CultureInfo culture)
        {
            return Localization.GetString(name, culture);
        }
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return Localization.GetString(name, culture, args);
        }
        protected bool IsEmptyGuidString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return true;
            Guid guid = new Guid(s);
            return guid == Guid.Empty;
        }
        protected override void Dispose(bool disposing)
        {

        }
        #endregion
      
    }
}
