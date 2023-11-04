using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SD.Toolkits.Drawing.Tests.TestCases
{
    /// <summary>
    /// 验证码测试
    /// </summary>
    [TestClass]
    public class ValidCodeTests
    {
        #region # 测试生成验证码 —— void TestGenerateValidCode()
        /// <summary>
        /// 测试生成验证码
        /// </summary>
        [TestMethod]
        public void TestGenerateValidCode()
        {
            string validCode = ValidCodeGenerator.GenerateCode(4);
            Console.WriteLine(validCode);
        }
        #endregion

        #region # 测试生成验证码图片 —— void TestGenerateValidCodeImage()
        /// <summary>
        /// 测试生成验证码图片
        /// </summary>
        [TestMethod]
        public void TestGenerateValidCodeImage()
        {
            string validCode = ValidCodeGenerator.GenerateCode(4);
            byte[] imageBytes = ValidCodeGenerator.GenerateStream(validCode);

            File.WriteAllBytes("Images/ValidCode.jpg", imageBytes);
        }
        #endregion
    }
}
