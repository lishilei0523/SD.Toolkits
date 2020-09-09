using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.SerialNumber.Mediators;
using System;
using System.Linq;
using System.Text;

namespace SD.Toolkits.NoGenerator.Tests
{
    /// <summary>
    /// 注册机测试
    /// </summary>
    [TestClass]
    public class KeygenTests
    {
        /// <summary>
        /// 生成序列号测试
        /// </summary>
        [TestMethod]
        public void GenerateTest()
        {
            string seedName = nameof(KeygenTests);
            string prefix = "PRE";
            string stem = "STEM";
            string postfix = "POST";
            string timeFormat = "yyyyMMdd";
            int length = 3;
            string description = "描述";

            //生成序列号
            Keygen keygen = new Keygen();
            string serialNo = keygen.Generate(seedName, prefix, stem, postfix, timeFormat, length, description);

            //预期序列号
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(prefix);
            keyBuilder.Append(stem);
            keyBuilder.Append(postfix);
            keyBuilder.Append(DateTime.Now.ToString(timeFormat));
            string patialKey = keyBuilder.ToString();

            //断言
            Assert.IsTrue(serialNo.Contains(patialKey));
            Assert.AreEqual(serialNo.Length, patialKey.Length + length);
        }

        /// <summary>
        /// 批量生成序列号测试
        /// </summary>
        [TestMethod]
        public void GenerateRangeTest()
        {
            string seedName = nameof(KeygenTests);
            string prefix = "PRE";
            string stem = "STEM";
            string postfix = "POST";
            string timeFormat = "yyyyMMdd";
            int length = 3;
            string description = "描述";
            int count = 10;

            //生成序列号
            Keygen keygen = new Keygen();
            string[] serialNos = keygen.GenerateRange(seedName, prefix, stem, postfix, timeFormat, length, description, count);

            //预期序列号
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(prefix);
            keyBuilder.Append(stem);
            keyBuilder.Append(postfix);
            keyBuilder.Append(DateTime.Now.ToString(timeFormat));
            string patialKey = keyBuilder.ToString();

            //断言
            Assert.IsTrue(serialNos.All(serialNo => serialNo.Contains(patialKey)));
            Assert.AreEqual(serialNos.Length, count);
        }
    }
}
