
/* Announcement: The Unity WCF Lifetime Manager is implemented by using the source code provided by Andrew Oakley on
 * his blog post: http://blogs.msdn.com/b/atoakley/archive/2010/12/29/unity-lifetime-managers-and-wcf.aspx.
 * When using this lifetime manager, a unity extension should be installed by adding the behavior extension. For example:
 * 
 * <system.serviceModel>
    <extensions>
      <behaviorExtensions>
        <add name="unity" type="Apworks.ObjectContainers.Unity.LifetimeManagers.UnityBehaviorExtensionElement, Apworks.ObjectContainers.Unity" />
      </behaviorExtensions>
    </extensions>
  </system.serviceModel>
 * 
 * Then, specify this behavior on the service behavior:
 * 
 * <system.serviceModel>
    <extensions>
      <behaviorExtensions>
        <add name="unity" type="Apworks.ObjectContainers.Unity.LifetimeManagers.UnityBehaviorExtensionElement, Apworks.ObjectContainers.Unity" />
      </behaviorExtensions>
    </extensions>
    <behaviors>
      <serviceBehaviors>
        <behavior>
          <serviceMetadata httpGetEnabled="true" />
          <serviceDebug includeExceptionDetailInFaults="false" />
          <!-- The line below specifies the behavior settings -->
          <unity operationContextEnabled="true" instanceContextEnabled="true" contextChannelEnabled="true" serviceHostBaseEnabled="true" /> 
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>
 * 
 * */

using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// Declares methods that provide a service object or recycle a service object for a Windows Communication Foundation (WCF) service.
    /// </summary>
    public class UnityInstanceProvider : IInstanceProvider
    {
        /// <summary>
        /// Configuration section name for Unity.
        /// </summary>
        private const string UnityConfigurationSectionName = "unity";

        /// <summary>
        /// Unity container.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <see cref="System.Type"/> to create.
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Unity container name.
        /// </summary>
        private readonly string containerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityInstanceProvider"/> class with the <see cref="Type"/>
        /// to create and the name of the container to use.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to create.</param>
        /// <param name="containerName">The name of the container to use.</param>
        /// <remarks>
        /// If <paramref name="containerName"/> is null then the default configuration is used. If the Unity configuration section
        /// is not found, the container will just try and resolve the type.
        /// </remarks>
        public UnityInstanceProvider(Type type, string containerName)
        {
            this.type = type;
            this.containerName = containerName;
            this.container = UnityInstanceProvider.CreateUnityContainer(this.containerName);
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="InstanceContext" /> object.</param>
        /// <returns>
        /// A user-defined service object.
        /// </returns>
        /// <remarks>
        /// Uses the configured Unity container to resolve the service object.
        /// </remarks>
        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
        /// <param name="message">The message that triggered the creation of a service object.</param>
        /// <returns>
        /// The service object.
        /// </returns>
        /// <remarks>
        /// Uses the configured Unity container to resolve the service object.
        /// </remarks>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.container.Resolve(this.type);
        }

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.InstanceContext" /> object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">The service's instance context.</param>
        /// <param name="instance">The service object to be recycled.</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            // Since we created the service instance, we need to dispose of it, if needed.
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="Microsoft.Practices.Unity.UnityContainer"/> from configuration using the provided container name.
        /// </summary>
        /// <param name="containerName">Container name to configure.  If containerName is null or empty, the default container is configured.</param>
        /// <returns>Created and configured <see cref="Microsoft.Practices.Unity.UnityContainer"/>.</returns>
        private static IUnityContainer CreateUnityContainer(string containerName)
        {
            IUnityContainer unityContainer = new UnityContainer();
            try
            {
                UnityConfigurationSection section = ConfigurationManager.GetSection(
                    UnityInstanceProvider.UnityConfigurationSectionName) as UnityConfigurationSection;
                if (section != null)
                {
                    if (string.IsNullOrEmpty(containerName))
                    {
                        section.Configure(unityContainer);
                    }
                    else
                    {
                        section.Configure(unityContainer, containerName);
                    }
                }
            }
            catch (Exception)
            {
                unityContainer.Dispose();
                throw;
            }

            return unityContainer;
        }
    }
}
