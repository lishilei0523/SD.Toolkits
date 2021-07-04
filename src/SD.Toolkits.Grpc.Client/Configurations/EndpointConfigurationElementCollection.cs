using System.Configuration;

namespace SD.Toolkits.Grpc.Client.Configurations
{
    /// <summary>
    /// 终结点配置节点集合
    /// </summary>
    public class EndpointConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新配置节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointConfigurationElement();
        }

        /// <summary>
        /// 获取节点键
        /// </summary>
        /// <param name="element">节点</param>
        /// <returns>节点键</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndpointConfigurationElement)element).Name;
        }
    }
}
