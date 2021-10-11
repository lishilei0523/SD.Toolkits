using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.EntityConfigurations
{
    /// <summary>
    /// 单据实体配置
    /// </summary>
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
