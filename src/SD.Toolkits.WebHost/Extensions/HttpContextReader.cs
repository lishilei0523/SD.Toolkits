using System.Threading;
using System.Web;

namespace SD.Toolkits.WebHost.Extensions
{
    /// <summary>
    /// HttpContext读取器
    /// </summary>
    public static class HttpContextReader
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        /// <summary>
        /// HttpContext异步线程缓存
        /// </summary>
        private static readonly AsyncLocal<HttpContext> _AsyncHttpContext = new AsyncLocal<HttpContext>();

        /// <summary>
        /// 设置HttpContext
        /// </summary>
        internal static void SetHttpContext(HttpContext httpContext)
        {
            lock (HttpContextReader._Sync)
            {
                HttpContextReader._AsyncHttpContext.Value = httpContext;
            }
        }

        /// <summary>
        /// 清除HttpContext
        /// </summary>
        internal static void ClearHttpContext()
        {
            lock (HttpContextReader._Sync)
            {
                HttpContextReader._AsyncHttpContext.Value = null;
            }
        }

        /// <summary>
        /// 获取当前HttpContext
        /// </summary>
        /// <remarks>主要用于获取异步环境下的HttpContext.Current</remarks>
        public static HttpContext Current
        {
            get
            {
                lock (HttpContextReader._Sync)
                {
                    HttpContext httpContext = HttpContext.Current ?? HttpContextReader._AsyncHttpContext.Value;

                    return httpContext;
                }
            }
        }
    }
}
