using System;

namespace SD.Toolkits.Grpc.Client.ServiceModels
{
    /// <summary>
    /// gRPC服务客户端代理基类
    /// </summary>
    public abstract class GrpcServiceProxy : IDisposable
    {
        #region # 常量、字段及静态构造器

        /// <summary>
        /// 信道实例属性名
        /// </summary>
        public const string ChannelPropertyName = "Channel";

        /// <summary>
        /// 同步锁
        /// </summary>
        protected static readonly object _Sync;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcServiceProxy()
        {
            _Sync = new object();
        }

        #endregion

        #region # 释放资源 —— abstract void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void Dispose();
        #endregion
    }
}
