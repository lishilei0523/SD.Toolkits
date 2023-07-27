using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 字符串加密/解密测试
    /// </summary>
    [TestClass]
    public class EncryptTests
    {
        #region # 测试无Key —— void TestWithoutKey()
        /// <summary>
        /// 测试无Key
        /// </summary>
        [TestMethod]
        public void TestWithoutKey()
        {
            const string text = "Hello World";

            string ciphertext = text.Encrypt();
            string plaintext = ciphertext.Decrypt();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(text, plaintext);
        }
        #endregion

        #region # 测试含Key —— void TestWithKey()
        /// <summary>
        /// 测试含Key
        /// </summary>
        [TestMethod]
        public void TestWithKey()
        {
            const string text = "Hello World";
            const string key = "123456";

            string ciphertext = text.Encrypt(key);
            string plaintext = ciphertext.Decrypt(key);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(text, plaintext);
        }
        #endregion
    }
}
