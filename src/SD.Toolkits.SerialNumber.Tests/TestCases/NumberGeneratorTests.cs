using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.SerialNumber.Mediators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.Toolkits.SerialNumber.Tests.TestCases
{
    /// <summary>
    /// 序列号生成器测试
    /// </summary>
    [TestClass]
    public class NumberGeneratorTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            SerialNumberSection.Initialize(configuration);
#endif
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", baseDirectory);
        }
        #endregion

        #region # 测试生成序列号 —— void TestGenerate()
        /// <summary>
        /// 测试生成序列号
        /// </summary>
        [TestMethod]
        public void TestGenerate()
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
        #endregion

        #region # 测试批量生成序列号 —— void TestGenerateRange()
        /// <summary>
        /// 测试批量生成序列号
        /// </summary>
        [TestMethod]
        public void TestGenerateRange()
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
        #endregion

        #region # 测试Null —— void TestNull()
        /// <summary>
        /// 测试Null
        /// </summary>
        [TestMethod]
        public void TestNull()
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
        #endregion
    }
}
