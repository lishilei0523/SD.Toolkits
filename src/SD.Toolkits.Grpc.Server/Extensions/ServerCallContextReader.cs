using Grpc.Core;
using System.Threading;

namespace SD.Toolkits.Grpc.Server.Extensions
{
    /// <summary>
    /// gRPC服务请求上下文读取器
    /// </summary>
    public static class ServerCallContextReader
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        /// <summary>
        /// gRPC服务请求上下文异步线程缓存
        /// </summary>
        private static readonly AsyncLocal<ServerCallContext> _ServerCallContext = new AsyncLocal<ServerCallContext>();

        /// <summary>
        /// 设置gRPC服务请求上下文
        /// </summary>
        internal static void SetServerCallContext(ServerCallContext context)
        {
            lock (_Sync)
            {
                _ServerCallContext.Value = context;
            }
        }

        /// <summary>
        /// 清除gRPC服务请求上下文
        /// </summary>
        internal static void ClearServerCallContext()
        {
            lock (_Sync)
            {
                _ServerCallContext.Value = null;
            }
        }

        /// <summary>
        /// 获取当前gRPC服务请求上下文
        /// </summary>
        public static ServerCallContext Current
        {
            get
            {
                lock (_Sync)
                {
                    return _ServerCallContext.Value;
                }
            }
        }
    }
}
