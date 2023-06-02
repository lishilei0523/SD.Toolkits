using SD.Toolkits.CoreWCF.Client.Configurations.Clients;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// 客户端节点
    /// </summary>
    public sealed class ClientsSection : ConfigurationSection
    {
        [ConfigurationProperty(ConfigurationStrings.DefaultCollectionName, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public ChannelEndpointElementCollection Endpoints
        {
            get { return (ChannelEndpointElementCollection)this[ConfigurationStrings.DefaultCollectionName]; }
        }
    }
}
