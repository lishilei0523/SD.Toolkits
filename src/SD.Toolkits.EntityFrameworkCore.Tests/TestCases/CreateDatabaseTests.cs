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
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //删除数据库
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureDeleted();
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

        #region # 测试创建数据库 —— void TestCreateDatabase()
        /// <summary>
        /// 测试创建数据库
        /// </summary>
        [TestMethod]
        public void TestCreateDatabase()
        {
            //初始化
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureCreated();
            dbSession.Database.Migrate();
            bool alreadyExisted = dbSession.Database.EnsureCreated();

            //断言数据库已创建成功
            Assert.IsFalse(alreadyExisted);
        }
        #endregion
    }
}
