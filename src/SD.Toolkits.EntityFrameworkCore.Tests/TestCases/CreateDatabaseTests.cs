using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Context;

namespace SD.Toolkits.EntityFrameworkCore.Tests.TestCases
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
        /// 初始化测试
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
        /// 创建数据库
        /// </summary>
        [TestMethod]
        public void CreateDataBase()
        {
            //初始化
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureCreated();
            dbSession.Database.Migrate();
            bool alreadyExisted = dbSession.Database.EnsureCreated();

            //断言数据库已创建成功
            Assert.IsFalse(alreadyExisted);
        }
    }
}
