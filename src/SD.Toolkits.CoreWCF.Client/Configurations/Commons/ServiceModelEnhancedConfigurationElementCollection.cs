using System.Configuration;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Commons
{
    public abstract class ServiceModelEnhancedConfigurationElementCollection<TConfigurationElement> : ServiceModelConfigurationElementCollection<TConfigurationElement>
        where TConfigurationElement : ConfigurationElement, new()
    {
        internal ServiceModelEnhancedConfigurationElementCollection(string elementName)
            : base(ConfigurationElementCollectionType.AddRemoveClearMap, elementName)
        {
            this.AddElementName = elementName;
        }
    }
}
