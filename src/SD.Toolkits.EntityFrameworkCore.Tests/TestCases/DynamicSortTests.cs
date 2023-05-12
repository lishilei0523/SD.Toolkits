using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFrameworkCore.Extensions;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.EntityFrameworkCore.Tests.TestCases
{
    /// <summary>
    /// 动态排序测试
    /// </summary>
    [TestClass]
    public class DynamicSortTests
    {
        /// <summary>
        /// 初始化测试
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureCreated();

            //初始化数据
            IList<Student> students = new List<Student>();
            students.Add(new Student("001", "张三", true, 18, Guid.NewGuid()));
            students.Add(new Student("002", "李四", true, 21, Guid.NewGuid()));
            students.Add(new Student("003", "王五", false, 26, Guid.NewGuid()));
            students.Add(new Student("004", "赵六", false, 16, Guid.NewGuid()));
            students.Add(new Student("005", "天气", false, 26, Guid.NewGuid()));
            students.Add(new Student("006", "张三", true, 14, Guid.NewGuid()));

            dbSession.Set<Student>().AddRange(students);
            dbSession.SaveChanges();
            dbSession.Dispose();
        }

        /// <summary>
        /// 清理测试
        /// </summary>
        [TestCleanup]
        public void Clean()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureDeleted();

            dbSession.Dispose();
        }

        /// <summary>
        /// 测试动态排序
        /// </summary>
        [TestMethod]
        public void TestDynamicSort()
        {
            //初始化
            DbSession dbSession = new DbSession();

            //Lambda表达式排序
            IQueryable<Student> students1 = dbSession.Set<Student>();
            students1 = students1.OrderByDescending(x => x.Gender).ThenBy(x => x.Name).ThenBy(x => x.Number);

            //动态排序
            IDictionary<string, bool> sorts = new Dictionary<string, bool>();
            sorts.Add(nameof(Student.Gender), false); //性别降序
            sorts.Add(nameof(Student.Name), true);    //名称升序
            sorts.Add(nameof(Student.Number), true);  //编号升序

            IQueryable<Student> students2 = dbSession.Set<Student>();
            students2 = students2.OrderBy(sorts);

            string sql1 = students1.ParseSql();
            string sql2 = students2.ParseSql();

            Assert.AreEqual(sql1, sql2);
        }
    }
}
