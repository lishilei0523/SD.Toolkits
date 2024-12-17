using SD.Toolkits.Redis.Configurations;
using StackExchange.Redis;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Redis
{
    /// <summary>
    /// Redis管理器
    /// </summary>
    public static class RedisManager
    {
        #region # 字段及构造器

        /// <summary>
        /// Redis密码
        /// </summary>
        private static readonly string _RedisPassword;

        /// <summary>
        /// Redis终结点列表
        /// </summary>
        private static readonly IList<KeyValuePair<string, int>> _RedisEndpoints;

        /// <summary>
        /// Redis配置选项
        /// </summary>
        private static readonly ConfigurationOptions _RedisConfigurationOptions;

        /// <summary>
        /// Redis连接通道
        /// </summary>
        private static readonly ConnectionMultiplexer _Instance;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static RedisManager()
        {
            _RedisEndpoints = new List<KeyValuePair<string, int>>();
            _RedisConfigurationOptions = new ConfigurationOptions();
            _RedisConfigurationOptions.KeepAlive = 180;
            _RedisConfigurationOptions.AllowAdmin = true;
            foreach (EndpointElement endpoint in RedisSection.Setting.EndpointElements)
            {
                _RedisConfigurationOptions.EndPoints.Add(endpoint.Host, endpoint.Port);
                _RedisEndpoints.Add(new KeyValuePair<string, int>(endpoint.Host, endpoint.Port));
            }
            if (!string.IsNullOrWhiteSpace(RedisSection.Setting.Password))
            {
                _RedisConfigurationOptions.Password = RedisSection.Setting.Password;
                _RedisPassword = RedisSection.Setting.Password;
            }

            _Instance = ConnectionMultiplexer.Connect(_RedisConfigurationOptions);
        }

        #endregion

        #region # 属性

        #region Redis密码 —— static string RedisPassword
        /// <summary>
        /// Redis密码
        /// </summary>
        public static string RedisPassword
        {
            get => _RedisPassword;
        }
        #endregion

        #region Redis终结点列表 —— static IList<KeyValuePair<string, int>> RedisEndpoints
        /// <summary>
        /// Redis终结点列表
        /// </summary>
        public static IList<KeyValuePair<string, int>> RedisEndpoints
        {
            get => _RedisEndpoints;
        }
        #endregion

        #region Redis配置选项 —— static ConfigurationOptions RedisConfigurationOptions
        /// <summary>
        /// Redis配置选项
        /// </summary>
        public static ConfigurationOptions RedisConfigurationOptions
        {
            get => _RedisConfigurationOptions;
        }
        #endregion

        #region Redis连接通道实例 —— static ConnectionMultiplexer Instance
        /// <summary>
        /// Redis连接通道实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get => _Instance;
        }
        #endregion

        #endregion

        #region # 方法

        #region 获取数据库 —— static IDatabase GetDatabase(int db = -1, object asyncState = null)
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns>数据库</returns>
        public static IDatabase GetDatabase(int db = -1, object asyncState = null)
        {
            return _Instance.GetDatabase(db, asyncState);
        }
        #endregion 

        #endregion
    }
}
