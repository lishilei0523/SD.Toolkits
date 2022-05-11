using System.Configuration;

namespace SD.Toolkits.AspNet.Configurations
{
    /// <summary>
    /// X509证书节点
    /// </summary>
    public class X509Element : ConfigurationElement
    {
        #region # 路径 —— string Path
        /// <summary>
        /// 路径
        /// </summary>
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
        #endregion

        #region # 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true, IsKey = false)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
        #endregion
    }
}
