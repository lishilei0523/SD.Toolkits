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
    /// IQueryable集合转换SQL语句测试
    /// </summary>
    /// <remarks>
    /// 测试目标：
    /// 调试可查看IQueryable集合转换SQL语句，
    /// 判断Lambda表达式能否转换SQL语句
    /// </remarks>
    [TestClass]
    public class ParseSqlTests
    {
        /// <summary>
        /// 初始化测试
        /// </summary>
        [TestInitialize]
        public void Init()
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
        /// 测试IQueryable集合转换SQL语句
        /// </summary>
        [TestMethod]
        public void TestParseSql()
        {
            DbSession dbSession = new DbSession();
            IQueryable<Student> specStudents = dbSession.Set<Student>().Where(x => x.Age > 18);

            string sql = specStudents.ParseSql();
            Assert.IsNotNull(sql);
        }

        /// <summary>
        /// 测试IQueryable集合能转换SQL语句
        /// </summary>
        [TestMethod]
        public void TestCanParseSql()
        {
            DbSession dbSession = new DbSession();
            IQueryable<Student> specStudents = dbSession.Set<Student>().Where(x => x.Age > 18);

            Assert.IsTrue(specStudents.CanParseSQl());
        }

        /// <summary>
        /// 测试IQueryable集合不能转换SQL语句
        /// </summary>
        [TestMethod]
        public void TestCannotParseSql()
        {
            DbSession dbSession = new DbSession();
            IQueryable<Student> specStudents = dbSession.Set<Student>().Where(x => x.Age > float.Parse("18"));

            Assert.IsFalse(specStudents.CanParseSQl());
        }
    }
}
