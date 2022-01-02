using SD.Toolkits.CoreWCF.Xamarin.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
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
    }
}
