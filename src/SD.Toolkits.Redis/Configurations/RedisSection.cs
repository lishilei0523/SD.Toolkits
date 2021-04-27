using SD.Toolkits.Redis.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits
{
    /// <summary>
    /// SD.Toolkits.Redis配置
    /// </summary>
    internal class RedisSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly RedisSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static RedisSection()
        {
            _Setting = (RedisSection)ConfigurationManager.GetSection("sd.toolkits.redis");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("SD.Toolkits.Redis节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static RedisSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static RedisSection Setting
        {
            get { return _Setting; }
        }
        #endregion

        #region # 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("password", IsRequired = false, IsKey = false)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
        #endregion

        #region # 节点地址列表 —— EndpointElementCollection EndpointElements
        /// <summary>
        /// 节点地址列表
        /// </summary>
        [ConfigurationProperty("endpoints")]
        [ConfigurationCollection(typeof(EndpointElementCollection), AddItemName = "endpoint")]
        public EndpointElementCollection EndpointElements
        {
            get
            {
                EndpointElementCollection collection = this["endpoints"] as EndpointElementCollection;
                return collection ?? new EndpointElementCollection();
            }
            set { this["endpoints"] = value; }
        }
        #endregion
    }
}
