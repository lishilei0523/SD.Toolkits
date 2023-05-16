using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Infrastructure;
using SD.Toolkits.EntityFrameworkCore.Extensions;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.Toolkits.EntityFrameworkCore.Tests.TestCases
{
    /// <summary>
    /// 查询建造者测试
    /// </summary>
    /// <remarks>
    /// 测试目标：动态拼接Lambda表达式
    /// </remarks>
    [TestClass]
    public class QueryBuilderTests
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
            IList<OrderDetail> details = new List<OrderDetail>
            {
                new OrderDetail("香蕉", 10),
                new OrderDetail("苹果", 50),
                new OrderDetail("桃子", 20)
            };

            Order order = new Order("001", "单据1", "单据描述");
            order.SetDetails(details);

            dbSession.Set<Order>().Add(order);
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
            //清理数据库
            DbSession dbSession = new DbSession();
            foreach (OrderDetail detail in dbSession.Set<OrderDetail>().ToList())
            {
                dbSession.Set<OrderDetail>().Remove(detail);
            }
            foreach (Order order in dbSession.Set<Order>().ToList())
            {
                dbSession.Set<Order>().Remove(order);
            }

            dbSession.SaveChanges();
            dbSession.Dispose();
        }
        #endregion

        #region # 测试逻辑与 —— void TestBuildAnd()
        /// <summary>
        /// 测试逻辑与
        /// </summary>
        [TestMethod]
        public void TestBuildAnd()
        {
            string orderNo = "001";
            DateTime startTime = DateTime.Now.AddDays(-1);
            DateTime endTime = DateTime.Now.AddDays(1);

            QueryBuilder<Order> queryBuilder = QueryBuilder<Order>.Affirm();
            queryBuilder.And(x => x.Number == orderNo);
            queryBuilder.And(x => x.AddedTime >= startTime);
            queryBuilder.And(x => x.AddedTime <= endTime);
            Expression<Func<Order, bool>> condition = queryBuilder.Build();

            DbSession dbSession = new DbSession();
            IQueryable<Order> orders = dbSession.Set<Order>().Where(condition);
            string sql = orders.ParseSql();

            Assert.IsNotNull(sql);
        }
        #endregion

        #region # 测试逻辑或 —— void TestBuildOr()
        /// <summary>
        /// 测试逻辑或
        /// </summary>
        [TestMethod]
        public void TestBuildOr()
        {
            string orderNo = "001";
            string orderName = "单据1";

            QueryBuilder<Order> queryBuilder = QueryBuilder<Order>.Negate();
            queryBuilder.Or(x => x.Number == orderNo);
            queryBuilder.Or(x => x.Name == orderName);
            Expression<Func<Order, bool>> condition = queryBuilder.Build();

            DbSession dbSession = new DbSession();
            IQueryable<Order> orders = dbSession.Set<Order>().Where(condition);
            string sql = orders.ParseSql();

            Assert.IsNotNull(sql);
        }
        #endregion

        #region # 测试导航属性 —— void TestBuildWithNavigationProperty()
        /// <summary>
        /// 测试导航属性
        /// </summary>
        [TestMethod]
        public void TestBuildWithNavigationProperty()
        {
            string orderNo = "001";
            string product = "香蕉";

            QueryBuilder<Order> queryBuilder = QueryBuilder<Order>.Negate();
            queryBuilder.Or(x => x.Number == orderNo);
            queryBuilder.Or(x => x.Details.Any(y => y.Product == product));
            Expression<Func<Order, bool>> condition = queryBuilder.Build();

            DbSession dbSession = new DbSession();
            IQueryable<Order> orders = dbSession.Set<Order>().Where(condition);
            string sql = orders.ParseSql();

            Assert.IsNotNull(sql);
        }
        #endregion
    }
}
