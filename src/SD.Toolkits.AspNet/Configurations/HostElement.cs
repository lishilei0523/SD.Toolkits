using System.Configuration;

namespace SD.Toolkits.AspNet.Configurations
{
    /// <summary>
    /// 节点
    /// </summary>
    public class HostElement : ConfigurationElement
    {
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

        #region # 协议 —— string Protocol
        /// <summary>
        /// 协议
        /// </summary>
        [ConfigurationProperty("protocol", IsRequired = true, IsKey = false)]
        public string Protocol
        {
            get { return (string)this["protocol"]; }
            set { this["protocol"] = value; }
        }
        #endregion
    }
}
