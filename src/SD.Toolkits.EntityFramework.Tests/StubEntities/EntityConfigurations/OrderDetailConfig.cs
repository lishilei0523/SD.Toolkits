using System.Data.Entity.ModelConfiguration;

namespace SD.Toolkits.EntityFramework.Tests.StubEntities.EntityConfigurations
{
    /// <summary>
    /// 单据明细实体配置
    /// </summary>
    public class OrderDetailConfig : EntityTypeConfiguration<OrderDetail>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public OrderDetailConfig()
        {
            this.Property(x => x.Product).IsRequired();
        }
    }
}
