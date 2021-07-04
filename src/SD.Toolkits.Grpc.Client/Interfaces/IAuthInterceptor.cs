using Grpc.Core;
using System.Threading.Tasks;

namespace SD.Toolkits.Grpc.Client.Interfaces
{
    /// <summary>
    /// 授权拦截器接口
    /// </summary>
    public interface IAuthInterceptor
    {
        /// <summary>
        /// 授权拦截
        /// </summary>
        /// <param name="context">拦截上下文</param>
        /// <param name="metadata">元数据</param>
        Task AuthIntercept(AuthInterceptorContext context, Metadata metadata);
    }
}
