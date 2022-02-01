using Grpc.Net.Client;
using SD.Toolkits.Grpc.Client.Configurations;
using System;

namespace SD.Toolkits.Grpc.Client.ServiceModels
{
    /// <summary>
    /// gRPC服务客户端代理
    /// </summary>
    /// <typeparam name="T">服务契约类型</typeparam>
    public sealed class GrpcServiceProxy<T> : GrpcServiceProxy
    {
        #region # 字段

        /// <summary>
        /// 信道实例
        /// </summary>
        private T _channel;

        #endregion

        #region # 只读属性 - 信道 —— T Channel
        /// <summary>
        /// 只读属性 - 信道
        /// </summary>
        public T Channel
        {
            get
            {
                lock (_Sync)
                {
                    Type serviceType = typeof(T);
                    EndpointElement endpoint = GrpcSetting.Endpoints[serviceType.FullName];
                    GrpcChannel grpcChannel = GrpcChannelManager.GetGrpcChannel(endpoint.Address);
                    this._channel = (T)grpcChannel.CreateGrpcService(serviceType);

                    return this._channel;
                }
            }
        }
        #endregion

        #region # 关闭客户端信道 —— void Close()
        /// <summary>
        /// 关闭客户端信道
        /// </summary>
        public void Close()
        {
            if (this._channel is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        #endregion

        #region # 释放资源 —— override void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            this.Close();
        }
        #endregion
    }
}
