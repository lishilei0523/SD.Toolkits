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

        /// <summary>
        /// 可用绑定列表
        /// </summary>
        public static readonly IReadOnlyList<string> AvailableBindings = new List<string>
        {
            BasicHttpBinding
        };
    }
}
