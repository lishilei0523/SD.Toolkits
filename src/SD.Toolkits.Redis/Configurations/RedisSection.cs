using SD.Toolkits.Redis.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits
{
    /// <summary>
    /// SD.Toolkits.Redis配置
    /// </summary>
    public class RedisSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static RedisSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static RedisSection()
        {
            _Setting = null;
        }

        #endregion

        #region # 初始化 —— static void Initialize(Configuration configuration)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public static void Initialize(Configuration configuration)
        {
            #region # 验证

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "配置不可为空！");
            }

            #endregion

            _Setting = (RedisSection)configuration.GetSection("sd.toolkits.redis");
        }
        #endregion

        #region # 访问器 —— static RedisSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static RedisSection Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = (RedisSection)ConfigurationManager.GetSection("sd.toolkits.redis");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("SD.Toolkits.Redis节点未配置，请检查程序！");
                }

                return _Setting;
            }
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
