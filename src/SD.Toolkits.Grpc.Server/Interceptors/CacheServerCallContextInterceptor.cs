using Grpc.Core;
using Grpc.Core.Interceptors;
using SD.Toolkits.Grpc.Server.Extensions;
using System.Threading.Tasks;

namespace SD.Toolkits.Grpc.Server.Interceptors
{
    /// <summary>
    /// gRPC缓存ServerCallContext拦截器
    /// </summary>
    public class CacheServerCallContextInterceptor : Interceptor
    {
        #region # 拦截Unary服务处理 —— override Task<TResponse> UnaryServerHandler(TRequest request...
        /// <summary>
        /// 拦截Unary服务处理
        /// </summary>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            ServerCallContextReader.SetServerCallContext(context);

            TResponse response = await continuation.Invoke(request, context);

            ServerCallContextReader.ClearServerCallContext();

            return response;
        }
        #endregion

        #region # 拦截客户端Streaming服务处理 —— override Task<TResponse> ClientStreamingServerHandler(...
        /// <summary>
        /// 拦截客户端Streaming服务处理
        /// </summary>
        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            ServerCallContextReader.SetServerCallContext(context);

            TResponse response = await continuation.Invoke(requestStream, context);

            ServerCallContextReader.ClearServerCallContext();

            return response;
        }
        #endregion

        #region # 拦截服务端Streaming服务处理 —— override Task ServerStreamingServerHandler(TRequest request...
        /// <summary>
        /// 拦截服务端Streaming服务处理
        /// </summary>
        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            ServerCallContextReader.SetServerCallContext(context);

            await continuation.Invoke(request, responseStream, context);

            ServerCallContextReader.ClearServerCallContext();
        }
        #endregion

        #region # 拦截双向Streaming服务处理 —— override Task DuplexStreamingServerHandler(IAsyncStreamReader...
        /// <summary>
        /// 拦截双向Streaming服务处理
        /// </summary>
        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            ServerCallContextReader.SetServerCallContext(context);

            await continuation.Invoke(requestStream, responseStream, context);

            ServerCallContextReader.ClearServerCallContext();
        }
        #endregion
    }
}
