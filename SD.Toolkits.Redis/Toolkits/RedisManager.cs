using System.Collections.Generic;
using SD.Toolkits.Redis.Configuration;
using ServiceStack.Redis;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Redis
{
    /// <summary>
    /// Redis管理器
    /// </summary>
    public static class RedisManager
    {
        #region # 创建客户端管理器 —— static IRedisClientsManager CreateClientsManager()
        /// <summary>
        /// 创建客户端管理器
        /// </summary>
        /// <returns></returns>
        public static IRedisClientsManager CreateClientsManager()
        {
            IList<string> readWriteServers = new List<string>();
            IList<string> readOnlyServers = new List<string>();

            #region # 构造读写服务器

            foreach (ServerElement element in RedisConfiguration.Setting.ReadWriteServers)
            {
                string server;

                if (string.IsNullOrWhiteSpace(element.Password))
                {
                    server = string.Format("{0}@{1}:{2}", element.Password, element.Host, element.Port);
                }
                else
                {
                    server = string.Format("{0}:{1}", element.Host, element.Port);
                }
                readWriteServers.Add(server);
            }

            #endregion

            #region # 构造器只读服务器

            foreach (ServerElement element in RedisConfiguration.Setting.ReadOnlyServers)
            {
                string server;

                if (string.IsNullOrWhiteSpace(element.Password))
                {
                    server = string.Format("{0}@{1}:{2}", element.Password, element.Host, element.Port);
                }
                else
                {
                    server = string.Format("{0}:{1}", element.Host, element.Port);
                }
                readOnlyServers.Add(server);
            }

            #endregion

            return new PooledRedisClientManager(readWriteServers, readOnlyServers);
        }
        #endregion
    }
}
