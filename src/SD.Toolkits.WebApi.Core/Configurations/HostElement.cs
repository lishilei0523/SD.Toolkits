using System.Configuration;

namespace SD.Toolkits.WebApi.Core.Configurations
{
    /// <summary>
    /// 节点
    /// </summary>
    public class HostElement : ConfigurationElement
    {
        #region # IP地址 —— string IPAddress
        /// <summary>
        /// IP地址
        /// </summary>
        [ConfigurationProperty("ipAddress", IsRequired = true, IsKey = true)]
        public string IPAddress
        {
            get { return (string)this["ipAddress"]; }
            set { this["ipAddress"] = value; }
        }
        #endregion

        #region # 端口号 —— int Port
        /// <summary>
        /// 端口号
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
