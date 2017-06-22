using Autofac;
using Autofac.Core;
using LCL;
using LCL.Domain.Events;
using LCL.Domain.Repositories;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;
using LCL.LData;
using LCL.Repositories.EntityFramework;
using LCL.Tests.ACore.Domain.Services;
using LCL.Tests.Domain.Events;
using LCL.Tests.Domain.Events.Handlers;
using LCL.Tests.Domain.Repositories;
using LCL.Tests.Domain.Repositories.EntityFramework;
using System.Data.Entity;

namespace LCL.Tests.ACore
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // EntityFramework  Repository Context & Repositories
            var dbset = DbSetting.FindOrCreate("LCL");
            var efDataProviderManager = new EfDataProviderManager(dbset);
            var dataProvider = efDataProviderManager.LoadDataProvider();
            dataProvider.InitConnectionFactory();
            builder.Register(c => dbset).As<DbSetting>();
            builder.Register<BaseDbContext>(c => new EFTestContext(dbset.ConnectionString)).As(typeof(IRepositoryContext)).Named<IRepositoryContext>(dbset.Name).InstancePerLifetimeScope();
            builder.Register<IRepositoryContext>(c => new EFTestContext(dbset.ConnectionString)).Named<IRepositoryContext>(dbset.Name).InstancePerLifetimeScope();

            builder.RegisterType<CategorizationRepository>().As<ICategorizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SalesOrderRepository>().As<ISalesOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartItemRepository>().As<IShoppingCartItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartRepository>().As<IShoppingCartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // Event Handlers
            builder.RegisterType<SendEmailHandler>().As<IEventHandler<OrderDispatchedEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<SendEmailHandler>().As<IEventHandler<OrderConfirmedEvent>>().InstancePerLifetimeScope();

            // Domain Event Handlers
            builder.RegisterType<GetUserOrdersEvent>().As<IDomainEventHandler<GetUserOrdersEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDispatchedEventHandler>().As<IDomainEventHandler<OrderDispatchedEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<OrderConfirmedEventHandler>().As<IDomainEventHandler<OrderConfirmedEvent>>().InstancePerLifetimeScope();

            // Event Aggregator

            // Server
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();


            System.Diagnostics.Debug.WriteLine(Order + " init plugin LCL.Plugin.EasyUI.UCenter");
        }

        public int Order
        {
            get { return -2; }
        }
    }
}
