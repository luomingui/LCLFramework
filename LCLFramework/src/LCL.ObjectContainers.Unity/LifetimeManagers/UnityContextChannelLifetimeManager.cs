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

using System.ServiceModel;

namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// Unity lifetime manager to support <see cref="System.ServiceModel.IContextChannel"/>.
    /// </summary>
    public class UnityContextChannelLifetimeManager : UnityWcfLifetimeManager<IContextChannel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContextChannelLifetimeManager"/> class.
        /// </summary>
        public UnityContextChannelLifetimeManager()
            : base()
        {
        }

        /// <summary>
        /// Returns the appropriate extension for the current lifetime manager.
        /// </summary>
        /// <returns>The registered extension for the current lifetime manager, otherwise, null if the extension is not registered.</returns>
        protected override UnityWcfExtension<IContextChannel> FindExtension()
        {
            return UnityContextChannelExtension.Current;
        }
    }
}
