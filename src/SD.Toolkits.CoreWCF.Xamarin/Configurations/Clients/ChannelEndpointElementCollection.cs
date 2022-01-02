using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System.Configuration;
using System.Globalization;

namespace SD.Toolkits.CoreWCF.Xamarin.Configurations.Clients
{
    /// <summary>
    /// 信道终结点节点集合
    /// </summary>
    [ConfigurationCollection(typeof(ChannelEndpointElement), AddItemName = ConfigurationStrings.Endpoint)]
    public sealed class ChannelEndpointElementCollection : ServiceModelEnhancedConfigurationElementCollection<ChannelEndpointElement>
    {
        public ChannelEndpointElementCollection()
            : base(ConfigurationStrings.Endpoint)
        {

        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }

            ChannelEndpointElement configElementKey = (ChannelEndpointElement)element;

            return string.Format(CultureInfo.InvariantCulture, "contractType:{0};name:{1}", configElementKey.Contract, configElementKey.Name);
        }
    }
}
