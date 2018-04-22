using SD.Toolkits.Redis.Configurations;
using StackExchange.Redis;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Redis
{
    /// <summary>
    /// Redis管理器
    /// </summary>
    public static class RedisManager
    {
        /// <summary>
        /// Redis连接通道字段
        /// </summary>
        private static readonly ConnectionMultiplexer _Instance;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static RedisManager()
        {
            ConfigurationOptions config = new ConfigurationOptions();

            foreach (EndpointElement endpoint in RedisSection.Setting.EndpointElement)
            {
                config.EndPoints.Add(endpoint.Host, endpoint.Port);
            }

            config.KeepAlive = 180;
            config.AllowAdmin = true;

            if (string.IsNullOrWhiteSpace(RedisSection.Setting.Password))
            {
                config.Password = RedisSection.Setting.Password;
            }

            _Instance = ConnectionMultiplexer.Connect(config);
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
        public static IDatabase GetDatabase()
        {
            return _Instance.GetDatabase();
        }
    }
}
