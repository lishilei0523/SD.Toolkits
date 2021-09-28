using SD.Toolkits.CoreWCF.Client.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// 绑定节点
    /// </summary>
    public sealed class BindingsSection : ConfigurationSection
    {
        [ConfigurationProperty(ConfigurationStrings.BasicHttpBindingCollectionElementName, Options = ConfigurationPropertyOptions.None)]
        public BasicHttpBindingCollectionElement BasicHttpBinding
        {
            get { return (BasicHttpBindingCollectionElement)base[ConfigurationStrings.BasicHttpBindingCollectionElementName]; }
        }

        [ConfigurationProperty(ConfigurationStrings.NetTcpBindingCollectionElementName, Options = ConfigurationPropertyOptions.None)]
        public NetTcpBindingCollectionElement NetTcpBinding
        {
            get { return (NetTcpBindingCollectionElement)base[ConfigurationStrings.NetTcpBindingCollectionElementName]; }
        }

        [ConfigurationProperty(ConfigurationStrings.NetHttpBindingCollectionElementName, Options = ConfigurationPropertyOptions.None)]
        public NetHttpBindingCollectionElement NetHttpBinding
        {
            get { return (NetHttpBindingCollectionElement)base[ConfigurationStrings.NetHttpBindingCollectionElementName]; }
        }

        [ConfigurationProperty(ConfigurationStrings.WSHttpBindingCollectionElementName, Options = ConfigurationPropertyOptions.None)]
        public WSHttpBindingCollectionElement WSHttpBinding
        {
            get { return (WSHttpBindingCollectionElement)base[ConfigurationStrings.WSHttpBindingCollectionElementName]; }
        }
    }
}
