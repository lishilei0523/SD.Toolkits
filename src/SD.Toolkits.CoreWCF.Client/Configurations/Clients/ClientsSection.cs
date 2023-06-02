using SD.Toolkits.CoreWCF.Client.Configurations.Clients;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// �ͻ��˽ڵ�
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
