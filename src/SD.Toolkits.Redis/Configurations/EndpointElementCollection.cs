using System.Configuration;

namespace SD.Toolkits.Redis.Configurations
{
    /// <summary>
    /// 节点集合
    /// </summary>
    public class EndpointElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新配置节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointElement();
        }

        /// <summary>
        /// 获取节点键
        /// </summary>
        /// <param name="element">节点</param>
        /// <returns>节点键</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndpointElement)element).Host;
        }
    }
}
