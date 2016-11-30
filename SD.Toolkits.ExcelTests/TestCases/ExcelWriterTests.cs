using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel;
using SD.Toolkits.ExcelTests.StubEntities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SD.Toolkits.ExcelTests.TestCases
{
    /// <summary>
    /// Excel写入测试
    /// </summary>
    [TestClass]
    public class ExcelWriterTests
    {
        /// <summary>
        /// 目标路径
        /// </summary>
        private const string TargetExcelPath = "西游记.xls";

        /// <summary>
        /// 人员集
        /// </summary>
        private IList<Person> _persons;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            if (File.Exists(TargetExcelPath))
            {
                File.Delete(TargetExcelPath);
            }

            this._persons = new List<Person>
            {
                new Person{姓名="唐三藏", 性别 = true, 年龄 = 25, 爱好 = "取经"},
                new Person{姓名="孙悟空", 性别 = true, 年龄 = 19, 爱好 = "取经"},
                new Person{姓名="猪八戒", 性别 = true, 年龄 = 18, 爱好 = "取经"},
                new Person{姓名="沙悟净", 性别 = true, 年龄 = 17, 爱好 = null}
            };
        }

        /// <summary>
        /// 写入至文件测试
        /// </summary>
        [TestMethod]
        public void WriteToFileTest()
        {
            ExcelWriter.WriteFile(this._persons, TargetExcelPath);

            Assert.IsTrue(File.Exists(TargetExcelPath));
        }

        /// <summary>
        /// 写入至字节数组测试
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest()
        {
            byte[] buffer = ExcelWriter.WriteStream(this._persons);

            Assert.IsTrue(buffer.Any());
        }
    }
}
