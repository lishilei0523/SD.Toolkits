using SD.Toolkits.EntityFramework.Base;
using SD.Toolkits.EntityFramework.Tests.StubEntities.Base;
using System;

namespace SD.Toolkits.EntityFramework.Tests.StubEntities
{
    /// <summary>
    /// EF上下文对象
    /// </summary>
    public class DbSession : DbContextBase
    {
        /// <summary>
        /// 基础构造器
        /// </summary>
        public DbSession()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// 实体所在程序集
        /// </summary>
        public override string EntityAssembly
        {
            get { return "SD.Toolkits.EntityFramework.Tests"; }
        }

        /// <summary>
        /// 实体配置所在程序集
        /// </summary>
        public override string EntityConfigAssembly
        {
            get { return "SD.Toolkits.EntityFramework.Tests"; }
        }

        /// <summary>
        /// 类型查询条件
        /// </summary>
        public override Func<Type, bool> TypeQuery
        {
            get { return x => x.IsSubclassOf(typeof(PlainEntity)); }
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
