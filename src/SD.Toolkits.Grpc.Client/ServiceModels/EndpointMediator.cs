using SD.Toolkits.Grpc.Client.Configurations;
using System.Collections.Concurrent;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Grpc
{
    /// <summary>
    /// 终结点配置中介者
    /// </summary>
    public static class EndpointMediator
    {
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
        static EndpointMediator()
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

        /// <summary>
        /// 终结点字典
        /// </summary>
        public static IDictionary<string, EndpointElement> Endpoints
        {
            get { return _Endpoints; }
        }

        /// <summary>
        /// 终结点配置字典
        /// </summary>
        public static IDictionary<string, EndpointConfigurationElement> EndpointConfigurations
        {
            get { return _EndpointConfigurations; }
        }

        /// <summary>
        /// 授权拦截器配置字典
        /// </summary>
        public static IDictionary<string, AuthInterceptorElement> AuthInterceptors
        {
            get { return _AuthInterceptors; }
        }
    }
}
