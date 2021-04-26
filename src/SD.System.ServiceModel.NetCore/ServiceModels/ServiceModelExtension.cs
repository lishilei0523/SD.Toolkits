using System.Collections.Generic;
using System.ServiceModel.Channels;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.NetCore
{
    /// <summary>
    /// WCF扩展工具类
    /// </summary>
    public static class ServiceModelExtension
    {
        /// <summary>
        /// BasicHttp绑定名
        /// </summary>
        private const string BasicHttpBindingName = "basicHttpBinding";

        /// <summary>
        /// NetTcp绑定名
        /// </summary>
        private const string NetTcpBindingName = "netTcpBinding";

        /// <summary>
        /// 超时时间
        /// </summary>
        private static readonly TimeSpan _Timeout = new TimeSpan(0, 10, 0);

        /// <summary>
        /// BasicHttp绑定实例
        /// </summary>
        public static BasicHttpBinding BasicHttpBinding
        {
            get
            {
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None)
                {
                    MaxBufferPoolSize = int.MaxValue,
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                    CloseTimeout = _Timeout,
                    OpenTimeout = _Timeout,
                    ReceiveTimeout = _Timeout,
                    SendTimeout = _Timeout,
                    Security = new BasicHttpSecurity
                    {
                        Mode = BasicHttpSecurityMode.None
                    }
                };

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
                NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None)
                {
                    MaxBufferPoolSize = int.MaxValue,
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                    CloseTimeout = _Timeout,
                    OpenTimeout = _Timeout,
                    ReceiveTimeout = _Timeout,
                    SendTimeout = _Timeout,
                    Security = new NetTcpSecurity
                    {
                        Mode = SecurityMode.None,
                        Transport = new TcpTransportSecurity
                        {
                            ClientCredentialType = TcpClientCredentialType.None
                        },
                        Message = new MessageSecurityOverTcp { ClientCredentialType = MessageCredentialType.None }
                    }
                };

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

        /// <summary>
        /// 关闭信道
        /// </summary>
        /// <param name="channel">信道实例</param>
        public static void CloseChannel(this object channel)
        {
            if (!(channel is ICommunicationObject communicationObject))
            {
                return;
            }
            try
            {
                if (communicationObject.State == CommunicationState.Faulted)
                {
                    communicationObject.Abort();
                }
                else
                {
                    communicationObject.Close();
                }
            }
            catch (TimeoutException)
            {
                communicationObject.Abort();
            }
            catch (CommunicationException)
            {
                communicationObject.Abort();
            }
            catch (Exception)
            {
                communicationObject.Abort();
                throw;
            }
        }
    }
}