using System.Collections.Generic;
using System.ServiceModel.Channels;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.NetCore
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// BasicHttp绑定名
        /// </summary>
        public const string BasicHttpBindingName = "basicHttpBinding";

        /// <summary>
        /// NetTcp绑定名
        /// </summary>
        public const string NetTcpBindingName = "netTcpBinding";

        /// <summary>
        /// BasicHttp绑定实例
        /// </summary>
        public static BasicHttpBinding BasicHttpBinding
        {
            get
            {
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.MaxBufferPoolSize = 2147483647;
                basicHttpBinding.MaxReceivedMessageSize = 2147483647;

                TimeSpan defaultSpan = new TimeSpan(0, 10, 0);
                basicHttpBinding.CloseTimeout = defaultSpan;
                basicHttpBinding.OpenTimeout = defaultSpan;
                basicHttpBinding.ReceiveTimeout = defaultSpan;
                basicHttpBinding.SendTimeout = defaultSpan;

                return basicHttpBinding;
            }
        }

        /// <summary>
        /// NetTcp绑定实例
        /// </summary>
        public static NetTcpBinding NetTcpBinding
        {
            get
            {
                NetTcpBinding netTcpBinding = new NetTcpBinding();
                netTcpBinding.MaxBufferPoolSize = 2147483647;
                netTcpBinding.MaxReceivedMessageSize = 2147483647;

                TimeSpan defaultSpan = new TimeSpan(0, 10, 0);
                netTcpBinding.CloseTimeout = defaultSpan;
                netTcpBinding.OpenTimeout = defaultSpan;
                netTcpBinding.ReceiveTimeout = defaultSpan;
                netTcpBinding.SendTimeout = defaultSpan;

                return netTcpBinding;
            }
        }

        /// <summary>
        /// 可用绑定字典
        /// </summary>
        public static readonly IDictionary<string, Binding> AvailableBindings = new Dictionary<string, Binding>
        {
            {BasicHttpBindingName, BasicHttpBinding},
            {NetTcpBindingName, NetTcpBinding}
        };
    }
}
