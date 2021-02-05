using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SD.Common.Tests
{
    /// <summary>
    /// 字符串解密解密测试
    /// </summary>
    [TestClass]
    public class EncryptTest
    {
        /// <summary>
        /// 无Key测试
        /// </summary>
        [TestMethod]
        public void TestNoKey()
        {
            const string text = "Hello World";

            string password = text.Encrypt();
            string source = password.Decrypt();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(text, source);
        }

        /// <summary>
        /// 含Key测试
        /// </summary>
        [TestMethod]
        public void TestContainsKey()
        {
            const string text = "Hello World";
            const string key = "123456";

            string password = text.Encrypt(key);
            string source = password.Decrypt(key);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(text, source);
        }
    }
}
