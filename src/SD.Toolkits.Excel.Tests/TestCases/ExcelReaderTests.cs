using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel.Tests.StubEntities;

namespace SD.Toolkits.Excel.Tests.TestCases
{
    /// <summary>
    /// Excel读取测试
    /// </summary>
    [TestClass]
    public class ExcelReaderTests
    {
        /// <summary>
        /// 读取Excel 2003文件至数组测试（依靠索引定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByIndexTest03()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", 0, 1);

            Assert.IsTrue(persons.Length == 3);
        }

        /// <summary>
        /// 读取Excel 2003文件至数组测试（依靠名称定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest03()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", "default", 1);

            Assert.IsTrue(persons.Length == 3);
        }

        /// <summary>
        /// 读取Excel 2007文件至数组测试（依靠索引定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByIndexTest07()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xlsx", 0, 1);

            Assert.IsTrue(persons.Length == 3);
        }

        /// <summary>
        /// 读取Excel 2003文件至数组测试（依靠名称定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest07()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xlsx", "default", 1);

            Assert.IsTrue(persons.Length == 3);
        }
    }
}
