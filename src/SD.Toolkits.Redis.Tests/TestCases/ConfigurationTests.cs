using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.Redis.Configurations;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace SD.Toolkits.Redis.Tests.TestCases
{
    /// <summary>
    /// 配置文件测试
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            RedisSection.Initialize(configuration);
#endif
        }
        #endregion

        #region # 测试配置文件 —— void TestConfigurations()
        /// <summary>
        /// 测试配置文件
        /// </summary>
        [TestMethod]
        public void TestConfigurations()
        {
            string password = RedisSection.Setting.Password;
            Trace.WriteLine(password);

            foreach (EndpointElement endpoint in RedisSection.Setting.EndpointElements)
            {
                Trace.WriteLine(endpoint.Name);
                Trace.WriteLine(endpoint.Host);
                Trace.WriteLine(endpoint.Port);
            }
        }
        #endregion
    }
}
