using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel.Tests.Models;

namespace SD.Toolkits.Excel.Tests.TestCases
{
    /// <summary>
    /// Excel读取测试
    /// </summary>
    [TestClass]
    public class ExcelReaderTests
    {
        #region # 测试读取Excel 2003文件至数组 —— void TestReadToArrayByIndex03()
        /// <summary>
        /// 测试读取Excel 2003文件至数组
        /// </summary>
        /// <remarks>依靠索引定位工作表</remarks>
        [TestMethod]
        public void TestReadToArrayByIndex03()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", 0, 1);

            Assert.IsTrue(persons.Length == 4);
        }
        #endregion

        #region # 测试读取Excel 2003文件至数组 —— void TestReadToArrayByName03()
        /// <summary>
        /// 测试读取Excel 2003文件至数组
        /// </summary>
        /// <remarks>依靠名称定位工作表</remarks>
        [TestMethod]
        public void TestReadToArrayByName03()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", "default", 1);

            Assert.IsTrue(persons.Length == 4);
        }
        #endregion

        #region # 测试读取Excel 2007文件至数组 —— void TestReadToArrayByIndex07()
        /// <summary>
        /// 测试读取Excel 2007文件至数组
        /// </summary>
        /// <remarks>依靠索引定位工作表</remarks>
        [TestMethod]
        public void TestReadToArrayByIndex07()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xlsx", 0, 1);

            Assert.IsTrue(persons.Length == 4);
        }
        #endregion

        #region # 测试读取Excel 2003文件至数组 —— void TestReadToArrayByName07()
        /// <summary>
        /// 测试读取Excel 2003文件至数组
        /// </summary>
        /// <remarks></remarks>
        [TestMethod]
        public void TestReadToArrayByName07()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xlsx", "default", 1);

            Assert.IsTrue(persons.Length == 4);
        }
        #endregion
    }
}
