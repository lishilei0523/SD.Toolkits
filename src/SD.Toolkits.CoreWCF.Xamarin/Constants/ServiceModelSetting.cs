using SD.Toolkits.CoreWCF.Xamarin.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Xamarin.Configurations.Clients;
using System.Collections.Concurrent;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// CoreWCF客户端设置
    /// </summary>
    public static class ServiceModelSetting
    {
        #region # 字段及构造器

        /// <summary>
        /// 终结点配置字典
        /// </summary>
        private static readonly IDictionary<string, ChannelEndpointElement> _Endpoints;

        /// <summary>
        /// BasicHttpBinding配置字典
        /// </summary>
        private static readonly IDictionary<string, BasicHttpBindingElement> _BasicHttpBindings;

        /// <summary>
        /// 终结点行为配置字典
        /// </summary>
        private static readonly IDictionary<string, BehaviorConfigurationElement> _BehaviorConfigurations;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ServiceModelSetting()
        {
            _Endpoints = new ConcurrentDictionary<string, ChannelEndpointElement>();
            _BasicHttpBindings = new ConcurrentDictionary<string, BasicHttpBindingElement>();
            _BehaviorConfigurations = new ConcurrentDictionary<string, BehaviorConfigurationElement>();
            foreach (ChannelEndpointElement endpoint in ServiceModelSectionGroup.Setting.Clients.Endpoints)
            {
                _Endpoints.Add(endpoint.Name, endpoint);
            }
            foreach (BasicHttpBindingElement basicHttpBinding in ServiceModelSectionGroup.Setting.Bindings.BasicHttpBinding.Bindings)
            {
                _BasicHttpBindings.Add(basicHttpBinding.Name, basicHttpBinding);
            }
            foreach (BehaviorConfigurationElement behaviorConfiguration in ServiceModelSectionGroup.Setting.Behaviors.BehaviorConfigurations)
            {
                _BehaviorConfigurations.Add(behaviorConfiguration.Name, behaviorConfiguration);
            }
        }

        #endregion

        #region # 终结点配置字典 —— static IDictionary<string, ChannelEndpointElement> Endpoints
        /// <summary>
        /// 终结点配置字典
        /// </summary>
        public static IDictionary<string, ChannelEndpointElement> Endpoints
        {
            get { return _Endpoints; }
        }
        #endregion

        #region # BasicHttpBinding配置字典 —— static IDictionary<string, BasicHttpBindingElement> BasicHttpBindings
        /// <summary>
        /// BasicHttpBinding配置字典
        /// </summary>
        public static IDictionary<string, BasicHttpBindingElement> BasicHttpBindings
        {
            get { return _BasicHttpBindings; }
        }
        #endregion

        #region # 终结点行为配置字典 —— static IDictionary<string, BehaviorConfigurationElement> BehaviorConfigurations
        /// <summary>
        /// 终结点行为配置字典
        /// </summary>
        public static IDictionary<string, BehaviorConfigurationElement> BehaviorConfigurations
        {
            get { return _BehaviorConfigurations; }
        }
        #endregion
    }
}
