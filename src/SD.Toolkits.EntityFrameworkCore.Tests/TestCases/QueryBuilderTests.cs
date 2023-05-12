using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.EntityFrameworkCore.Extensions;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        /// <summary>
        /// 初始化测试
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
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

        /// <summary>
        /// 清理测试
        /// </summary>
        [TestCleanup]
        public void Clean()
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
    }
}
