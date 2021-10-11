using System.Data.Entity.ModelConfiguration;

namespace SD.Toolkits.EntityFramework.Tests.StubEntities.EntityConfigurations
{
    /// <summary>
    /// 单据实体配置
    /// </summary>
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public OrderConfig()
        {
            this.Property(x => x.Number).IsRequired();
            this.Property(x => x.Name).IsRequired();
        }
    }
}
