using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common.Tests.StubEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 集合测试
    /// </summary>
    [TestClass]
    public class CollectionTests
    {
        #region # 字段与初始化器

        /// <summary>
        /// 源集合
        /// </summary>
        private IList<Student> _sourceList;

        /// <summary>
        /// 目标集合
        /// </summary>
        private IList<Student> _targetList;

        /// <summary>
        /// 源字典
        /// </summary>
        private IDictionary<string, Student> _sourceDict;

        /// <summary>
        /// 目标字典
        /// </summary>
        private IDictionary<string, Student> _targetDict;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this._sourceList = new List<Student>
            {
                new Student{Id = 3,Name = "学生3",BirthDay = new DateTime(1996,2,20)},
                new Student{Id = 1,Name = "学生1",BirthDay = new DateTime(1992,4,18)},
                new Student{Id = 2,Name = "学生2",BirthDay = new DateTime(1991,4,20)}
            };
            this._targetList = new List<Student>
            {
                new Student{Id = 1,Name = "学生1",BirthDay = new DateTime(1992,4,18)},
                new Student{Id = 2,Name = "学生2",BirthDay = new DateTime(1991,4,20)},
                new Student{Id = 3,Name = "学生3",BirthDay = new DateTime(1996,2,20)}
            };

            this._sourceDict = new Dictionary<string, Student>
            {
                {"001", this._sourceList[0]},
                {"002", this._sourceList[1]},
                {"003", this._sourceList[2]}
            };
            this._targetDict = new Dictionary<string, Student>
            {
                {"001", this._sourceList[0]},
                {"002", this._sourceList[1]},
                {"003", this._sourceList[2]}
            };
        }

        #endregion

        #region # 测试集合转DataTable —— void TestEnumableToDataTable()
        /// <summary>
        /// 测试集合转DataTable
        /// </summary>
        [TestMethod]
        public void TestEnumableToDataTable()
        {
            IList<Student> students = new List<Student>
            {
                new Student
                {
                    Id = 3, Name = "学生3", Gender = Gender.Male, Age = 21, BirthDay = new DateTime(1996, 2, 20)
                },
                new Student
                {
                    Id = 1, Name = "学生1", Gender = Gender.Female, Age = null, BirthDay = new DateTime(1992, 4, 18)
                },
                new Student
                {
                    Id = 2, Name = "学生2", Gender = null, Age = 19, BirthDay = new DateTime(1991, 4, 20)
                }
            };

            DataTable dataTable = students.ToDataTable(true);

            Assert.AreEqual(students.Count, dataTable.Rows.Count);
        }
        #endregion

        #region # 测试DataTable转集合 —— void TestDataTableToCollection()
        /// <summary>
        /// 测试DataTable转集合
        /// </summary>
        [TestMethod]
        public void TestDataTableToCollection()
        {
            IList<Student> students = new List<Student>
            {
                new Student {Id = 3, Name = "学生3", Age = 21, BirthDay = new DateTime(1996, 2, 20)},
                new Student {Id = 1, Name = "学生1", Age = null, BirthDay = new DateTime(1992, 4, 18)},
                new Student
                {
                    Id = 2, Name = "学生2", Age = 19, BirthDay = new DateTime(1991, 4, 20),
                    Extension = new StudentExtension
                    {
                        Extension1 = "扩展1",
                        Extension2 = "扩展2"
                    }
                }
            };

            DataTable dataTable = students.ToDataTable();
            ICollection<Student> collection = dataTable.ToCollection<Student>();

            Assert.IsTrue(students.EqualsTo(collection));
        }
        #endregion

        #region # 测试集合是否值相等 —— void TestCollectionEquals()
        /// <summary>
        /// 测试集合是否值相等
        /// </summary>
        [TestMethod]
        public void TestCollectionEquals()
        {
            Assert.IsTrue(this._sourceList.OrderBy(x => x.Id).EqualsTo(this._targetList.OrderBy(x => x.Id)));
        }
        #endregion

        #region # 测试字典是否值相等 —— void TestDictionaryEquals()
        /// <summary>
        /// 测试字典是否值相等
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals()
        {
            Assert.IsTrue(this._sourceDict.EqualsTo(this._targetDict));
        }
        #endregion

        #region # 测试字典是否值相等 —— void TestDictionaryEquals2()
        /// <summary>
        /// 测试字典是否值相等
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals2()
        {
            IDictionary<Guid, float> source = new Dictionary<Guid, float>();
            source.Add(new Guid("494bcada-3377-476e-912f-1eecef3eb9fc"), 10.0f);

            IDictionary<Guid, float> target = new Dictionary<Guid, float>();
            target.Add(new Guid("494bcada-3377-476e-912f-1eecef3eb9fc"), 10.0f);

            Assert.IsTrue(source.EqualsTo(target));
        }
        #endregion
    }
}
