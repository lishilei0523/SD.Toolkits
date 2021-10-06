// ReSharper disable once CheckNamespace
namespace SD.Toolkits.AspNet
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Http协议
        /// </summary>
        public const string Http = "http";

        /// <summary>
        /// Https协议
        /// </summary>
        public const string Https = "https";

        /// <summary>
        /// Net.TCP协议
        /// </summary>
        public const string NetTcp = "net.tcp";

        /// <summary>
        /// 可用协议列表
        /// </summary>
        public static readonly string[] AvailableProtocols =
        {
            Http, Https, NetTcp
        };
    }
}
