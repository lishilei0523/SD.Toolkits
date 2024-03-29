﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Infrastructure;
using SD.Toolkits.EntityFrameworkCore.Extensions;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //初始化配置文件
            Assembly assembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(assembly);
            FrameworkSection.Initialize(configuration);

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
        #endregion

        #region # 测试清理 —— void Cleanup()
        /// <summary>
        /// 测试清理
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureDeleted();

            dbSession.Dispose();
        }
        #endregion

        #region # 测试IQueryable集合转换SQL语句 —— void TestParseSql()
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
        #endregion

        #region # 测试IQueryable集合能转换SQL语句 —— void TestCanParseSql()
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
        #endregion

        #region # 测试IQueryable集合不能转换SQL语句 —— void TestCannotParseSql()
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
        #endregion
    }
}
