using SD.Toolkits.CoreWCF.Client.Configurations.Commons;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Behaviors
{
    /// <summary>
    /// �ս����Ϊ���ýڵ㼯��
    /// </summary>
    [ConfigurationCollection(typeof(BehaviorConfigurationElement), AddItemName = ConfigurationStrings.Behavior)]
    public sealed class BehaviorConfigurationElementCollection : ServiceModelEnhancedConfigurationElementCollection<BehaviorConfigurationElement>
    {
        public BehaviorConfigurationElementCollection()
            : base(ConfigurationStrings.Behavior)
        {

        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
            }

            BehaviorConfigurationElement configElementKey = (BehaviorConfigurationElement)element;
            return configElementKey.Name;
        }
    }
}
