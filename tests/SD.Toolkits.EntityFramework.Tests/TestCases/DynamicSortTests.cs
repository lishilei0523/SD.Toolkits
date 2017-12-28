using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFramework.Extensions;
using SD.Toolkits.EntityFramework.Tests.StubEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.EntityFramework.Tests.TestCases
{
    /// <summary>
    /// 动态排序测试
    /// </summary>
    [TestClass]
    public class DynamicSortTests
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.Delete();
            dbSession.Database.CreateIfNotExists();

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
        /// 测试动态排序
        /// </summary>
        [TestMethod]
        public void TestDynamicSort()
        {
            //初始化
            DbSession dbSession = new DbSession();

            //Lambda表达式排序
            IQueryable<Student> studens1 = dbSession.Set<Student>();
            studens1 = studens1.OrderByDescending(x => x.Gender).ThenBy(x => x.Name).ThenBy(x => x.Number);

            //动态排序
            IQueryable<Student> studens2 = dbSession.Set<Student>();

            IDictionary<string, bool> sorts = new Dictionary<string, bool>();
            sorts.Add("Gender", false); //性别降序
            sorts.Add("Name", true);    //名称升序
            sorts.Add("Number", true);  //变化升序

            studens2 = studens2.OrderBy(sorts);


            Assert.AreEqual(studens1.ParseSql(), studens2.ParseSql());
        }

        /// <summary>
        /// 测试清理
        /// </summary>
        [TestCleanup]
        public void Clean()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.Delete();

            dbSession.Dispose();
        }
    }
}
