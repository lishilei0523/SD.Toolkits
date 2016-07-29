using System.Configuration;

namespace SD.Toolkits.Redis.Configuration
{
    /// <summary>
    /// 服务器节点
    /// </summary>
    public class ServerElement : ConfigurationElement
    {
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

        #region # 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false, IsKey = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
        #endregion
    }
}
