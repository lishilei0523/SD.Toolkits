using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using StackExchange.Redis;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.Toolkits.Redis.Tests.TestCases
{
    /// <summary>
    /// 读写测试
    /// </summary>
    [TestClass]
    public class ReadWriteTests
    {
        #region # 测试初始化

        private const string ReadKey = "ReadKey";
        private const string ReadValue = "ReadValue";
        private const string WriteKey = "WriteKey";
        private const string WriteValue = "WriteValue";

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
            IDatabase database = RedisManager.Instance.GetDatabase();
            database.StringSet(ReadKey, ReadValue);
        }

        #endregion

        #region # 测试清理 —— void Cleanup()
        /// <summary>
        /// 测试清理
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            database.KeyDelete(ReadKey);
            database.KeyDelete(WriteKey);
        }
        #endregion

        #region # 测试读取 —— void TestRead()
        /// <summary>
        /// 测试读取
        /// </summary>
        [TestMethod]
        public void TestRead()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            string value = database.StringGet(ReadKey);

            Assert.AreEqual(ReadValue, value);
        }
        #endregion

        #region # 测试写入 —— void TestWrite()
        /// <summary>
        /// 测试写入
        /// </summary>
        [TestMethod]
        public void TestWrite()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            database.StringSet(WriteKey, WriteValue);
            string value = database.StringGet(WriteKey);

            Assert.AreEqual(WriteValue, value);
        }
        #endregion
    }
}
