using LCL;
using LCL.Bus;
using LCL.Caching;
using LCL.Config;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Plugins;
using System;
using System.Collections.Generic;
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
        #endregion

        #region Ctor
        public ApplicationService(IRepositoryContext context, IEventBus bus)
        {
            this.context = context;
            this.bus = bus;
        }
        #endregion

        #region Protected Properties
        public static IRepository<TAggregateRoot> Repository<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            var repo = RF.Service<IRepository<TAggregateRoot>>();
            return repo;
        }
        protected IEventBus EventBus
        {
            get { return this.bus; }
        }
        protected IRepositoryContext Context
        {
            get { return this.context; }
        }
        public static IWebHelper WebHelper
        {
            get
            {
                return RF.Service<IWebHelper>();
            }
        }
        public static LConfig Config
        {
            get
            {
                return RF.Service<LConfig>();
            }
        }
        public static ISettingService Setting
        {
            get
            {
                return RF.Service<ISettingService>();
            }
        }
        public static ILocalizationService Localization
        {
            get
            {
                return RF.Service<ILocalizationService>();
            }
        }
        public static IPluginFinder PluginFinder
        {
            get
            {
                return RF.Service<IPluginFinder>();
            }
        }
        public static ITypeFinder TypeFinder
        {
            get
            {
                return RF.Service<ITypeFinder>();
            }
        }
        #endregion

        #region Protected Methods
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
