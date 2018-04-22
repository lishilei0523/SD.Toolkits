using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

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
            IDatabase db = RedisManager.Instance.GetDatabase();
            db.StringSet(ReadWriteTests.ReadKey, ReadWriteTests.ReadValue);
        }

        [TestCleanup]
        public void Cleanup()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            db.KeyDelete(ReadWriteTests.ReadKey);
            db.KeyDelete(ReadWriteTests.WriteKey);
        }


        [TestMethod]
        public void ReadTest()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            string value = db.StringGet(ReadWriteTests.ReadKey);

            Assert.AreEqual(ReadWriteTests.ReadValue, value);
        }

        [TestMethod]
        public void WriteTest()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            db.StringSet(ReadWriteTests.WriteKey, ReadWriteTests.WriteValue);
            string value = db.StringGet(ReadWriteTests.WriteKey);

            Assert.AreEqual(ReadWriteTests.WriteValue, value);
        }
    }
}
