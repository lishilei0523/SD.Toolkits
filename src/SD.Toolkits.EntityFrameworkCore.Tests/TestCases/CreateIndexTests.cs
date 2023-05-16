using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Infrastructure;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;
using System.Configuration;
using System.Reflection;

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
        /// <remarks>查看数据库Student表是否有ClassId的索引</remarks>
        [TestMethod]
        public void TestCreateDatabase()
        {
            //初始化
            DbSession dbSession = new DbSession();
            dbSession.Database.EnsureCreated();
            dbSession.Database.Migrate();
        }
        #endregion
    }
}
