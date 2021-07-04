using System.Configuration;

namespace System.ServiceModel.NetCore.Configurations
{
    /// <summary>
    /// 终节点行为配置节点
    /// </summary>
    public class BehaviorConfigurationElement : ConfigurationElement
    {
        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        #endregion

        #region # 终节点行为列表 —— EndpointBehaviorElementCollection EndpointBehaviorElements
        /// <summary>
        /// 终节点行为列表
        /// </summary>
        [ConfigurationProperty("endpointBehaviors")]
        [ConfigurationCollection(typeof(EndpointBehaviorElementCollection), AddItemName = "endpointBehavior")]
        public EndpointBehaviorElementCollection EndpointBehaviorElements
        {
            get
            {
                EndpointBehaviorElementCollection collection = this["endpointBehaviors"] as EndpointBehaviorElementCollection;
                return collection ?? new EndpointBehaviorElementCollection();
            }
            set
            {
                this["endpointBehaviors"] = value;
            }
        }
        #endregion
    }
}
