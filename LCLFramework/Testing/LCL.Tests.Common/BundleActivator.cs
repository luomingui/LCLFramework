using LCL;
using LCL.ComponentModel;
using LCL.Events;
using LCL.Events.Bus;
using LCL.MetaModel;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Repositories.EntityFrameworkRepository;
using System.Data.Entity;
using System.Diagnostics;

namespace LCL.Tests.Common
{
    public class BundleActivator : LCLPlugin
    {
        public override void Initialize(IApp app)
        {
            DatabaseInitializer.Initialize();
            // Repository Context & Repositories Entity Framework

            ServiceLocator.Instance.Register<DbContext, EFTestDbContext>();

            //ServiceLocator.Instance.Register<IRepository<Org>, EntityFrameworkRepository<Org>>();
            //ServiceLocator.Instance.Register<IRepository<OrgPositionOperationDeny>, EntityFrameworkRepository<OrgPositionOperationDeny>>();
            //ServiceLocator.Instance.Register<IRepository<Position>, EntityFrameworkRepository<Position>>();
            //ServiceLocator.Instance.Register<IRepository<User>, EntityFrameworkRepository<User>>();

            ServiceLocator.Instance.Register<IUserRepository, UserRepository>();

            // Domain Event Handlers
            ServiceLocator.Instance.Register<IDomainEventHandler<UserChangeEmailDomainEvent>, UserChangeEmailDomainEventHandler>();
            // Event Handlers
            ServiceLocator.Instance.Register<IEventHandler<UserChangeEmailDomainEvent>, SendEmailHandler>();
            // Event Aggregator
            
        }

    }
}
