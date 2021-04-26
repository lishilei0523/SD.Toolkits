using System.Configuration;
using System.ServiceModel.NetCore.Configurations;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// WCF配置
    /// </summary>
    public class ServiceModelSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly ServiceModelSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ServiceModelSection()
        {
            _Setting = (ServiceModelSection)ConfigurationManager.GetSection("system.serviceModel");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("WCF节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static RedisConfiguration Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static ServiceModelSection Setting
        {
            get { return _Setting; }
        }
        #endregion

        #region # 终节点配置列表 —— EndpointElementCollection Endpoints
        /// <summary>
        /// 终节点配置列表
        /// </summary>
        [ConfigurationProperty("client")]
        [ConfigurationCollection(typeof(EndpointElementCollection), AddItemName = "endpoint")]
        public EndpointElementCollection Endpoints
        {
            get
            {
                EndpointElementCollection collection = this["client"] as EndpointElementCollection;
                return collection ?? new EndpointElementCollection();
            }
            set
            {
                this["client"] = value;
            }
        }
        #endregion

        #region # 终节点行为配置列表 —— BehaviorConfigurationCollection BehaviorConfigurations
        /// <summary>
        /// 终节点行为配置列表
        /// </summary>
        [ConfigurationProperty("behaviorConfigurations")]
        [ConfigurationCollection(typeof(BehaviorConfigurationElementCollection), AddItemName = "behaviorConfiguration")]
        public BehaviorConfigurationElementCollection BehaviorConfigurations
        {
            get
            {
                BehaviorConfigurationElementCollection collection = this["behaviorConfigurations"] as BehaviorConfigurationElementCollection;
                return collection ?? new BehaviorConfigurationElementCollection();
            }
            set
            {
                this["behaviorConfigurations"] = value;
            }
        }
        #endregion
    }
}
