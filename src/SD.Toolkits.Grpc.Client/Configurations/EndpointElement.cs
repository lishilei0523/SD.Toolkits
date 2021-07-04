using System.Configuration;

namespace SD.Toolkits.Grpc.Client.Configurations
{
    /// <summary>
    /// 终节点节点
    /// </summary>
    public class EndpointElement : ConfigurationElement
    {
        #region # 地址 —— string Address
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
        #endregion

        #region # 契约 —— string Contract
        /// <summary>
        /// 契约
        /// </summary>
        [ConfigurationProperty("contract", IsRequired = true)]
        public string Contract
        {
            get { return (string)this["contract"]; }
            set { this["contract"] = value; }
        }
        #endregion

        #region # 授权拦截器列表 —— string AuthInterceptors
        /// <summary>
        /// 授权拦截器列表
        /// </summary>
        [ConfigurationProperty("authInterceptors", IsRequired = false)]
        public string AuthInterceptors
        {
            get { return (string)this["authInterceptors"]; }
            set { this["authInterceptors"] = value; }
        }
        #endregion

        #region # 终结点配置 —— string EndpointConfiguration
        /// <summary>
        /// 终结点配置
        /// </summary>
        [ConfigurationProperty("endpointConfiguration", IsRequired = false)]
        public string EndpointConfiguration
        {
            get { return (string)this["endpointConfiguration"]; }
            set { this["endpointConfiguration"] = value; }
        }
        #endregion
    }
}
