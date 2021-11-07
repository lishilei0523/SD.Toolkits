using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common.Tests.StubEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 集合测试
    /// </summary>
    [TestClass]
    public class CollectionTests
    {
        /// <summary>
        /// 集合转DataTable测试
        /// </summary>
        [TestMethod]
        public void TestEnumableToDataTable()
        {
            IList<Student> students = new List<Student>
            {
                new Student { Id = 3, Name = "学生3", Age = 21, BirthDay = new DateTime(1996, 2, 20) },
                new Student { Id = 1, Name = "学生1", Age = null, BirthDay = new DateTime(1992, 4, 18) },
                new Student { Id = 2, Name = "学生2", Age = 19, BirthDay = new DateTime(1991, 4, 20) }
            };

            DataTable dataTable = students.ToDataTable(true);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(students.Count, dataTable.Rows.Count);
        }

        /// <summary>
        /// DataTable转集合测试
        /// </summary>
        [TestMethod]
        public void TestDataTableToCollection()
        {
            IList<Student> students = new List<Student>
            {
                new Student { Id = 3, Name = "学生3", Age = 21, BirthDay = new DateTime(1996, 2, 20) },
                new Student { Id = 1, Name = "学生1", Age = null, BirthDay = new DateTime(1992, 4, 18) },
                new Student { Id = 2, Name = "学生2", Age = 19, BirthDay = new DateTime(1991, 4, 20),Extension = new StudentExtension{Extension1 = "扩展1",Extension2 = "扩展2"}}
            };

            DataTable dataTable = students.ToDataTable();
            ICollection<Student> collection = dataTable.ToCollection<Student>();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(students.EqualsTo(collection));
        }
    }
}
