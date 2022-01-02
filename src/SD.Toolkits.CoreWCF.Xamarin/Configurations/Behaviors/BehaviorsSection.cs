using SD.Toolkits.CoreWCF.Xamarin.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// �ս����Ϊ�ڵ�
    /// </summary>
    public class BehaviorsSection : ConfigurationSection
    {
        /// <summary>
        /// �ս����Ϊ���ýڵ��б�
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.DefaultCollectionName, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public BehaviorConfigurationElementCollection BehaviorConfigurations
        {
            get => (BehaviorConfigurationElementCollection)base[ConfigurationStrings.DefaultCollectionName];
        }
    }
}
