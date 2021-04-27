using System.Configuration;

namespace SD.Toolkits.SerialNumber.Configurations
{
    /// <summary>
    /// 序列种子提供者配置节点
    /// </summary>
    public class SerialSeedProviderElement : ConfigurationElement
    {
        #region # 类型 —— string Type
        /// <summary>
        /// 类型
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }
        #endregion

        #region # 程序集 —— string Assembly
        /// <summary>
        /// 程序集
        /// </summary>
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }
        #endregion
    }
}
