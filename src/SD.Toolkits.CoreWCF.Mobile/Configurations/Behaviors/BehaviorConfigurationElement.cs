using SD.Toolkits.CoreWCF.Mobile.Configurations.Commons;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Behaviors
{
    /// <summary>
    /// 终结点行为配置节点
    /// </summary>
    public sealed class BehaviorConfigurationElement : ConfigurationElement
    {
        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }
        #endregion

        #region # 终结点行为节点列表 —— EndpointBehaviorElementCollection EndpointBehaviors
        /// <summary>
        /// 终结点行为节点列表
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.DefaultCollectionName, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public EndpointBehaviorElementCollection EndpointBehaviors
        {
            get => (EndpointBehaviorElementCollection)base[ConfigurationStrings.DefaultCollectionName];
        }
        #endregion
    }
}
