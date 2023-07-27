using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel.Tests.Models;
using System;
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
        #region # 测试初始化

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
        public void Initialize()
        {
            this._persons = new List<Person>
            {
                new Person {姓名 = "唐三藏", 性别 = true, 年龄 = 25, 爱好 = "取经", 日期 = DateTime.Now},
                new Person {姓名 = "孙悟空", 性别 = true, 年龄 = 19, 爱好 = "取经", 日期 = DateTime.Now},
                new Person {姓名 = "猪八戒", 性别 = true, 年龄 = 18, 爱好 = "取经", 日期 = DateTime.Now},
                new Person {姓名 = "沙悟净", 性别 = true, 年龄 = 17, 爱好 = null, 日期 = DateTime.Now},
                new Person {姓名 = "费奥多尔·米哈伊洛维奇·陀思妥耶夫斯基", 性别 = true, 年龄 = 80, 爱好 = "文学", 日期 = DateTime.Now}
            };
        }

        #endregion

        #region # 测试Excel 2003写入至文件 —— void TestWriteToFile03()
        /// <summary>
        /// 测试Excel 2003写入至文件
        /// </summary>
        [TestMethod]
        public void TestWriteToFile03()
        {
            if (File.Exists(TargetExcel03Path))
            {
                File.Delete(TargetExcel03Path);
            }

            ExcelWriter.WriteFile(this._persons, TargetExcel03Path);
            Assert.IsTrue(File.Exists(TargetExcel03Path));
        }
        #endregion

        #region # 测试Excel 2003写入至字节数组 —— void TestWriteToArray03()
        /// <summary>
        /// 测试Excel 2003写入至字节数组
        /// </summary>
        [TestMethod]
        public void TestWriteToArray03()
        {
            byte[] buffer = ExcelWriter.WriteStream(this._persons, ExcelVersion.Excel03);

            Assert.IsTrue(buffer.Any());
        }
        #endregion

        #region # 测试Excel 2007写入至文件 —— void TestWriteToFile07()
        /// <summary>
        /// 测试Excel 2007写入至文件
        /// </summary>
        [TestMethod]
        public void TestWriteToFile07()
        {
            if (File.Exists(TargetExcel07Path))
            {
                File.Delete(TargetExcel07Path);
            }

            ExcelWriter.WriteFile(this._persons, TargetExcel07Path);
            Assert.IsTrue(File.Exists(TargetExcel07Path));
        }
        #endregion

        #region # 测试Excel 2007写入至字节数组 —— void TestWriteToArray07()
        /// <summary>
        /// 测试Excel 2007写入至字节数组
        /// </summary>
        [TestMethod]
        public void TestWriteToArray07()
        {
            byte[] buffer = ExcelWriter.WriteStream(this._persons, ExcelVersion.Excel07);

            Assert.IsTrue(buffer.Any());
        }
        #endregion
    }
}
