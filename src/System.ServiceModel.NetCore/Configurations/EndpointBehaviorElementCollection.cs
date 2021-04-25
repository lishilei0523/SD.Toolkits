using System.Configuration;

namespace System.ServiceModel.NetCore.Configurations
{
    /// <summary>
    /// 终节点行为元素集合
    /// </summary>
    public class EndpointBehaviorElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新配置节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointBehaviorElement();
        }

        /// <summary>
        /// 获取节点键
        /// </summary>
        /// <param name="element">节点</param>
        /// <returns>节点键</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndpointBehaviorElement)element);
        }
    }
}
