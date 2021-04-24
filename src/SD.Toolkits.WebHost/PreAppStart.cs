using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SD.Toolkits.WebHost.Modules;
using System.ComponentModel;

namespace SD.Toolkits.WebHost
{
    /// <summary>
    /// 应用程序预启动
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreAppStart
    {
        /// <summary>
        /// 是否已初始化
        /// </summary>
        private static bool _InitWasCalled;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (!PreAppStart._InitWasCalled)
            {
                PreAppStart._InitWasCalled = true;
                DynamicModuleUtility.RegisterModule(typeof(CacheHttpContextModule));
            }
        }
    }
}
