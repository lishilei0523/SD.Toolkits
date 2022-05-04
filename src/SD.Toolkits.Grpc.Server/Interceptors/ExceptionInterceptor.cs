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
        /// <summary>
        /// 拦截服务请求
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
    }
}
