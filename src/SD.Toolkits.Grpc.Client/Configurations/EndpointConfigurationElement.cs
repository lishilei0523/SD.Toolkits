using System.Configuration;

namespace SD.Toolkits.Grpc.Client.Configurations
{
    /// <summary>
    /// 终结点配置节点
    /// </summary>
    public class EndpointConfigurationElement : ConfigurationElement
    {
        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }
        #endregion

        #region # 最大发送消息规模 —— int? MaxSendMessageSize
        /// <summary>
        /// 最大发送消息规模
        /// </summary>
        [ConfigurationProperty("maxSendMessageSize", IsRequired = false)]
        public int? MaxSendMessageSize
        {
            get { return (int?)this["maxSendMessageSize"]; }
            set { this["maxSendMessageSize"] = value; }
        }
        #endregion

        #region # 最大接收消息规模 —— int? MaxReceiveMessageSize
        /// <summary>
        /// 最大接收消息规模
        /// </summary>
        [ConfigurationProperty("maxReceiveMessageSize", IsRequired = false)]
        public int? MaxReceiveMessageSize
        {
            get { return (int?)this["maxReceiveMessageSize"]; }
            set { this["maxReceiveMessageSize"] = value; }
        }
        #endregion

        #region # 最大重试次数 —— int? MaxRetryAttempts
        /// <summary>
        /// 最大重试次数
        /// </summary>
        [ConfigurationProperty("maxRetryAttempts", IsRequired = false)]
        public int? MaxRetryAttempts
        {
            get { return (int?)this["maxRetryAttempts"]; }
            set { this["maxRetryAttempts"] = value; }
        }
        #endregion

        #region # 最大重试缓冲区规模 —— long? MaxRetryBufferSize
        /// <summary>
        /// 最大重试缓冲区规模
        /// </summary>
        [ConfigurationProperty("maxRetryBufferSize", IsRequired = false)]
        public long? MaxRetryBufferSize
        {
            get { return (long?)this["maxRetryBufferSize"]; }
            set { this["maxRetryBufferSize"] = value; }
        }
        #endregion

        #region # 最大重试缓冲区每次请求规模 —— long? MaxRetryBufferPerCallSize
        /// <summary>
        /// 最大重试缓冲区每次请求规模
        /// </summary>
        [ConfigurationProperty("maxRetryBufferPerCallSize", IsRequired = false)]
        public long? MaxRetryBufferPerCallSize
        {
            get { return (long?)this["maxRetryBufferPerCallSize"]; }
            set { this["maxRetryBufferPerCallSize"] = value; }
        }
        #endregion

        #region # 是否释放HttpClient —— bool? DisposeHttpClient
        /// <summary>
        /// 是否释放HttpClient
        /// </summary>
        [ConfigurationProperty("disposeHttpClient", IsRequired = false)]
        public bool? DisposeHttpClient
        {
            get { return (bool?)this["disposeHttpClient"]; }
            set { this["disposeHttpClient"] = value; }
        }
        #endregion

        #region # 取消时是否抛出异常 —— bool? ThrowOperationCanceledOnCancellation
        /// <summary>
        /// 取消时是否抛出异常
        /// </summary>
        [ConfigurationProperty("throwOperationCanceledOnCancellation", IsRequired = false)]
        public bool? ThrowOperationCanceledOnCancellation
        {
            get { return (bool?)this["throwOperationCanceledOnCancellation"]; }
            set { this["throwOperationCanceledOnCancellation"] = value; }
        }
        #endregion
    }
}
