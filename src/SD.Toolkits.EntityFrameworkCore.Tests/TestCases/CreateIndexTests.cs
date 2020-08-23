﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;

namespace SD.Toolkits.EntityFrameworkCore.Tests.TestCases
{
    /// <summary>
    /// 创建索引测试
    /// </summary>
    /// <remarks>
    /// 测试目标：成功创建Student表ClassId非聚集索引
    /// </remarks>
    [TestClass]
    public class CreateIndexTests
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureDeleted();
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
            dbSession.Database.EnsureCreated();
            bool isDbExists = dbSession.Database.EnsureCreated();

            //断言数据库已创建成功
            //TODO 查看数据库Student表是否有ClassId的索引
            Assert.IsTrue(isDbExists);
        }
    }
}
