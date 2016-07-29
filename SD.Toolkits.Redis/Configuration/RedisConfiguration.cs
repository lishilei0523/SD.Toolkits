using System;
using System.Configuration;
using SD.Toolkits.Redis.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits
{
    /// <summary>
    /// Redis服务器配置
    /// </summary>
    public class RedisConfiguration : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly RedisConfiguration _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static RedisConfiguration()
        {
            _Setting = (RedisConfiguration)ConfigurationManager.GetSection("redisConfiguration");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("Redis节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static RedisConfiguration Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static RedisConfiguration Setting
        {
            get { return _Setting; }
        }
        #endregion

        #region # 读写服务器地址列表 —— ServerElementCollection ReadWriteServers
        /// <summary>
        /// 读写服务器地址列表
        /// </summary>
        [ConfigurationProperty("readWriteServers")]
        [ConfigurationCollection(typeof(ServerElementCollection), AddItemName = "server")]
        public ServerElementCollection ReadWriteServers
        {
            get
            {
                ServerElementCollection collection = this["readWriteServers"] as ServerElementCollection;
                return collection ?? new ServerElementCollection();
            }
            set { this["readWriteServers"] = value; }
        }
        #endregion

        #region # 只读服务器地址列表 —— ServerElementCollection ReadOnlyServers
        /// <summary>
        /// 读写服务器地址列表
        /// </summary>
        [ConfigurationProperty("readOnlyServers")]
        [ConfigurationCollection(typeof(ServerElementCollection), AddItemName = "server")]
        public ServerElementCollection ReadOnlyServers
        {
            get
            {
                ServerElementCollection collection = this["readOnlyServers"] as ServerElementCollection;
                return collection ?? new ServerElementCollection();
            }
            set { this["readOnlyServers"] = value; }
        }
        #endregion
    }
}
