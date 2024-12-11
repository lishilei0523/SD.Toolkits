using Microsoft.AspNetCore.Http;
using System.Threading;

namespace SD.Toolkits.AspNetCore.Extensions
{
    /// <summary>
    /// OwinContext读取器
    /// </summary>
    public static class OwinContextReader
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        /// <summary>
        /// OwinContext异步线程缓存
        /// </summary>
        private static readonly AsyncLocal<HttpContext> _OwinContext = new AsyncLocal<HttpContext>();

        /// <summary>
        /// 设置OwinContext
        /// </summary>
        internal static void SetOwinContext(HttpContext owinContext)
        {
            lock (_Sync)
            {
                _OwinContext.Value = owinContext;
            }
        }

        /// <summary>
        /// 清除OwinContext
        /// </summary>
        internal static void ClearOwinContext()
        {
            lock (_Sync)
            {
                _OwinContext.Value = null;
            }
        }

        /// <summary>
        /// 获取当前OwinContext
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                lock (_Sync)
                {
                    HttpContext owinContext = _OwinContext.Value;

                    return owinContext;
                }
            }
        }
    }
}
