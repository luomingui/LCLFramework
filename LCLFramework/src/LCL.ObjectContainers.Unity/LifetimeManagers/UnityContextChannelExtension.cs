
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
    /// Implements the lifetime manager storage for the <see cref="System.ServiceModel.IContextChannel"/> extension.
    /// </summary>
    public class UnityContextChannelExtension : UnityWcfExtension<IContextChannel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContextChannelExtension"/> class.
        /// </summary>
        public UnityContextChannelExtension()
            : base()
        {
        }

        /// <summary>
        /// Gets the <see cref="UnityContextChannelExtension"/> for the current channel.
        /// </summary>
        public static UnityContextChannelExtension Current
        {
            get { return OperationContext.Current.Channel.Extensions.Find<UnityContextChannelExtension>(); }
        }
    }
}
