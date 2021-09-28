using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Behaviors
{
    /// <summary>
    /// 终节点行为节点集合
    /// </summary>
    [ConfigurationCollection(typeof(EndpointBehaviorElement), AddItemName = ConfigurationStrings.EndpointBehavior)]
    public class EndpointBehaviorElementCollection : ServiceModelEnhancedConfigurationElementCollection<EndpointBehaviorElement>
    {
        public EndpointBehaviorElementCollection()
            : base(ConfigurationStrings.Behavior)
        {

        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }

            EndpointBehaviorElement configElementKey = (EndpointBehaviorElement)element;
            return configElementKey;
        }
    }
}
