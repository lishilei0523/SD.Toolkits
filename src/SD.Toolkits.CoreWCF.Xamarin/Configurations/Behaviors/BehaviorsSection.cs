using SD.Toolkits.CoreWCF.Xamarin.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// 终结点行为节点
    /// </summary>
    public class BehaviorsSection : ConfigurationSection
    {
        /// <summary>
        /// 终结点行为配置节点列表
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.DefaultCollectionName, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public BehaviorConfigurationElementCollection BehaviorConfigurations
        {
            get => (BehaviorConfigurationElementCollection)base[ConfigurationStrings.DefaultCollectionName];
        }
    }
}
