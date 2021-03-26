using Microsoft.AspNetCore.Http;
using SD.Toolkits.Owin.Core.Extensions;
using System.Threading.Tasks;

namespace SD.Toolkits.Owin.Core.Middlewares
{
    /// <summary>
    /// 缓存OwinContext中间件
    /// </summary>
    public class CacheOwinContextMiddleware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public CacheOwinContextMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            OwinContextReader.SetOwinContext(context);

            await this._next.Invoke(context);

            OwinContextReader.ClearOwinContext();
        }
    }
}
