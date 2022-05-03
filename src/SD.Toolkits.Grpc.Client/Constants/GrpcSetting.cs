using SD.Toolkits.Grpc.Client.Configurations;
using System.Collections.Concurrent;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Grpc
{
    /// <summary>
    /// gRPC客户端设置
    /// </summary>
    public static class GrpcSetting
    {
        #region # 字段及构造器

        /// <summary>
        /// 终结点字典
        /// </summary>
        private static readonly IDictionary<string, EndpointElement> _Endpoints;

        /// <summary>
        /// 终结点配置字典
        /// </summary>
        private static readonly IDictionary<string, EndpointConfigurationElement> _EndpointConfigurations;

        /// <summary>
        /// 授权拦截器配置字典
        /// </summary>
        private static readonly IDictionary<string, AuthInterceptorElement> _AuthInterceptors;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcSetting()
        {
            _Endpoints = new ConcurrentDictionary<string, EndpointElement>();
            _EndpointConfigurations = new ConcurrentDictionary<string, EndpointConfigurationElement>();
            _AuthInterceptors = new ConcurrentDictionary<string, AuthInterceptorElement>();
            foreach (EndpointElement endpoint in GrpcSection.Setting.EndpointElements)
            {
                _Endpoints.Add(endpoint.Contract, endpoint);
            }
            foreach (EndpointConfigurationElement endpointConfiguration in GrpcSection.Setting.EndpointConfigurationElements)
            {
                _EndpointConfigurations.Add(endpointConfiguration.Name, endpointConfiguration);
            }
            foreach (AuthInterceptorElement authInterceptor in GrpcSection.Setting.AuthInterceptorElements)
            {
                _AuthInterceptors.Add(authInterceptor.Name, authInterceptor);
            }
        }

        #endregion

        #region # 终结点字典 —— static IDictionary<string, EndpointElement> Endpoints
        /// <summary>
        /// 终结点字典
        /// </summary>
        public static IDictionary<string, EndpointElement> Endpoints
        {
            get { return _Endpoints; }
        }
        #endregion

        #region # 终结点配置字典 —— static IDictionary<string, EndpointConfigurationElement> EndpointConfigurations
        /// <summary>
        /// 终结点配置字典
        /// </summary>
        public static IDictionary<string, EndpointConfigurationElement> EndpointConfigurations
        {
            get { return _EndpointConfigurations; }
        }
        #endregion

        #region # 授权拦截器配置字典 —— static IDictionary<string, AuthInterceptorElement> AuthInterceptors
        /// <summary>
        /// 授权拦截器配置字典
        /// </summary>
        public static IDictionary<string, AuthInterceptorElement> AuthInterceptors
        {
            get { return _AuthInterceptors; }
        }
        #endregion
    }
}
