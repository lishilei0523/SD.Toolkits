using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFramework.Extensions;
using SD.Toolkits.EntityFramework.Tests.StubEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.EntityFramework.Tests.TestCases
{
    /// <summary>
    /// 拼接Lambda表达式测试
    /// </summary>
    /// <remarks>
    /// 测试目标：动态拼接Lambda表达式
    /// </remarks>
    [TestClass]
    public class BuildExpressionTests
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
        /// 测试动态拼接Lambda表达式
        /// </summary>
        [TestMethod]
        public void TestBuildExpression()
        {
            //初始化
            DbSession dbSession = new DbSession();

            string[] keywords = { "你好", "分词", "等等" };

            //动态拼接
            PredicateBuilder<Student> builder = new PredicateBuilder<Student>(x => false);

            foreach (string keyword in keywords)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    builder.Or(x => x.Keywords.Contains(keyword));
                }
            }

            IQueryable<Student> specStudents = dbSession.Set<Student>().Where(builder.Build());

            string sql = specStudents.ParseSql();
            Assert.IsNotNull(sql);
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
