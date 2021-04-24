using SD.Toolkits.WebHost.Extensions;
using System.Web;

namespace SD.Toolkits.WebHost.Modules
{
    /// <summary>
    /// 缓存HttpContext模块
    /// </summary>
    internal class CacheHttpContextModule : IHttpModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.OnBeginRequest;
            context.EndRequest += this.OnEndRequest;
        }

        /// <summary>
        /// 请求开始
        /// </summary>
        private void OnBeginRequest(object sender, System.EventArgs e)
        {
            HttpContextReader.SetHttpContext(HttpContext.Current);
        }

        /// <summary>
        /// 请求结束
        /// </summary>
        private void OnEndRequest(object sender, System.EventArgs e)
        {
            HttpContextReader.ClearHttpContext();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { }
    }
}
