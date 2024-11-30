using SD.Toolkits.CoreWCF.Mobile.Configurations.Commons;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Behaviors
{
    /// <summary>
    /// �ս����Ϊ���ýڵ�
    /// </summary>
    public sealed class BehaviorConfigurationElement : ConfigurationElement
    {
        #region # ���� ���� string Name
        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }
        #endregion

        #region # �ս����Ϊ�ڵ��б� ���� EndpointBehaviorElementCollection EndpointBehaviors
        /// <summary>
        /// �ս����Ϊ�ڵ��б�
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.DefaultCollectionName, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public EndpointBehaviorElementCollection EndpointBehaviors
        {
            get => (EndpointBehaviorElementCollection)base[ConfigurationStrings.DefaultCollectionName];
        }
        #endregion
    }
}
