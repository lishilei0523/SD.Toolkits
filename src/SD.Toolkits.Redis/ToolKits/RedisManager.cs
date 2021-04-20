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
        /// <summary>
        /// Redis密码
        /// </summary>
        private static readonly string _RedisPassword;

        /// <summary>
        /// Redis终结点列表
        /// </summary>
        private static readonly IList<KeyValuePair<string, int>> _RedisEndpoints;

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
            ConfigurationOptions config = new ConfigurationOptions();

            foreach (EndpointElement endpoint in RedisSection.Setting.EndpointElement)
            {
                config.EndPoints.Add(endpoint.Host, endpoint.Port);
                _RedisEndpoints.Add(new KeyValuePair<string, int>(endpoint.Host, endpoint.Port));
            }

            config.KeepAlive = 180;
            config.AllowAdmin = true;

            if (!string.IsNullOrWhiteSpace(RedisSection.Setting.Password))
            {
                config.Password = RedisSection.Setting.Password;
                _RedisPassword = RedisSection.Setting.Password;
            }

            _Instance = ConnectionMultiplexer.Connect(config);
        }


        /// <summary>
        /// Redis密码
        /// </summary>
        public static string RedisPassword
        {
            get { return _RedisPassword; }
        }

        /// <summary>
        /// Redis终结点列表
        /// </summary>
        public static IList<KeyValuePair<string, int>> RedisEndpoints
        {
            get { return _RedisEndpoints; }
        }

        /// <summary>
        /// Redis连接通道
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get { return _Instance; }
        }


        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns>数据库</returns>
        public static IDatabase GetDatabase(int db = -1, object asyncState = null)
        {
            return _Instance.GetDatabase(db, asyncState);
        }
    }
}
