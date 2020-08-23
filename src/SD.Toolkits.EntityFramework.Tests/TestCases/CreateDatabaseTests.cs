﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFramework.Tests.StubEntities;

namespace SD.Toolkits.EntityFramework.Tests.TestCases
{
    /// <summary>
    /// 创建数据库测试
    /// </summary>
    /// <remarks>
    /// 测试目标：能够正常生成数据库
    /// </remarks>
    [TestClass]
    public class CreateDatabaseTests
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
            dbSession.Dispose();
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        [TestMethod]
        public void CreateDataBase()
        {
            //初始化
            DbSession dbSession = new DbSession();

            //断言数据库已创建成功
            Assert.IsTrue(dbSession.Database.Exists());
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
