using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Threading.Tasks;

namespace SD.Toolkits.Grpc.Server.Interceptors
{
    /// <summary>
    /// gRPC异常拦截器
    /// </summary>
    public class ExceptionInterceptor : Interceptor
    {
        #region # 拦截Unary服务处理 —— override Task<TResponse> UnaryServerHandler(TRequest request...
        /// <summary>
        /// 拦截Unary服务处理
        /// </summary>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation.Invoke(request, context);
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Exception innerException = GetInnerException(exception);
                Status status = new Status(StatusCode.Aborted, innerException.Message, exception);
                throw new RpcException(status);
            }
        }
        #endregion

        #region # 拦截客户端Streaming服务处理 —— override Task<TResponse> ClientStreamingServerHandler(...
        /// <summary>
        /// 拦截客户端Streaming服务处理
        /// </summary>
        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation.Invoke(requestStream, context);
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Exception innerException = GetInnerException(exception);
                Status status = new Status(StatusCode.Aborted, innerException.Message, exception);
                throw new RpcException(status);
            }
        }
        #endregion

        #region # 拦截服务端Streaming服务处理 —— override Task ServerStreamingServerHandler(TRequest request...
        /// <summary>
        /// 拦截服务端Streaming服务处理
        /// </summary>
        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation.Invoke(request, responseStream, context);
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Exception innerException = GetInnerException(exception);
                Status status = new Status(StatusCode.Aborted, innerException.Message, exception);
                throw new RpcException(status);
            }
        }
        #endregion

        #region # 拦截双向Streaming服务处理 —— override Task DuplexStreamingServerHandler(IAsyncStreamReader...
        /// <summary>
        /// 拦截双向Streaming服务处理
        /// </summary>
        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation.Invoke(requestStream, responseStream, context);
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Exception innerException = GetInnerException(exception);
                Status status = new Status(StatusCode.Aborted, innerException.Message, exception);
                throw new RpcException(status);
            }
        }
        #endregion


        //Private

        #region # 递归获取内部异常 —— static Exception GetInnerException(Exception exception)
        /// <summary>
        /// 递归获取内部异常
        /// </summary>
        private static Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnerException(exception.InnerException);
            }

            return exception;
        }
        #endregion
    }
}
