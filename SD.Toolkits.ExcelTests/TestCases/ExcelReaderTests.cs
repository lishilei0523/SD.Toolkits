using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Excel;
using SD.Toolkits.ExcelTests.StubEntities;

namespace SD.Toolkits.ExcelTests.TestCases
{
    /// <summary>
    /// Excel读取测试
    /// </summary>
    [TestClass]
    public class ExcelReaderTests
    {
        /// <summary>
        /// 读取文件至数组测试（依靠索引定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByIndexTest()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", 0, 1);

            Assert.IsTrue(persons.Length == 3);
        }

        /// <summary>
        /// 读取文件至数组测试（依靠名称定位工作表）
        /// </summary>
        [TestMethod]
        public void ReadToArrayByNameTest()
        {
            Person[] persons = ExcelReader.ReadFile<Person>("StubExcel.xls", "default", 1);

            Assert.IsTrue(persons.Length == 3);
        }
    }
}
