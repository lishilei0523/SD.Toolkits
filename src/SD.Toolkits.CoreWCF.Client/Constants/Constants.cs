using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constants
    {
        public const string BasicHttpBinding = "basicHttpBinding";
        public const string NetTcpBinding = "netTcpBinding";
        public const string NetHttpBinding = "netHttpBinding";
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
