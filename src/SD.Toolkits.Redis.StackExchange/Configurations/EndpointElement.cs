using System.Configuration;

namespace SD.Toolkits.Redis.Configurations
{
    /// <summary>
    /// 节点
    /// </summary>
    internal class EndpointElement : ConfigurationElement
    {
        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        #endregion

        #region # 服务器 —— string Host
        /// <summary>
        /// 服务器
        /// </summary>
        [ConfigurationProperty("host", IsRequired = true, IsKey = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }
        #endregion

        #region # 端口 —— int Port
        /// <summary>
        /// 端口
        /// </summary>
        [ConfigurationProperty("port", IsRequired = true, IsKey = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
        #endregion
    }
}
