using System.ComponentModel;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace SD.Toolkits.SessionSharing
{
    /// <summary>
    /// Web应用程序Session共享
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreAppStart
    {
        /// <summary>
        /// 是否已初始化
        /// </summary>
        private static bool _InitWasCalled;

        /// <summary>
        /// 初始化Session共享
        /// </summary>
        public static void InitSessionSharing()
        {
            if (!_InitWasCalled)
            {
                _InitWasCalled = true;
                DynamicModuleUtility.RegisterModule(typeof(SessionSharingModule));
            }
        }
    }
}
