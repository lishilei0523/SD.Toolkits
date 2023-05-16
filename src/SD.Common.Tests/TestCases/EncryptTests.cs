using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 字符串解密/解密测试
    /// </summary>
    [TestClass]
    public class EncryptTests
    {
        #region # 无Key测试 —— void TestWithoutKey()
        /// <summary>
        /// 无Key测试
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

        #region # 含Key测试 —— void TestWithKey()
        /// <summary>
        /// 含Key测试
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
