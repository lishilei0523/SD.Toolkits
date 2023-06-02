using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// BasicHttp绑定
        /// </summary>
        public const string BasicHttpBinding = "basicHttpBinding";

        /// <summary>
        /// NetTcp绑定
        /// </summary>
        public const string NetTcpBinding = "netTcpBinding";

        /// <summary>
        /// NetHttp绑定
        /// </summary>
        public const string NetHttpBinding = "netHttpBinding";

        /// <summary>
        /// WSHttp绑定
        /// </summary>
        public const string WSHttpBinding = "wsHttpBinding";

        /// <summary>
        /// 可用绑定列表
        /// </summary>
        public static readonly IReadOnlyList<string> AvailableBindings = new List<string>
        {
            BasicHttpBinding, NetTcpBinding, NetHttpBinding, WSHttpBinding
        };
    }
}
