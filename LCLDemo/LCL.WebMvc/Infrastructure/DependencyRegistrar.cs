using Autofac;
using LCL.Infrastructure.DependencyManagement;
using LCL.Infrastructure;
using LCL.Domain.Repositories.EntityFramework;
using LCL.Domain.Repositories;
using LCL.Domain.Events;
using LCL.Domain.Events.Handlers;
using LCL.LData;
using LCL.Infrastructure.Config;
using LCL.Domain.Services;
using LCL.Application.Implementation;
using System.Data.Entity;

namespace LCL.WebMvc.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            RepositoryImp(builder, typeFinder);

            ServerImp(builder, typeFinder);

            System.Diagnostics.Debug.WriteLine(Order + " init LCL.WebMvc");

        }
        private void RepositoryImp(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //DbContext注入
            builder.Register(c => AppConfig.DbSetting).As<DbSetting>();
            builder.Register<IRepositoryContext>(c => new EFTestContext(AppConfig.DbSetting.ConnectionString)).InstancePerLifetimeScope();
            //仓储模式注入
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
            builder.RegisterType<GetUserOrdersEventHandler>().As<IDomainEventHandler<GetUserOrdersEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDispatchedEventHandler>().As<IDomainEventHandler<OrderDispatchedEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<OrderConfirmedEventHandler>().As<IDomainEventHandler<OrderConfirmedEvent>>().InstancePerLifetimeScope();

        }

        private void ServerImp(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<LDomainService>().As<ILDomainService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderServiceImpl>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductServiceImpl>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<UserServiceImpl>().As<IUserService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 5; }
        }
    }
}
