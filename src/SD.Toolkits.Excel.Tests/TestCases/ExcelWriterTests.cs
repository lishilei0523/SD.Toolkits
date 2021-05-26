using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel.Tests.StubEntities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SD.Toolkits.Excel.Tests.TestCases
{
    /// <summary>
    /// Excel写入测试
    /// </summary>
    [TestClass]
    public class ExcelWriterTests
    {
        /// <summary>
        /// Excel 2003目标路径
        /// </summary>
        private const string TargetExcel03Path = "西游记.xls";

        /// <summary>
        /// Excel 2007目标路径
        /// </summary>
        private const string TargetExcel07Path = "西游记.xlsx";

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
            this._persons = new List<Person>
            {
                new Person {姓名 = "唐三藏", 性别 = true, 年龄 = 25, 爱好 = "取经"},
                new Person {姓名 = "孙悟空", 性别 = true, 年龄 = 19, 爱好 = "取经"},
                new Person {姓名 = "猪八戒", 性别 = true, 年龄 = 18, 爱好 = "取经"},
                new Person {姓名 = "沙悟净", 性别 = true, 年龄 = 17, 爱好 = null}
            };
        }

        /// <summary>
        /// Excel 2003写入至文件测试
        /// </summary>
        [TestMethod]
        public void WriteToFileTest03()
        {
            if (File.Exists(TargetExcel03Path))
            {
                File.Delete(TargetExcel03Path);
            }

            ExcelWriter.WriteFile(this._persons, TargetExcel03Path);
            Assert.IsTrue(File.Exists(TargetExcel03Path));
        }

        /// <summary>
        /// Excel 2003写入至字节数组测试
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest03()
        {
            byte[] buffer = ExcelWriter.WriteStream(this._persons, ExcelVersion.Excel03);

            Assert.IsTrue(buffer.Any());
        }

        /// <summary>
        /// Excel 2007写入至文件测试
        /// </summary>
        [TestMethod]
        public void WriteToFileTest07()
        {
            if (File.Exists(TargetExcel07Path))
            {
                File.Delete(TargetExcel07Path);
            }

            ExcelWriter.WriteFile(this._persons, TargetExcel07Path);
            Assert.IsTrue(File.Exists(TargetExcel07Path));
        }

        /// <summary>
        /// Excel 2007写入至字节数组测试
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest07()
        {
            byte[] buffer = ExcelWriter.WriteStream(this._persons, ExcelVersion.Excel07);

            Assert.IsTrue(buffer.Any());
        }
    }
}
