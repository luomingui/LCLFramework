using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Happy.Infrastructure.Ioc;
using Happy.Infrastructure.ExtentionMethods.Reflection;
using Happy.Bootstrap.RegistyByConvention;

namespace Happy.Command.Bootstrap.Internal
{
    internal class CommandRegister : IConventionRegister
    {
        public void Register(IServiceRegistry registry, Type serviceType)
        {
            if (serviceType.HasGenericInterfaceTypeDefinition(typeof(ICommandHandler<>)))
            {
                var baseTypes = serviceType.GetInterfaces().Where(x => x.Name == typeof(ICommandHandler<>).Name);

                foreach (var baseType in baseTypes)
                {
                    registry.RegisterType(baseType, serviceType, ServiceLifecycle.Transient);
                }
            }
        }
    }
}
