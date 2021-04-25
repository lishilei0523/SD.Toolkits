using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel.NetCore.Configurations;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.NetCore
{
    /// <summary>
    /// 终结点配置中介者
    /// </summary>
    public static class EndpointMediator
    {
        /// <summary>
        /// 终结点配置字典
        /// </summary>
        private static readonly IDictionary<string, EndpointElement> _Endpoints;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static EndpointMediator()
        {
            _Endpoints = new ConcurrentDictionary<string, EndpointElement>();
            foreach (EndpointElement endpoint in ServiceModelSection.Setting.Endpoints)
            {
                _Endpoints.Add(endpoint.Name, endpoint);
            }

        }

        /// <summary>
        /// 终结点配置字典
        /// </summary>
        public static IDictionary<string, EndpointElement> Endpoints
        {
            get { return _Endpoints; }
        }
    }
}
