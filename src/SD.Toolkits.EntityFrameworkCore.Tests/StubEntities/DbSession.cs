using Microsoft.EntityFrameworkCore;
using SD.Toolkits.EntityFrameworkCore.Base;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;
using System;
using System.Configuration;
using System.Diagnostics;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities
{
    /// <summary>
    /// EntityFramework上下文对象
    /// </summary>
    public class DbSession : BaseDbContext
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public DbSession()
        {
            base.Database.EnsureCreated();
        }

        /// <summary>
        /// 配置
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            Trace.WriteLine(connectionStringSettings);

            string connectionString = "Data Source=.;Initial Catalog=EntityFrameworkCore;User Id=sa;Password=realgoal123!;MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 实体所在程序集
        /// </summary>
        public override string EntityAssembly
        {
            get { return "SD.Toolkits.EntityFrameworkCore.Tests"; }
        }

        /// <summary>
        /// 实体配置所在程序集
        /// </summary>
        public override string EntityConfigAssembly
        {
            get { return "SD.Toolkits.EntityFrameworkCore.Tests"; }
        }

        /// <summary>
        /// 类型查询条件
        /// </summary>
        public override Func<Type, bool> TypeQuery
        {
            get { return x => x.IsSubclassOf(typeof(AggregateRootEntity)); }
        }

        /// <summary>
        /// 数据表名前缀
        /// </summary>
        public override string TablePrefix
        {
            get { return "My"; }
        }
    }
}
