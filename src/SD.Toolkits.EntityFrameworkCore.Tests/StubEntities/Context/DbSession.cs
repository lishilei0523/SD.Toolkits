using Microsoft.EntityFrameworkCore;
using SD.Toolkits.EntityFrameworkCore.Base;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;
using System;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Context
{
    /// <summary>
    /// EF Cre上下文对象
    /// </summary>
    public class DbSession : DbContextBase
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=.;Initial Catalog=SD.Toolkits.EntityFrameworkCoreTests;User Id=sa;Password=realgoal123!;MultipleActiveResultSets=true;";
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
            get
            {
                return type =>
                    type != typeof(PlainEntity) &&
                    type != typeof(AggregateRootEntity) &&
                    type.IsSubclassOf(typeof(PlainEntity));
            }
        }

        /// <summary>
        /// 数据表名前缀
        /// </summary>
        public override string TablePrefix
        {
            get { return null; }
        }
    }
}
