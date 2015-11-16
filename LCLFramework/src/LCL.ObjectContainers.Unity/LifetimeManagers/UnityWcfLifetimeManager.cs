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

using Microsoft.Practices.Unity;
using System;
using System.Globalization;
using System.ServiceModel;


namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// Abstract base class for Unity WCF lifetime support.
    /// </summary>
    /// <typeparam name="T">IExtensibleObject for which to register Unity lifetime manager support.</typeparam>
    public abstract class UnityWcfLifetimeManager<T> : LifetimeManager
        where T : IExtensibleObject<T>
    {
        /// <summary>
        /// Key for Unity to use for the associated object's instance.
        /// </summary>
        private Guid key = Guid.NewGuid();

        /// <summary>
        /// Initializes a new instance of the UnityWcfLifetimeManager class.
        /// </summary>
        protected UnityWcfLifetimeManager()
            : base()
        {
        }

        /// <summary>
        /// Gets the currently registered UnityWcfExtension for this lifetime manager.
        /// </summary>
        private UnityWcfExtension<T> Extension
        {
            get
            {
                UnityWcfExtension<T> extension = this.FindExtension();
                if (extension == null)
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.CurrentCulture, "UnityWcfExtension<{0}> must be registered in WCF.", typeof(T).Name));
                }

                return extension;
            }
        }

        /// <summary>
        /// Gets the object instance for the given key from the currently registered extension.
        /// </summary>
        /// <returns>The object instance associated with the given key.  If no instance is registered, null is returned.</returns>
        public override object GetValue()
        {
            return this.Extension.FindInstance(this.key);
        }

        /// <summary>
        /// Removes the object instance for the given key from the currently registered extension.
        /// </summary>
        public override void RemoveValue()
        {
            // Not called, but just in case.
            this.Extension.RemoveInstance(this.key);
        }

        /// <summary>
        /// Associates the supplied object instance with the given key in the currently registered extension.
        /// </summary>
        /// <param name="newValue">The object to register in the currently registered extension.</param>
        public override void SetValue(object newValue)
        {
            this.Extension.RegisterInstance(this.key, newValue);
        }

        /// <summary>
        /// Finds the currently registered UnityWcfExtension for this lifetime manager.
        /// </summary>
        /// <returns>Currently registered UnityWcfExtension of the given type, or null if no UnityWcfExtension is registered.</returns>
        protected abstract UnityWcfExtension<T> FindExtension();
    }
}
