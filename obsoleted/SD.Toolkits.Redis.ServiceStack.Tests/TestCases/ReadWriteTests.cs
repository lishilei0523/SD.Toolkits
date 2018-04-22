using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Redis;

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
            IRedisClientsManager clientsManager = RedisManager.CreateClientsManager();
            IRedisClient client = clientsManager.GetClient();
            client.Set(ReadWriteTests.ReadKey, ReadWriteTests.ReadValue);
        }

        [TestCleanup]
        public void Cleanup()
        {
            IRedisClientsManager clientsManager = RedisManager.CreateClientsManager();
            IRedisClient client = clientsManager.GetClient();
            client.Remove(ReadWriteTests.ReadKey);
            client.Remove(ReadWriteTests.WriteKey);
        }


        [TestMethod]
        public void ReadTest()
        {
            IRedisClientsManager clientsManager = RedisManager.CreateClientsManager();
            IRedisClient client = clientsManager.GetClient();
            string value = client.Get<string>(ReadWriteTests.ReadKey);

            Assert.AreEqual(ReadWriteTests.ReadValue, value);
        }

        [TestMethod]
        public void WriteTest()
        {
            IRedisClientsManager clientsManager = RedisManager.CreateClientsManager();
            IRedisClient client = clientsManager.GetClient();
            client.Set(ReadWriteTests.WriteKey, ReadWriteTests.WriteValue);
            string value = client.Get<string>(ReadWriteTests.WriteKey);

            Assert.AreEqual(ReadWriteTests.WriteValue, value);
        }
    }
}
