using Grpc.Core;
using Grpc.Net.Client;
using SD.Toolkits.Grpc.Client.Configurations;
using SD.Toolkits.Grpc.Client.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.Toolkits.Grpc.Client.ServiceModels
{
    /// <summary>
    /// gRPC信道管理者
    /// </summary>
    public static class GrpcChannelManager
    {
        /// <summary>
        /// gRPC信道字典
        /// </summary>
        private static readonly IDictionary<string, GrpcChannel> _GrpcChannels;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcChannelManager()
        {
            _GrpcChannels = new ConcurrentDictionary<string, GrpcChannel>();
            foreach (EndpointElement endpoint in GrpcSection.Setting.EndpointElements)
            {
                if (!_GrpcChannels.ContainsKey(endpoint.Address))
                {
                    GrpcChannel channel = CreateGrpcChannel(endpoint);
                    _GrpcChannels.Add(endpoint.Address, channel);
                }
            }
        }

        /// <summary>
        /// 获取gRPC信道
        /// </summary>
        /// <param name="uri">连接地址</param>
        /// <returns>gRPC信道</returns>
        public static GrpcChannel GetGrpcChannel(string uri)
        {
            #region # 验证

            if (!_GrpcChannels.ContainsKey(uri))
            {
                throw new ArgumentOutOfRangeException(nameof(uri), $"未找到uri为\"{uri}\"的gRPC信道！");
            }

            #endregion

            GrpcChannel channel = _GrpcChannels[uri];

            return channel;
        }

        /// <summary>
        /// 创建gRPC信道
        /// </summary>
        /// <param name="endpoint">终结点配置</param>
        /// <returns>gRPC信道</returns>
        private static GrpcChannel CreateGrpcChannel(EndpointElement endpoint)
        {
            //读取配置文件
            EndpointConfigurationElement endpointConfiguration = null;
            IList<AuthInterceptorElement> authInterceptorElements = new List<AuthInterceptorElement>();
            if (!string.IsNullOrWhiteSpace(endpoint.EndpointConfiguration))
            {
                endpointConfiguration = EndpointMediator.EndpointConfigurations[endpoint.EndpointConfiguration];
            }
            if (!string.IsNullOrWhiteSpace(endpoint.AuthInterceptors))
            {
                string[] authInterceptorNames = endpoint.AuthInterceptors.Split(',');
                authInterceptorElements = EndpointMediator.AuthInterceptors.Where<KeyValuePair<string, AuthInterceptorElement>>(x => authInterceptorNames.Contains(x.Key)).Select(x => x.Value).ToList();
            }

            //构造身份凭据
            IList<CallCredentials> callCredentials = new List<CallCredentials>();
            foreach (AuthInterceptorElement authInterceptorElement in authInterceptorElements)
            {
                Assembly assembly = Assembly.Load(authInterceptorElement.Assembly);
                Type type = assembly.GetType(authInterceptorElement.Type);
                IAuthInterceptor authInterceptor = (IAuthInterceptor)Activator.CreateInstance(type);

                CallCredentials callCredential = CallCredentials.FromInterceptor(authInterceptor.AuthIntercept);
                callCredentials.Add(callCredential);
            }
            CallCredentials credentials = null;
            if (callCredentials.Count == 1)
            {
                credentials = callCredentials.Single();
            }
            if (callCredentials.Count > 1)
            {
                credentials = CallCredentials.Compose(callCredentials.ToArray());
            }

            //构造gRPC信道选项
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                MaxSendMessageSize = endpointConfiguration?.MaxSendMessageSize,
                MaxReceiveMessageSize = endpointConfiguration?.MaxReceiveMessageSize,
                MaxRetryAttempts = endpointConfiguration?.MaxRetryAttempts,
                MaxRetryBufferSize = endpointConfiguration?.MaxRetryBufferSize,
                MaxRetryBufferPerCallSize = endpointConfiguration?.MaxRetryBufferPerCallSize,
                DisposeHttpClient = endpointConfiguration?.DisposeHttpClient ?? false,
                ThrowOperationCanceledOnCancellation = endpointConfiguration?.ThrowOperationCanceledOnCancellation ?? false
            };
            if (credentials != null)
            {
                channelOptions.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);
            }

            //创建gRPC信道
            GrpcChannel channel = GrpcChannel.ForAddress(endpoint.Address, channelOptions);

            return channel;
        }
    }
}
