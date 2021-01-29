using Microsoft.Owin;
using SD.Toolkits.Owin.Extensions;
using System.Threading.Tasks;

namespace SD.Toolkits.Owin.Middlewares
{
    /// <summary>
    /// 缓存OwinContext中间件
    /// </summary>
    public class CacheOwinContextMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 默认构造器
        /// </summary>
        public CacheOwinContextMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        public override async Task Invoke(IOwinContext context)
        {
            OwinContextReader.SetOwinContext(context);

            await base.Next.Invoke(context);

            OwinContextReader.ClearOwinContext();
        }
    }
}
