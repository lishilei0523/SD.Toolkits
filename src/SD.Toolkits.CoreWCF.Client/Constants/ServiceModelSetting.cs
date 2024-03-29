﻿using SD.Toolkits.CoreWCF.Client.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Client.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Client.Configurations.Clients;
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
        /// NetTcpBinding配置字典
        /// </summary>
        private static readonly IDictionary<string, NetTcpBindingElement> _NetTcpBindings;

        /// <summary>
        /// NetHttpBinding配置字典
        /// </summary>
        private static readonly IDictionary<string, NetHttpBindingElement> _NetHttpBindings;

        /// <summary>
        /// WSHttpBinding配置字典
        /// </summary>
        private static readonly IDictionary<string, WSHttpBindingElement> _WsHttpBindings;

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
            _NetTcpBindings = new ConcurrentDictionary<string, NetTcpBindingElement>();
            _NetHttpBindings = new ConcurrentDictionary<string, NetHttpBindingElement>();
            _WsHttpBindings = new ConcurrentDictionary<string, WSHttpBindingElement>();
            _BehaviorConfigurations = new ConcurrentDictionary<string, BehaviorConfigurationElement>();
            foreach (ChannelEndpointElement endpoint in ServiceModelSectionGroup.Setting.Clients.Endpoints)
            {
                _Endpoints.Add(endpoint.Name, endpoint);
            }
            foreach (BasicHttpBindingElement basicHttpBinding in ServiceModelSectionGroup.Setting.Bindings.BasicHttpBinding.Bindings)
            {
                _BasicHttpBindings.Add(basicHttpBinding.Name, basicHttpBinding);
            }
            foreach (NetTcpBindingElement netTcpBinding in ServiceModelSectionGroup.Setting.Bindings.NetTcpBinding.Bindings)
            {
                _NetTcpBindings.Add(netTcpBinding.Name, netTcpBinding);
            }
            foreach (NetHttpBindingElement netHttpBinding in ServiceModelSectionGroup.Setting.Bindings.NetHttpBinding.Bindings)
            {
                _NetHttpBindings.Add(netHttpBinding.Name, netHttpBinding);
            }
            foreach (WSHttpBindingElement wsHttpBinding in ServiceModelSectionGroup.Setting.Bindings.WSHttpBinding.Bindings)
            {
                _WsHttpBindings.Add(wsHttpBinding.Name, wsHttpBinding);
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

        #region # NetTcpBinding配置字典 —— static IDictionary<string, NetTcpBindingElement> NetTcpBindings
        /// <summary>
        /// NetTcpBinding配置字典
        /// </summary>
        public static IDictionary<string, NetTcpBindingElement> NetTcpBindings
        {
            get { return _NetTcpBindings; }
        }
        #endregion

        #region # NetHttpBinding配置字典 —— static IDictionary<string, NetHttpBindingElement> NetHttpBindings
        /// <summary>
        /// NetHttpBinding配置字典
        /// </summary>
        public static IDictionary<string, NetHttpBindingElement> NetHttpBindings
        {
            get { return _NetHttpBindings; }
        }
        #endregion

        #region # WSHttpBinding配置字典 —— static IDictionary<string, WSHttpBindingElement> WSHttpBindings
        /// <summary>
        /// WSHttpBinding配置字典
        /// </summary>
        public static IDictionary<string, WSHttpBindingElement> WSHttpBindings
        {
            get { return _WsHttpBindings; }
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
