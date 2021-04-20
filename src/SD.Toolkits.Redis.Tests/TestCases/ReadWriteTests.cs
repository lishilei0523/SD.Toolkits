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
            db.StringSet(ReadKey, ReadValue);
        }

        [TestCleanup]
        public void Cleanup()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            db.KeyDelete(ReadKey);
            db.KeyDelete(WriteKey);
        }


        [TestMethod]
        public void ReadTest()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            string value = db.StringGet(ReadKey);

            Assert.AreEqual(ReadValue, value);
        }

        [TestMethod]
        public void WriteTest()
        {
            IDatabase db = RedisManager.Instance.GetDatabase();
            db.StringSet(WriteKey, WriteValue);
            string value = db.StringGet(WriteKey);

            Assert.AreEqual(WriteValue, value);
        }
    }
}
