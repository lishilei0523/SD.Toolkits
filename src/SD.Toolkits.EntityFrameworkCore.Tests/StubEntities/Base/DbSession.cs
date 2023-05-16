using Microsoft.EntityFrameworkCore;
using SD.Infrastructure;
using SD.Infrastructure.Constants;
using SD.Toolkits.EntityFrameworkCore.Base;
using System;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base
{
    /// <summary>
    /// EF Core上下文
    /// </summary>
    internal class DbSession : DbContextBase
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GlobalSetting.WriteConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 实体所在程序集
        /// </summary>
        public override string EntityAssembly
        {
            get { return FrameworkSection.Setting.EntityAssembly.Value; }
        }

        /// <summary>
        /// 实体配置所在程序集
        /// </summary>
        public override string EntityConfigAssembly
        {
            get { return FrameworkSection.Setting.EntityConfigAssembly.Value; }
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
            get { return FrameworkSection.Setting.EntityTablePrefix.Value; }
        }
    }
}
