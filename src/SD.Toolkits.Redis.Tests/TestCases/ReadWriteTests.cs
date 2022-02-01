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
        private const string ReadKey = "ReadKey";
        private const string ReadValue = "ReadValue";
        private const string WriteKey = "WriteKey";
        private const string WriteValue = "WriteValue";

        [TestInitialize]
        public void Init()
        {
#if NETCOREAPP3_1_OR_GREATER
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            RedisSection.Initialize(configuration);
#endif

            IDatabase database = RedisManager.Instance.GetDatabase();
            database.StringSet(ReadKey, ReadValue);
        }

        [TestCleanup]
        public void Cleanup()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            database.KeyDelete(ReadKey);
            database.KeyDelete(WriteKey);
        }


        [TestMethod]
        public void ReadTest()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            string value = database.StringGet(ReadKey);

            Assert.AreEqual(ReadValue, value);
        }

        [TestMethod]
        public void WriteTest()
        {
            IDatabase database = RedisManager.Instance.GetDatabase();
            database.StringSet(WriteKey, WriteValue);
            string value = database.StringGet(WriteKey);

            Assert.AreEqual(WriteValue, value);
        }
    }
}
