using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.SerialNumber.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Toolkits.NoGenerator.Tests
{
    /// <summary>
    /// 序列号生成器测试
    /// </summary>
    [TestClass]
    public class NumberGeneratorTests
    {
        /// <summary>
        /// 生成序列号测试
        /// </summary>
        [TestMethod]
        public void GenerateTest()
        {
            string seedName = nameof(NumberGeneratorTests);
            string prefix = "PRE";
            string timeFormat = "yyyyMMdd";
            int length = 3;
            string description = "描述";

            //生成序列号
            NumberGenerator generator = new NumberGenerator();
            string serialNo = generator.Generate(seedName, prefix, timeFormat, length, description);

            //预期序列号
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(prefix);
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
            string seedName = nameof(NumberGeneratorTests);
            string prefix = "PRE";
            string timeFormat = "yyyyMMdd";
            int length = 3;
            string description = "描述";
            int count = 10;

            //生成序列号
            NumberGenerator generator = new NumberGenerator();
            string[] serialNos = generator.GenerateRange(seedName, prefix, timeFormat, length, description, count);

            //预期序列号
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(prefix);
            keyBuilder.Append(DateTime.Now.ToString(timeFormat));
            string patialKey = keyBuilder.ToString();

            //断言
            Assert.IsTrue(serialNos.All(serialNo => serialNo.Contains(patialKey)));
            Assert.AreEqual(serialNos.Length, count);
        }

        /// <summary>
        /// Null测试
        /// </summary>
        [TestMethod]
        public void NullTest()
        {
            string seedName = nameof(NumberGeneratorTests);
            string prefix = null;
            string timeFormat = null;
            int length = 3;
            string description = null;

            //生成序列号
            NumberGenerator generator = new NumberGenerator();

            int count = 30;
            ICollection<string> keys = new HashSet<string>();
            for (int index = 0; index < count; index++)
            {
                string serialNo = generator.Generate(seedName, prefix, timeFormat, length, description);
                keys.Add(serialNo);

                //断言
                Assert.AreEqual(serialNo.Length, length);
            }

            Assert.AreEqual(count, keys.Count);
        }
    }
}
